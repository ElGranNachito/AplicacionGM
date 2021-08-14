using System;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador de <see cref="ModeloEfectoSiendoAplicado"/>
	/// </summary>
	public class ControladorEfectoSiendoAplicado : ControladorEfectoBase<ModeloEfectoSiendoAplicado>
	{
		#region Eventos

		public event Action<ControladorEfectoSiendoAplicado> OnEfectoQuitado = delegate{}; 

		#endregion

		#region Campos & Propiedades

		//---------------------------------------CAMPOS--------------------------------------

		private ControladorFuncion_Predicado mFnPuedeAplicarEfecto;

		private ControladorFuncion_Efecto mFnAplicarEfecto;

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

		public override ControladorFuncion_Predicado FnPuedeAplicarEfecto
		{
			get => mFnPuedeAplicarEfecto ?? controladorEfecto.FnPuedeAplicarEfecto;
			protected set => mFnPuedeAplicarEfecto = value;
		}

		public override ControladorFuncion_Efecto FnAplicarEfecto
		{
			get => mFnAplicarEfecto ?? controladorEfecto.FnAplicarEfecto;
			protected set => mFnAplicarEfecto = value;
		}

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
			
		public int TurnosDeDuracion => modelo.Efecto.Efecto.TurnosDeDuracion;

		public int TurnosRestantes
		{
			get => modelo.TurnosRestantes;
			set => modelo.TurnosRestantes = value;
		}

		public int ContadorAcumulaciones
		{
			get => modelo.ContadorAcumulaciones;
			set => modelo.ContadorAcumulaciones = value;
		}

		public EComportamientoAcumulativo ComportamientoAcumulativo
		{
			get => modelo.ComportamientoAcumulativo;
			set => ModificarComportamientoAcumulativo(value, EModoDeCambioDeComportamiento.SumarTurnosRestantes);
		}

		#endregion

		/// <summary>
		/// Inicializa esta instancia a partir de un nuevo <see cref="ModeloEfectoSiendoAplicado"/> creado a partir de <paramref name="_controladorEfecto"/>
		/// </summary>
		/// <param name="efecto">Controlador del <see cref="ModeloEfecto"/> que se aplicara</param>
		public ControladorEfectoSiendoAplicado(ControladorEfecto _controladorEfecto, ControladorPersonaje _instigador, ControladorPersonaje _objetivo)
			: base(null)
		{
			controladorEfecto = _controladorEfecto;
			instigador = _instigador;
			objetivo = _objetivo;

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

		public void AñadirAcumulacion()
		{
			//Si los efecto deben solapar entonces este metodo no debe hacer nada
			if(ComportamientoAcumulativo == EComportamientoAcumulativo.Solapar)
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
			if(--TurnosRestantes <= 0)
				QuitarEfecto();
		}

		/// <summary>
		/// Quita el efecto de su <see cref="objetivo"/>
		/// </summary>
		public void QuitarEfecto()
		{
			OnEfectoQuitado(this);

			Eliminar();
		}

		public override void ModificarComportamientoAcumulativo(
			EComportamientoAcumulativo nuevoComportamiento,
			EModoDeCambioDeComportamiento modoDeCambio)
		{
			if (nuevoComportamiento == ComportamientoAcumulativo)
				return;

			switch (nuevoComportamiento)
			{
				
			}
		}

		public override void ActulizarModelo(ModeloEfectoSiendoAplicado nuevoModelo)
		{
			base.ActulizarModelo(modelo);

			var nuevoEfecto = nuevoModelo.Efecto.Efecto;

			ActualizarFuncion(GetType().GetProperty(nameof(FnPuedeAplicarEfecto), typeof(ControladorFuncion_Predicado)), nuevoEfecto.FuncionPuedeAplicar.Funcion);
			ActualizarFuncion(GetType().GetProperty(nameof(FnAplicarEfecto), typeof(ControladorFuncion_Efecto)), nuevoEfecto.FuncionAplicar.Funcion);
			ActualizarFuncion(GetType().GetProperty(nameof(FnQuitarEfecto), typeof(ControladorFuncion_Efecto)), nuevoEfecto.FuncionQuitar.Funcion);

			ComportamientoAcumulativo = nuevoModelo.ComportamientoAcumulativo;
			TurnosRestantes           = nuevoModelo.TurnosRestantes;
		}

		private void ActualizarFuncion(PropertyInfo propertyFuncionActualizar, ModeloFuncion nuevaFuncion)
		{
			var funcionActulizar = propertyFuncionActualizar.GetValue(this) as ControladorFuncionBase;
			
			if (nuevaFuncion != funcionActulizar?.modelo)
			{
				if (nuevaFuncion.VariablesPersistentes.Count > 0 && funcionActulizar == null)
				{
					propertyFuncionActualizar.SetValue(this, new ControladorFuncion_Efecto(nuevaFuncion));
				}
				else if (nuevaFuncion.VariablesPersistentes.Count == 0 && funcionActulizar != null)
				{
					funcionActulizar.Eliminar();
					propertyFuncionActualizar.SetValue(this, null);
				}
				else
				{
					funcionActulizar?.ActulizarModelo(nuevaFuncion);
				}
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
	}
}