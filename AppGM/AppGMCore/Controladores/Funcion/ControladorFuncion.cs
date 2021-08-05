using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador de <see cref="ModeloFuncion"/>
	/// </summary>
	public abstract class ControladorFuncionBase : Controlador<ModeloFuncion>
	{
		#region Campos & Propiedades

		//----------------------------------CAMPOS------------------------------------

		/// <summary>
		/// Contiene todas los <see cref="ControladorVariableFuncionBase"/> de las variables persistentes de esta funcion
		/// </summary>
		private Dictionary<int, ControladorVariableFuncionBase> mVariablesPersistenes;

		//-------------------------------PROPIEDADES-------------------------------------

		/// <summary>
		/// <see cref="List{T}"/> que contiene todos los <see cref="BloqueBase"/> que componen esta funcion
		/// </summary>
		public List<BloqueBase> Bloques { get; private set; } = new List<BloqueBase>();

		/// <summary>
		/// Nombre de la funcion incluyendo su extension
		/// </summary>
		public string NombreArchivoFuncion => string.Intern($"{modelo.NombreFuncion}_{modelo.Id}.xml");

		public string NombreCompletoArchivoFuncion => string.Intern(ObtenerPathArchivoFuncion(NombreArchivoFuncion));

		/// <summary>
		/// Nombre de la funcion
		/// </summary>
		public string NombreFuncion
		{
			get => modelo.NombreFuncion;
			set
			{
				if (value == NombreFuncion || value.IsNullOrWhiteSpace())
					return;

				if (File.Exists(NombreCompletoArchivoFuncion))
				{
					//Intentamos cambiar el nombre del archivo
					try
					{
						File.Move(
							Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones,
								NombreArchivoFuncion),
							Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones,
								string.Intern(ObtenerNombreArchivo(value, modelo.Id))));
					}
					catch (Exception ex)
					{
						SistemaPrincipal.LoggerGlobal.LogCrash($"Error al intentar renombrar archivo {NombreCompletoArchivoFuncion}{Environment.NewLine}Excepcion:{ex.Message}");
					}
				}

				modelo.NombreFuncion = value;
			}
		} 

		#endregion

		#region Constructor

		public ControladorFuncionBase(ModeloFuncion _modelo)
			: base(_modelo)
		{
			//Si no hay variables persistentes creamos una lista nueva
			modelo.VariablesPersistentes ??= new List<TIFuncionVariable>();

			mVariablesPersistenes = new Dictionary<int, ControladorVariableFuncionBase>(modelo.VariablesPersistentes.Select(var =>
			{
				return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, ControladorVariableFuncionBase.CrearControladorCorrespondiente(var.Variable));
			}));
		} 

		#endregion

		#region Metodo

		/// <summary>
		/// Carga los <see cref="Bloques"/>
		/// </summary>
		/// <returns><see cref="bool"/> indicando si se pudieron cargar los bloques</returns>
		public bool CargarBloques()
		{
			SistemaPrincipal.LoggerGlobal.Log($"Cargando bloques para funcion {NombreFuncion}", ESeveridad.Info);

			var bloquesCargados = BloqueBase.DesdeXmlMultiple(NombreCompletoArchivoFuncion);

			if (bloquesCargados == null)
				return false;

			Bloques = bloquesCargados;

			return true;
		}

		/// <summary>
		/// Ejecuta <see cref="CargarBloques"/> en otro hilo
		/// </summary>
		/// <returns>Resultado de <see cref="CargarBloques"/></returns>
		public async Task<bool> CargarBloquesAsync()
		{
			return await Task.Run(CargarBloques);
		}

		/// <summary>
		/// Actualiza <see cref="Bloques"/>.
		/// </summary>
		/// <param name="nuevosBloques">Lista con los bloques actuales</param>
		public void ActualizarBloques(List<BloqueBase> nuevosBloques)
		{
			Bloques.Clear();

			Bloques.AddRange(nuevosBloques);

			Bloques.TrimExcess();

			List<int> idsVariablesPersistentesExistentes =
				modelo.VariablesPersistentes.Select(var => var.IDVariable).ToList();

			foreach (var bloque in nuevosBloques)
			{
				if (bloque is BloqueVariable {EsPersistente: true} var)
				{
					//Si la variable no existe en la lista actual de variables persistentes...
					if (!idsVariablesPersistentesExistentes.Remove(var.IDBloque))
					{
						//Creamos un modelo para la variable
						var modeloVariable = ControladorVariableFuncionBase.CrearModeloCorrespondiente(var.tipo, var.IDBloque, var.nombre);

						var nuevaVariablePersistente = new TIFuncionVariable
						{
							Variable = modeloVariable,
							Funcion = modelo
						};

						//La añadimos al modelo
						modelo.VariablesPersistentes.Add(nuevaVariablePersistente);

						//Creamos el controlador y lo añadimos a la lista de variables persistenes
						mVariablesPersistenes.Add(var.IDBloque, ControladorVariableFuncionBase.CrearControladorCorrespondiente(modeloVariable));
					}
					//Si existe solamente la actualizamos
					else
					{
						var variablePersistente = mVariablesPersistenes[var.IDBloque];

						variablePersistente.NombreVariable = var.nombre;
						variablePersistente.TipoVariable = var.tipo;
					}
				}
			}

			//Quitamos las relaciones del modelo con variables que ya no se encuentran en la funcion
			modelo.VariablesPersistentes.RemoveAll(var =>
			{
				for (int i = 0; i < idsVariablesPersistentesExistentes.Count; ++i)
				{
					if (var.Variable.IDVariable == idsVariablesPersistentesExistentes[i])
					{
						idsVariablesPersistentesExistentes.RemoveAt(i);

						return true;
					}
				}

				return false;
			});

			GuardarXML();
		}

		/// <summary>
		/// Version asincronica de <see cref="ActualizarBloques"/>
		/// </summary>
		/// <param name="nuevosBloques">Lista con los bloques actuales</param>
		public async Task ActualizarBloquesAsync(List<BloqueBase> nuevosBloques)
		{
			await Task.Run(() =>
			{
				ActualizarBloques(nuevosBloques);
			});
		}

		/// <summary>
		/// Guarda la funcion a un archivo XML
		/// </summary>
		private void GuardarXML()
		{
			try
			{
				XmlWriterSettings config = new XmlWriterSettings
				{ Encoding = Encoding.UTF8, Indent = true, NewLineOnAttributes = true };

				using XmlWriter writer =
					XmlWriter.Create(
						File.Open(
							Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones,
								NombreArchivoFuncion), FileMode.Create), config);

				writer.WriteStartDocument();
				writer.WriteStartElement("Cuerpo");

				writer.WriteAttributeString("NumeroDeBloques", Bloques.Count.ToString());

				foreach (var bloque in Bloques)
				{
					writer.WriteStartElement("Bloque");

					bloque.ConvertirHaciaXML(writer);

					writer.WriteEndElement();
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"Error al guardar funcion {NombreCompletoArchivoFuncion}{Environment.NewLine}Excepcion:{ex.Message}");
			}

			if (modelo.Id == 0)
				SistemaPrincipal.GuardarModelo(modelo);
		}

		[IndexerName("VariablesPersistentes")]
		public object this[int idVariable]
		{
			get
			{
				if (mVariablesPersistenes.ContainsKey(idVariable))
					return mVariablesPersistenes[idVariable].ObtenerValorVariable();

				SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener una variable con id: {idVariable}, pero no se encuentra en {nameof(mVariablesPersistenes)}", ESeveridad.Error);

				return null;
			}

			set
			{
				if (mVariablesPersistenes.ContainsKey(idVariable))
					mVariablesPersistenes[idVariable].GuardarValorVariable(value);

				SistemaPrincipal.LoggerGlobal.Log($@"Se intento actualizar el valor de una variable con id {idVariable} pero no se hallo.
														{Environment.NewLine}{this}");
			}
		}

		public override void Eliminar()
		{
			//Comprobamos que el archivo xml de la funcion exista
			if (File.Exists(NombreCompletoArchivoFuncion))
			{
				//Intentamos eliminarlo
				try
				{
					File.Delete(NombreCompletoArchivoFuncion);
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.LogCrash($"Error al intentar eliminar {NombreCompletoArchivoFuncion}{Environment.NewLine}Excepcion:{ex.Message}");
				}
			}

			//Eliminamos el modelo de la base de datos
			SistemaPrincipal.EliminarModelo(modelo);

			modelo = null;
		}

		public override string ToString()
		{
			return $"Controlador Funcion: {NombreFuncion}, {NombreArchivoFuncion})";
		}

		#endregion

		#region Metodos Estaticos

		/// <summary>
		/// Crea el <see cref="ControladorFuncionBase"/> para un <paramref name="tipoFuncion"/>
		/// </summary>
		/// <param name="modelo"><see cref="ModeloFuncion"/> para el que se creara el <see cref="ControladorFuncionBase"/></param>
		/// <param name="tipoFuncion"><see cref="ETipoFuncion"/></param>
		/// <returns><see cref="ControladorFuncionBase"/> para el <paramref name="modelo"/></returns>
		public static ControladorFuncionBase CrearControladorCorrespondiente(ModeloFuncion modelo, ETipoFuncion tipoFuncion)
		{
			switch (tipoFuncion)
			{
				case ETipoFuncion.Habilidad:
					return new ControladorFuncion_Habilidad(modelo);
				case ETipoFuncion.Efecto:
					return new ControladorFuncion_Efecto(modelo);
				case ETipoFuncion.Predicado:
					return new ControladorFuncion_Predicado(modelo);
				default:

					SistemaPrincipal.LoggerGlobal.Log($"{nameof(tipoFuncion)}({tipoFuncion}), valor no soportado!", ESeveridad.Error);

					return null;
			}
		}

		public static string ObtenerNombreArchivo(string nombreFuncion, int id) => $"{nombreFuncion}_{id}.xml";

		public static string ObtenerPathArchivoFuncion(string nombreArchivoFuncion)
		{
			return Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones, nombreArchivoFuncion);
		}

		#endregion
	}

	public abstract class ControladorFuncion<TipoFuncion> : ControladorFuncionBase
	{
		public TipoFuncion funcion;

		public ControladorFuncion(ModeloFuncion _modelo)
			: base(_modelo)
		{

		}
	}

	public class ControladorFuncion_Efecto : ControladorFuncion<Func<ControladorEfecto, ControladorPersonaje, List<ControladorPersonaje>, ControladorFuncionBase, bool>>
	{
		public ControladorFuncion_Efecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }


	}

	public class ControladorFuncion_Habilidad : ControladorFuncion<Action<ControladorFuncionBase, ControladorPersonaje, List<ControladorPersonaje>, List<object>>>
	{
		public ControladorFuncion_Habilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{}


	}

	public class ControladorFuncion_Predicado : ControladorFuncion<Func<ControladorEfecto, ControladorPersonaje, List<ControladorPersonaje>, ControladorFuncionBase, bool>>
	{
		public ControladorFuncion_Predicado(ModeloFuncion _modelo)
			: base(_modelo)
		{ }


	}
}