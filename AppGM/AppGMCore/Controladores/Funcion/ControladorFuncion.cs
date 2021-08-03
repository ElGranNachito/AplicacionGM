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
		/// <summary>
		/// Contiene todas los <see cref="ControladorVariableFuncionBase"/> de las variables persistentes de esta funcion
		/// </summary>
		private Dictionary<int, ControladorVariableFuncionBase> mVariablesPersistenes;

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

		public ControladorFuncionBase(ModeloFuncion _modelo)
			: base(_modelo)
		{
			//Si no hay variables persistentes creamos una lista nueva
			modelo.VariablesPersistentes ??= new List<TIFuncionVariable>();

			mVariablesPersistenes = new Dictionary<int, ControladorVariableFuncionBase>(modelo.VariablesPersistentes.Select(var =>
			{
				Type tipoVariable = null;

				try
				{
					tipoVariable = Type.GetType(var.Variable.TipoVariable);
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log(
						@$"Error al intentar obtener tipo de variable. IDFuncion: {var.Funcion.Id} - IDVariable: {var.Variable.Id} - TipoVariable: {var.Variable.TipoVariable}
															{Environment.NewLine} Excepcion: {ex.Message}.");

					return new KeyValuePair<int, ControladorVariableFuncionBase>();
				}

				if (tipoVariable == typeof(int))
					return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, new ControladorVariableFuncion_Int(var.Variable));
				else if (tipoVariable == typeof(float))
					return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, new ControladorVariableFuncion_Float(var.Variable));
				else if (tipoVariable == typeof(string))
					return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, new ControladorVariableFuncion_String(var.Variable));

				SistemaPrincipal.LoggerGlobal.Log($"{tipoVariable} no soportado!", ESeveridad.Error);

				return new KeyValuePair<int, ControladorVariableFuncionBase>();
			}));
		}

		/// <summary>
		/// Carga los <see cref="Bloques"/>
		/// </summary>
		/// <returns><see cref="bool"/> indicando si se pudieron cargar los bloques</returns>
		public bool CargarBloques()
		{
			SistemaPrincipal.LoggerGlobal.Log($"Cargando bloques para funcion {NombreFuncion}", ESeveridad.Info);

			Bloques = BloqueBase.DesdeXmlMultiple(NombreCompletoArchivoFuncion);

			return Bloques != null;
		}

		/// <summary>
		/// Ejecuta <see cref="CargarBloques"/> en otro hilo
		/// </summary>
		/// <returns>Resultado de <see cref="CargarBloques"/></returns>
		public async Task<bool> CargarBloquesAsync()
		{
			return await Task.Run(CargarBloques);
		}

		public void ActualizarBloques(List<BloqueBase> nuevosBloques)
		{
			Bloques.Clear();

			Bloques.AddRange(nuevosBloques);

			Bloques.TrimExcess();

			List<int> idsVariablesPersistentesExistentes = 
				modelo.VariablesPersistentes.Select(var => var.IDVariable).ToList();

			foreach (var bloque in nuevosBloques)
			{
				if (bloque is BloqueVariable var)
				{
					if (!idsVariablesPersistentesExistentes.Remove(var.IDBloque))
					{
						ModeloVariableFuncionBase modeloVariable = null;

						if (var.tipo == typeof(int))
							modeloVariable = new ModeloVariableFuncion_Int{IDVariable = var.IDBloque, NombreVariable = var.nombre};
						else if (var.tipo == typeof(float))
							modeloVariable = new ModeloVariableFuncion_Float {IDVariable = var.IDBloque, NombreVariable = var.nombre };
						else if (var.tipo == typeof(string))
							modeloVariable = new ModeloVariableFuncion_String {IDVariable = var.IDBloque, NombreVariable = var.nombre };
						else
						{
							SistemaPrincipal.LoggerGlobal.Log($"No se pudo crear modelo para variable persistente ({var})", ESeveridad.Error);

							continue;
						}

						var tiFuncionVariable = new TIFuncionVariable {Funcion = modelo, Variable = modeloVariable};

						modelo.VariablesPersistentes.Add(tiFuncionVariable);
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

		public void GuardarXML()
		{
			try
			{

				XmlWriterSettings config = new XmlWriterSettings
					{Encoding = Encoding.UTF8, Indent = true, NewLineOnAttributes = true};

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

			if(modelo.Id == 0)
				SistemaPrincipal.GuardarModelo(modelo);
		}

		public async Task GuardarXMLAsync()
		{
			await Task.Run(GuardarXML);
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
				//TODO: Implementar
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
				catch(Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.LogCrash($"Error al intentar eliminar {NombreCompletoArchivoFuncion}{Environment.NewLine}Excepcion:{ex.Message}");
				}
			}

			//Eliminamos el modelo de la base de datos
			SistemaPrincipal.EliminarModelo(modelo);

			modelo = null;
		}

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

	public class ControladorFuncion_Habilidad : ControladorFuncion<Action<ControladorPersonaje, List<ControladorPersonaje>, List<object>>>
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