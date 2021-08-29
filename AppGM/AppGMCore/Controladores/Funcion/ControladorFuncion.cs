using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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
		public ControladorBase ContenedorFuncion => SistemaPrincipal.ObtenerControlador(modelo.ContenedorFuncion.Contenedor);

		#endregion

		#region Constructor

		public ControladorFuncionBase(ModeloFuncion _modelo)
			: base(_modelo)
		{
			CargarVariablesYTiradas();
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
				modelo.Variables.Select(var => var.Variable.IDVariable).ToList();

			foreach (var bloque in nuevosBloques)
			{
				if (bloque is BloqueVariable {EsPersistente: true} var)
				{
					//Si la variable no existe en la lista actual de variables persistentes...
					if (!idsVariablesPersistentesExistentes.Remove(var.IDBloque))
					{
						//Creamos un modelo para la variable
						var modeloVariable = ControladorVariableBase.CrearModeloCorrespondiente(var.tipo, var.IDBloque, var.nombre);

						var nuevaVariablePersistente = new TIVarible
						{
							Variable            = modeloVariable,
							ModeloContenedorVar = modelo,
						};

						//La añadimos al modelo
						modelo.Variables.Add(nuevaVariablePersistente);

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

		public override void ActulizarModelo(ModeloFuncion nuevoModelo, bool eliminarSiNuevoModeloEsNull = false)
		{
			base.ActulizarModelo(nuevoModelo, true);

			//Si el nuevo modelo es null nos pegamos media vuelta porque ya se debio haber llamado Eliminar desde base.ActualizarModelo
			if (nuevoModelo == null)
				return;

			if (nuevoModelo.NombreFuncion != NombreFuncion && File.Exists(ObtenerPathArchivoFuncion(ObtenerNombreArchivo(nuevoModelo.NombreFuncion, nuevoModelo.IDFuncion))))
			{
				//Cambiamos el valor directamente al modelo para que no se intente mover el archivo desde la propiedad
				modelo.NombreFuncion = nuevoModelo.NombreFuncion;
			}
			else
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"Se intento cambiar el archivo de origen de {this} pero el nuevo archivo {ObtenerNombreArchivo(nuevoModelo.NombreFuncion, nuevoModelo.Id)} no existe!");
			}

			//Obtenemos las variables persistentes del nuevo modelo y las del modelo actual
			var variablesPersistentesNuevoModelo = nuevoModelo.Variables.Select(ti => ti.Variable).ToList();
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

				modelo.Variables.RemoveAll(ti => ti.IdVariable == var.modelo.Id);

				var.Eliminar();
			});

			//Añadimos al modelo las variables que quedaron en el nuevo modelo
			variablesPersistentesNuevoModelo.ForEach(var =>
			{
				modelo.Variables.Add(new TIVarible
				{
					ModeloContenedorVar = modelo,
					Variable            = var
				});

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

	/// <summary>
	/// Controlador que contiene una funcion de tipo conocido
	/// </summary>
	/// <typeparam name="TFuncion">Tipo de la funcion contenida</typeparam>
	public abstract class ControladorFuncion<TFuncion> : ControladorFuncionBase
	{
		#region Propiedades

		/// <summary>
		/// Funcion compilada y lista para utilizar
		/// </summary>
		[MaybeNull]
		public TFuncion Funcion
		{
			get
			{
				if (mFuncionesConocidas.ContainsKey(NombreArchivoFuncion))
					return mFuncionesConocidas[NombreArchivoFuncion].funcion;

				return default;
			}
		}

		/// <summary>
		/// Ultimo resultado de intentar compilar la funcion
		/// </summary>
		[MaybeNull]
		public ResultadoCompilacion<TFuncion> ResultadoCompilacion { get; private set; }

		/// <summary>
		/// Obtiene o establece los bloques de la funcion
		/// </summary>
		[MaybeNull]
		public override List<BloqueBase> Bloques
		{
			get
			{
				if (mFuncionesConocidas.ContainsKey(NombreArchivoFuncion))
					return mFuncionesConocidas[NombreArchivoFuncion].bloques;

				return null;
			}

			protected set => mFuncionesConocidas[NombreArchivoFuncion].bloques = value;
		}

		/// <summary>
		/// <para>
		///		Relacion un path a un archivo xml que representa una funcion con la lambda compilada y lista de bloques que la representa.
		/// </para>
		///
		/// <para>
		///		El principal proposito de esta variable es para que si se crean varios controladores de una misma funcion porque esta tiene variables
		///		estaticas y por lo tanto puede tener varias instancias, no necesitemos cargar el xml y compilar los bloques para cada una.
		/// </para>
		/// </summary>
		internal static Dictionary<string, FuncionCargada<TFuncion>> mFuncionesConocidas = new Dictionary<string, FuncionCargada<TFuncion>>();
		
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelo"></param>
		public ControladorFuncion(ModeloFuncion _modelo)
			: base(_modelo){} 

		#endregion

		#region Metodos

		public override async Task CompilarAsync()
		{
			if (mFuncionesConocidas.ContainsKey(NombreArchivoFuncion))
				return;

			var compilador = new Compilador(Bloques);

			ResultadoCompilacion = await Task.Run(() => compilador.Compilar<TFuncion>());

			mFuncionesConocidas[NombreArchivoFuncion].funcion = ResultadoCompilacion.Funcion;
		} 

		#endregion
	}

	/// <summary>
	/// <para>
	///		Representa una funcion cuya existencia es conocida y como minimo se han cargado sus bloques
	/// </para>
	/// <para>
	///		Esta clase existe porque no se pueden modificar los valores de una tupla desde un diccionario
	/// </para>
	/// </summary>
	/// <typeparam name="TFuncion">Tipo de la funcion</typeparam>
	internal class FuncionCargada<TFuncion>
	{
		/// <summary>
		/// Lambda compilada
		/// </summary>
		public TFuncion funcion;

		/// <summary>
		/// Bloques que conforman la funcion
		/// </summary>
		public List<BloqueBase> bloques;
	}

	#region Implementaciones especificas del controlador de funcion

	/// <summary>
	/// Controlador para una funcion de una habilidad
	/// </summary>
	public class ControladorFuncion_Habilidad : ControladorFuncion<Action<ControladorHabilidad, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ControladorFuncion_Habilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			return new ViewModelCreacionDeFuncionHabilidad(accionSalir, this);
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="controladorAplicacionEfecto">Controlador de la aplicacion del efecto que llama esta funcion</param>
		/// <param name="controladorEfecto">Controlador del efecto</param>
		/// <param name="instigador">Personaje responsable de aplicar el efecto</param>
		/// <param name="objetivo">Personaje al que se le aplica el efecto</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns><see cref="bool"/> indicando si se la funcion se ejecuto con extio</returns>
		public bool EjecutarFuncion(
			ControladorHabilidad controladorHabilidad,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			try
			{
				Funcion(controladorHabilidad, instigador, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return false;
			}

			return true;
		}
	}

	/// <summary>
	/// Controlador para una funcion de un efecto
	/// </summary>
	public class ControladorFuncion_Efecto : ControladorFuncion<Action<ControladorEfectoSiendoAplicado, ControladorEfecto, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ControladorFuncion_Efecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente
		/// </summary>
		/// <param name="controladorAplicacionEfecto">Controlador de la aplicacion del efecto que llama esta funcion</param>
		/// <param name="controladorEfecto">Controlador del efecto</param>
		/// <param name="instigador">Personaje responsable de aplicar el efecto</param>
		/// <param name="objetivo">Personaje al que se le aplica el efecto</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns><see cref="bool"/> indicando si se la funcion se ejecuto con extio</returns>
		public bool EjecutarFuncion(
			ControladorEfectoSiendoAplicado controladorAplicacionEfecto,
			ControladorEfecto controladorEfecto,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			try
			{
				Funcion(controladorAplicacionEfecto, controladorEfecto, instigador, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return false;
			}

			return true;
		}
	}

	/// <summary>
	/// Controlador para un predicado de un efecto
	/// </summary>
	public class ControladorFuncion_PredicadoEfecto : ControladorFuncion<Func<ControladorEfectoSiendoAplicado, ControladorEfecto, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>>
	{
		public ControladorFuncion_PredicadoEfecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="controladorAplicacionEfecto">Controlador de la aplicacion del efecto que llama esta funcion</param>
		/// <param name="controladorEfecto">Controlador del efecto</param>
		/// <param name="instigador">Personaje responsable de aplicar el efecto</param>
		/// <param name="objetivo">Personaje al que se le aplica el efecto</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns>Tupla con dos <see cref="bool"/>, el primero indica si se pudo ejecutar la funcion en su totalidad y el segundo contiene su resultado</returns>
		public (bool funcionEjecutadaConExito, bool resultadoFuncion) EjecutarFuncion(
			ControladorEfectoSiendoAplicado controladorAplicacionEfecto,
			ControladorEfecto controladorEfecto,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			bool res = false;

			try
			{
				res = Funcion(controladorAplicacionEfecto, controladorEfecto, instigador, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return (false, res);
			}

			return (true, res);
		}
	}

	/// <summary>
	/// Controlador para un predicado de un efecto
	/// </summary>
	public class ControladorFuncion_PredicadoHabilidad : ControladorFuncion<Func<ControladorHabilidad, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>>
	{
		public ControladorFuncion_PredicadoHabilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="controladorHabilidad">Controlador de la habilidad que llama esta funcion</param>
		/// <param name="instigador">Personaje que utilizo la habilidad</param>
		/// <param name="objetivo">Personaje objetivo de la habilidad</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns>Tupla con dos <see cref="bool"/>, el primero indica si se pudo ejecutar la funcion en su totalidad y el segundo contiene su resultado</returns>
		public (bool funcionEjecutadaConExito, bool resultadoFuncion) EjecutarFuncion(
			ControladorHabilidad controladorHabilidad,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			bool res = false;

			try
			{
				res = Funcion(controladorHabilidad, instigador, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return (false, res);
			}

			return (true, res);
		}
	}
	#endregion
}