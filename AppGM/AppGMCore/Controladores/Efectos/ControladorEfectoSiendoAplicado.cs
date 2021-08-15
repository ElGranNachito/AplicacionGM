using System;
using System.Reflection;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador de <see cref="ModeloEfectoSiendoAplicado"/>
	/// </summary>
	public class ControladorEfectoSiendoAplicado : ControladorEfectoBase<ModeloEfectoSiendoAplicado>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando se deja de aplicar este efecto a su objetivo
		/// </summary>
		public event Action<ControladorEfectoSiendoAplicado> OnEfectoQuitado = delegate{}; 

		#endregion

		#region Campos & Propiedades

		//---------------------------------------CAMPOS--------------------------------------

		/// <summary>
		/// Controlador del predicado que indica si este efecto puede ser aplicado
		/// </summary>
		private ControladorFuncion_Predicado mFnPuedeAplicarEfecto;

		/// <summary>
		/// Controlador de la funcion que aplica este efecto
		/// </summary>
		private ControladorFuncion_Efecto mFnAplicarEfecto;

		/// <summary>
		/// Controlador de la funcion que quita este efecto
		/// </summary>
		private ControladorFuncion_Efecto mFnQuitarEfecto;

		/// <summary>
		/// Personaje que aplico el efecto
		/// </summary>
		public readonly ControladorPersonaje instigador;

		/// <summary>
		/// Personajes a quienes se les esta aplicando el efecto
		/// </summary>
		public readonly ControladorPersonaje objetivo;

		/// <summary>
		/// Controlador del efecto
		/// </summary>
		public readonly ControladorEfecto controladorEfecto;


		//-------------------------------------PROPIEDADES----------------------------------

		/// <summary>
		/// Obtiene el predicado desde el <see cref="controladorEfecto"/> o desde <see cref="mFnPuedeAplicarEfecto"/>
		/// en caso de que no sea null
		/// </summary>
		public override ControladorFuncion_Predicado FnPuedeAplicarEfecto
		{
			get => mFnPuedeAplicarEfecto ?? controladorEfecto.FnPuedeAplicarEfecto;
			protected set => mFnPuedeAplicarEfecto = value;
		}

		/// <summary>
		/// Obtiene la funcion desde el <see cref="controladorEfecto"/> o desde <see cref="mFnAplicarEfecto"/>
		/// en caso de que no sea null
		/// </summary>
		public override ControladorFuncion_Efecto FnAplicarEfecto
		{
			get => mFnAplicarEfecto ?? controladorEfecto.FnAplicarEfecto;
			protected set => mFnAplicarEfecto = value;
		}

		/// <summary>
		/// Obtiene la funcion desde el <see cref="controladorEfecto"/> o desde <see cref="mFnQuitarEfecto"/>
		/// en caso de que no sea null
		/// </summary>
		public override ControladorFuncion_Efecto FnQuitarEfecto
		{
			get => mFnQuitarEfecto ?? controladorEfecto.FnQuitarEfecto;
			protected set => mFnQuitarEfecto = value;
		}

		/// <summary>
		/// <see cref="ModeloEfecto"/> que representa este <see cref="ModeloEfectoSiendoAplicado"/>
		/// </summary>
		public ModeloEfecto Efecto => controladorEfecto.modelo;

		/// <summary>
		/// <see cref="ModeloEfectoSiendoAplicado"/> que este controlador contiene
		/// </summary>
		public ModeloEfectoSiendoAplicado AplicacionEfecto => modelo;
			
		/// <summary>
		/// Obtiene el valor de <see cref="ModeloEfecto.TurnosDeDuracion"/>
		/// </summary>
		[AccesibleEnGuraScratch(nameof(TurnosDeDuracion))]
		public int TurnosDeDuracion => modelo.Efecto.Efecto.TurnosDeDuracion;

		/// <summary>
		/// Obtiene o establece el valor de <see cref="ModeloEfectoSiendoAplicado.TurnosRestantes"/>
		/// </summary>
		[AccesibleEnGuraScratch(nameof(TurnosRestantes))]
		public int TurnosRestantes
		{
			get => modelo.TurnosRestantes;
			set => modelo.TurnosRestantes = value;
		}

		/// <summary>
		/// Obtiene o establece el valor de <see cref="ModeloEfectoSiendoAplicado.ContadorAcumulaciones"/>
		/// </summary>
		[AccesibleEnGuraScratch(nameof(ContadorAcumulaciones))]
		public int ContadorAcumulaciones
		{
			get => modelo.ContadorAcumulaciones;
			set => modelo.ContadorAcumulaciones = value;
		}

		/// <summary>
		/// Obtiene o establece el valor de <see cref="ModeloEfectoSiendoAplicado.ComportamientoAcumulativo"/>
		/// </summary>
		public EComportamientoAcumulativo ComportamientoAcumulativo
		{
			get => modelo.ComportamientoAcumulativo;
			set => ModificarComportamientoAcumulativo(value, EModoDeCambioDeComportamientoAcumulativo.SumarTurnosRestantes);
		}

		/// <summary>
		/// Numero de veces que se ha aplicado este efecto sobre el personaje
		/// </summary>
		[AccesibleEnGuraScratch(nameof(ContadorIteraciones))]
		public int ContadorIteraciones { get; private set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Inicializa esta instancia a partir de un nuevo <see cref="ModeloEfectoSiendoAplicado"/> creado a partir de <paramref name="_controladorEfecto"/>
		/// </summary>
		/// <param name="_controladorEfecto">Controlador del <see cref="ModeloEfecto"/> que se aplicara</param>
		public ControladorEfectoSiendoAplicado(ControladorEfecto _controladorEfecto, ControladorPersonaje _instigador, ControladorPersonaje _objetivo)
			: base(new ModeloEfectoSiendoAplicado())
		{
			controladorEfecto = _controladorEfecto;
			instigador        = _instigador;
			objetivo          = _objetivo;

			modelo.Efecto = new TIEfectoSiendoAplicadoEfecto
			{
				Efecto = controladorEfecto.modelo,
				EfectoAplicandose = modelo
			};

			modelo.Instigador = new TIEfectoSiendoAplicadoPersonajeInstigador
			{
				EfectoAplicandose = modelo,
				PersonajeInstigador = instigador.modelo
			};

			modelo.Objetivo = new TIEfectoSiendoAplicadoPersonajeObjetivo
			{
				EfectoAplicandose = modelo,
				PersonajeObjetivo = objetivo.modelo
			};

			modelo.TurnosRestantes = controladorEfecto.modelo.TurnosDeDuracion;
			modelo.EstaSiendoAplicado = false;

			SistemaPrincipal.GuardarModelo(modelo);
			SistemaPrincipal.GuardarModelo(modelo.Efecto);
			SistemaPrincipal.GuardarModelo(modelo.Instigador);
			SistemaPrincipal.GuardarModelo(modelo.Objetivo);

			SistemaPrincipal.GuardarDatosRol();
		}

		/// <summary>
		/// Inicializa esta instancia a partir de un <see cref="ModeloEfectoSiendoAplicado"/> preexistente
		/// </summary>
		/// <param name="_efectoSiendoAplicado">Modelo preexistente</param>
		/// <param name="_controladorEfecto">Controlador del efecto que creo esta instancia</param>
		/// <param name="_instigador">Controlador del personaje que aplico este efecto en el <paramref name="_objetivo"/></param>
		/// <param name="_objetivo">Controlador del personaje al que se le aplicara este efecto</param>
		public ControladorEfectoSiendoAplicado(
			ModeloEfectoSiendoAplicado _efectoSiendoAplicado, 
			ControladorEfecto _controladorEfecto,
			ControladorPersonaje _instigador,
			ControladorPersonaje _objetivo)

			: base(_efectoSiendoAplicado)
		{
			controladorEfecto = _controladorEfecto;
			instigador        = _instigador;
			objetivo          = _objetivo;

			modelo.Funciones.ForEach(f =>
			{
				InicializarFuncion(f.ModeloFuncion, f.TipoFuncion);
			});
		} 

		#endregion

		#region Metodos

		public void AñadirAcumulacion()
		{
			//Si los efecto deben solapar entonces este metodo no debe hacer nada
			if (ComportamientoAcumulativo == EComportamientoAcumulativo.Solapar)
				return;

			if (ComportamientoAcumulativo == EComportamientoAcumulativo.SumarTurnos)
				TurnosRestantes += TurnosDeDuracion;

			ContadorAcumulaciones++;
		}

		/// <summary>
		/// Indica si este efecto puede ser aplicado
		/// </summary>
		/// <param name="objetivo">Objetivo sobre el cual revisar si se puede aplicar el efecto</param>
		/// <returns></returns>
		public bool PuedeAplicarEfecto(ControladorPersonaje objetivo)
		{
			return controladorEfecto.PuedeAplicarEfecto(instigador, objetivo);
		}

		/// <summary>
		/// Aplica el efecto sobre su <see cref="objetivo"/>
		/// </summary>
		public void AplicarEfecto()
		{
			//FnAplicarEfecto.Funcion()

			if (--TurnosRestantes <= 0)
				QuitarEfecto();
		}

		/// <summary>
		/// Quita el efecto de su <see cref="objetivo"/>
		/// </summary>
		public void QuitarEfecto()
		{
			//FnQuitarEfecto.Funcion

			OnEfectoQuitado(this);

			Eliminar();
		}

		/// <summary>
		/// Modifica el comportamiento acumulativo de esta aplicacion del efecto
		/// </summary>
		/// <param name="nuevoComportamiento">Nuevo comportamiento que tendra este efecto</param>
		/// <param name="modoDeCambio">Manera en la que se pasara al nuevo comportamiento</param>
		public override void ModificarComportamientoAcumulativo(
			EComportamientoAcumulativo nuevoComportamiento,
			EModoDeCambioDeComportamientoAcumulativo modoDeCambio)
		{
			if (nuevoComportamiento == ComportamientoAcumulativo)
				return;

			//TODO: Implementar

			switch (nuevoComportamiento)
			{

			}
		}

		public override void ActulizarModelo(ModeloEfectoSiendoAplicado nuevoModelo, bool eliminarSiNuevoModeloEsNull = false)
		{
			base.ActulizarModelo(modelo, eliminarSiNuevoModeloEsNull);

			var nuevoEfecto = nuevoModelo.Efecto.Efecto;

			ActualizarFuncion(
				GetType().GetProperty(nameof(FnPuedeAplicarEfecto), typeof(ControladorFuncion_Predicado)), 
				nuevoEfecto.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionPuedeAplicar)?.Funcion);

			ActualizarFuncion(
				GetType().GetProperty(nameof(FnAplicarEfecto), typeof(ControladorFuncion_Efecto)),
				nuevoEfecto.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionAplicar)?.Funcion);

			ActualizarFuncion(GetType().GetProperty(nameof(FnQuitarEfecto), typeof(ControladorFuncion_Efecto)),
				nuevoEfecto.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionQuitar)?.Funcion);

			ComportamientoAcumulativo = nuevoModelo.ComportamientoAcumulativo;
			TurnosRestantes = nuevoModelo.TurnosRestantes;
		}

		/// <summary>
		/// Actualiza el valor de las propiedades <see cref="FnPuedeAplicarEfecto"/>, <see cref="FnAplicarEfecto"/> o <see cref="FnQuitarEfecto"/>
		/// </summary>
		/// <param name="propertyFuncionActualizar">Propiedad cuyo valor actualizar</param>
		/// <param name="nuevaFuncion">Modelo de la nueva funcion</param>
		private void ActualizarFuncion(PropertyInfo propertyFuncionActualizar, ModeloFuncion nuevaFuncion)
		{
			if (!propertyFuncionActualizar.PropertyType.IsAssignableFrom(nuevaFuncion.GetType()))
			{
				SistemaPrincipal.LoggerGlobal.Log($"No se puede asignar a {propertyFuncionActualizar.Name} utilizando una variable de tipo {nuevaFuncion.GetType()}!", ESeveridad.Error);

				return;
			}

			var funcionActual = propertyFuncionActualizar.GetValue(this) as ControladorFuncionBase;

			//Si el modelo dela funcion actual es igual entonces no hacemos nada
			if (nuevaFuncion == funcionActual?.modelo)
				return;

			//Si la nueva funcion tiene variables persistentes y la funcion actual no existe...
			if (nuevaFuncion.VariablesPersistentes.Count > 0 && funcionActual == null)
			{
				//TODO: Compilar nueva funcion

				//Creamos el controlador para la nueva funcion y asignamos el valor a la propiedad
				propertyFuncionActualizar.SetValue(this, new ControladorFuncion_Efecto(nuevaFuncion));
			}
			//Si la nueva funcion no tiene variables persistentes y la funcion actual no es null...
			else if (nuevaFuncion.VariablesPersistentes.Count == 0 && funcionActual != null)
			{
				//Eliminamos la funcion actual
				funcionActual.Eliminar();
				propertyFuncionActualizar.SetValue(this, null);
			}
			//Si no se cumple nada de lo de arriba...
			else
			{
				//Actualizamos el modelo de la funcion actual si es que existe
				funcionActual?.ActulizarModelo(nuevaFuncion);
			}
		}

		/// <summary>
		/// Elimina el modelo y quita el efecto de su <see cref="objetivo"/>
		/// </summary>
		public override void Eliminar()
		{
			base.Eliminar();

			SistemaPrincipal.EliminarModelo(modelo);
		} 

		#endregion
	}
}