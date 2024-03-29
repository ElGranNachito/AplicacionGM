﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
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
		/// Lock para acceder a <see cref="Bloques"/>
		/// </summary>
		private object LockListaBloques = new object();

		//-------------------------------PROPIEDADES-------------------------------------

		/// <summary>
		/// <see cref="List{T}"/> que contiene todos los <see cref="BloqueBase"/> que componen esta funcion
		/// </summary>
		public virtual List<BloqueBase> Bloques { get; protected set; } = new List<BloqueBase>();

		/// <summary>
		/// Nombre de la funcion incluyendo su extension
		/// </summary>
		public string NombreArchivoFuncion => string.Intern($"{modelo.NombreFuncion}_{modelo.IDFuncion}.xml");

		/// <summary>
		/// Ruta completa al archivo de la funcion
		/// </summary>
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

		/// <summary>
		/// Obtiene el <see cref="ControladorBase"/> del modelo que contiene esta funcion
		/// </summary>
		public ControladorBase ContenedorFuncion
		{
			get
			{
				var controladorHallado = SistemaPrincipal.ObtenerControlador(modelo.ObtenerModeloContenedor());

				if (controladorHallado != null)
					return controladorHallado;

				SistemaPrincipal.LoggerGlobal.Log($"No se pudo hallar el contenedor de {this}", ESeveridad.Error);

				return null;
			}
		}

		#endregion

		#region Constructor

		public ControladorFuncionBase(ModeloFuncion _modelo)
			: base(_modelo)
		{
			CargarVariablesYTiradas();
		} 

		#endregion

		#region Metodos

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
			Bloques ??= new List<BloqueBase>();

			Bloques.Clear();

			Bloques.AddRange(nuevosBloques);

			Bloques.TrimExcess();

			List<int> idsVariablesPersistentesExistentes =
				modelo.Variables.Select(var => var.IDVariable).ToList();

			foreach (var bloque in nuevosBloques)
			{
				if (bloque is BloqueVariable {EsPersistente: true} var)
				{
					//Si la variable no existe en la lista actual de variables persistentes...
					if (!idsVariablesPersistentesExistentes.Remove(var.IDBloque))
					{
						//Creamos un modelo para la variable
						var modeloVariable = ControladorVariableBase.CrearModeloCorrespondiente(var.tipo, var.IDBloque, var.nombre);

						//La añadimos al modelo
						modelo.Variables.Add(modeloVariable);

						//Creamos el controlador y lo añadimos a la lista de variables persistenes
						mVariablesPersistenes.Add(var.IDBloque, ControladorVariableBase.CrearControladorCorrespondiente(modeloVariable));
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
			modelo.Variables.RemoveAll(var =>
			{
				for (int i = 0; i < idsVariablesPersistentesExistentes.Count; ++i)
				{
					if (var.IDVariable == idsVariablesPersistentesExistentes[i])
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

		public override async Task Recargar()
		{
			await base.Recargar();

			if (!File.Exists(ObtenerPathArchivoFuncion(ObtenerNombreArchivo(modelo.NombreFuncion, modelo.IDFuncion))))
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"Se intento cambiar el archivo de origen de {this} pero el nuevo archivo {ObtenerNombreArchivo(modelo.NombreFuncion, modelo.Id)} no existe!");
			}

			//Obtenemos las variables persistentes del nuevo modelo y las del modelo actual
			var variablesPersistentesNuevoModelo = modelo.Variables;
			var variablesPersistentesModeloActual = mVariablesPersistenes.Values.ToList();

			//Quitamos de la lista de variables de ambos modelos las que se encuentren repetidas en ambos
			variablesPersistentesModeloActual.RemoveAll(varModeloActual =>
			{
				foreach (var varNuevoModelo in variablesPersistentesNuevoModelo)
				{
					if (varNuevoModelo == varModeloActual.modelo)
					{
						variablesPersistentesNuevoModelo.Remove(varNuevoModelo);

						return true;
					}
				}

				return false;
			});

			//Eliminamos las variablesque quedaron en la lista del modelo actual
			variablesPersistentesModeloActual.ForEach(var =>
			{
				mVariablesPersistenes.Remove(var.IDVariable);

				modelo.Variables.RemoveAll(v => v.Id == var.modelo.Id);

				var.Eliminar();
			});

			//Añadimos al modelo las variables que quedaron en el nuevo modelo
			variablesPersistentesNuevoModelo.ForEach(var =>
			{
				modelo.Variables.Add(var);

				mVariablesPersistenes.Add(var.IDVariable, ControladorVariableBase.CrearControladorCorrespondiente(var));
			});

			SistemaPrincipal.LoggerGlobal.Log($@"Actualizado modelo para {this}. 
													Variables persistentes eliminadas: {variablesPersistentesModeloActual.Count}. 
													Variables persistentes nuevas: {variablesPersistentesNuevoModelo.Count}", ESeveridad.Debug);
		}

		/// <summary>
		/// Cuando se lo sobreescribe en una clase derivada intenta compilar la funcion asincronicamente
		/// </summary>
		/// <returns><see cref="Task"/> de compilacion</returns>
		public virtual async Task CompilarAsync() => await Task.FromResult(true);

		public override ControladorVariableBase ObtenerControladorVariable(int idVariable)
		{
			if (mVariablesPersistenes.ContainsKey(idVariable))
				return mVariablesPersistenes[idVariable];
			
			//Si la variable no se encuentra en esta funcion entonces intentamos obtenerla a traves de su contenedor
			if (ContenedorFuncion.ObtenerControladorVariable(idVariable) is { } controladorVar)
				return controladorVar;

			return null;
		}

		public override void Eliminar(bool mostrarMensajeConfirmacion = false)
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

			base.Eliminar();

			//Eliminamos el modelo de la base de datos
			SistemaPrincipal.EliminarModelo(modelo);

			modelo = null;
		}

		public override string ToString()
		{
			return $"Controlador Funcion: {NombreFuncion}, {NombreArchivoFuncion})";
		}

		/// <summary>
		/// Inicializa un <see cref="ViewModelCreacionDeFuncionBase"/> del tipo correcto para editar esta funcion
		/// </summary>
		/// <param name="accionSalir">Contiene el delegado que se ejecutara al salir de la edicion</param>
		/// <returns>Instancia de <see cref="ViewModelCreacionDeFuncionBase"/></returns>
		public abstract ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir);

		/// <summary>
		/// Inicializa un <see cref="ViewModelCreacionDeFuncionBase"/> del tipo correcto para crear una funcion del <see cref="tipoFuncion"/>
		/// </summary>
		/// <param name="tipoFuncion">Tipo del controlador de funcion para el que crear el vm</param>
		/// <param name="accionSalir">Contiene el delegado que se ejecutara al salir de la edicion</param>
		/// <param name="tipoDelegado">En caso de que la funcion sea un handler, este debera ser el tipo del delegado para el que se queire crear</param>
		/// <returns>Instancia de <see cref="ViewModelCreacionDeFuncionBase"/></returns>
		public static ViewModelCreacionDeFuncionBase CrearVMParaCrear(
			Type tipoFuncion,
			Action<ViewModelCreacionDeFuncionBase> accionSalir,
			Type tipoDelegado = null)
		{
			//TODO: Terminar

			if (tipoFuncion == typeof(ControladorFuncion_Habilidad))
			{
				return new ViewModelCreacionDeFuncionHabilidad(accionSalir);
			}
			else if (tipoFuncion == typeof(ControladorFuncion_Efecto))
			{
				return null;
			}
			else if (tipoFuncion == typeof(ControladorFuncion_PredicadoHabilidad))
			{
				return null;
			}
			else if (tipoFuncion == typeof(ControladorFuncion_PredicadoEfecto))
			{
				return null;
			}
			else if (tipoFuncion == typeof(ControladorFuncion_HandlerEvento))
			{
				return new ViewModelCreacionDeFuncionHandlerEvento(accionSalir, tipoDelegado);
			}

			return null;
		}

		#endregion

		#region Metodos Estaticos

		/// <summary>
		/// Crea el <see cref="ControladorFuncionBase"/> para un <paramref name="propositoFuncion"/>
		/// </summary>
		/// <param name="modelo"><see cref="ModeloFuncion"/> para el que se creara el <see cref="ControladorFuncionBase"/></param>
		/// <param name="propositoFuncion"><see cref="EPropositoFuncion"/></param>
		/// <returns><see cref="ControladorFuncionBase"/> para el <paramref name="modelo"/></returns>
		public static ControladorFuncionBase CrearControladorCorrespondiente(ModeloFuncion modelo, EPropositoFuncion propositoFuncion)
		{
			switch (propositoFuncion)
			{
				case EPropositoFuncion.Habilidad:
					return new ControladorFuncion_Habilidad(modelo);
				case EPropositoFuncion.Efecto:
					return new ControladorFuncion_Efecto(modelo);
				case EPropositoFuncion.PredicadoEfecto:
					return new ControladorFuncion_PredicadoEfecto(modelo);
				case EPropositoFuncion.UsoItem:
					return new ControladorFuncion_Item(modelo);
				default:
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(propositoFuncion)}({propositoFuncion}), valor no soportado!", ESeveridad.Error);

					return null;
			}
		}

		/// <summary>
		/// Obtiene el nombre de archivo que tendria una funcion con determinado <paramref name="nombreFuncion"/> e <paramref name="id"/>
		/// </summary>
		/// <param name="nombreFuncion">Nombre de la funcion</param>
		/// <param name="id">Id de la funcion</param>
		/// <returns>Nombre del archivo que tendria la funcion</returns>
		public static string ObtenerNombreArchivo(string nombreFuncion, int id) => $"{nombreFuncion}_{id}.xml";

		/// <summary>
		/// Obtiene la ruta a un archivo de funcion a partir del <paramref name="nombreArchivoFuncion"/>
		/// </summary>
		/// <param name="nombreArchivoFuncion">Nombre del archivo de la funcion</param>
		/// <returns>Ruta completa al archivo</returns>
		public static string ObtenerPathArchivoFuncion(string nombreArchivoFuncion)
		{
			return Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones, nombreArchivoFuncion);
		}

		#endregion
	}
}