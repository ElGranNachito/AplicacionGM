using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que define propiedades y metodos compartidos entre <see cref="ControladorEfecto"/> y <see cref="ControladorEfectoSiendoAplicado"/>
	/// </summary>
	/// <typeparam name="TModelo"></typeparam>
	public abstract class ControladorEfectoBase<TModelo> : Controlador<TModelo>
		where TModelo : ModeloBase, new()
	{
		#region Propiedades

		/// <summary>
		/// Funcion que nos permite saber si el <see cref="ModeloEfecto"/> puede aplicarse desde determinado <see cref="ControladorPersonaje"/>
		/// a determinados <see cref="ControladorPersonaje"/>
		/// </summary>
		public virtual ControladorFuncion_Predicado FnPuedeAplicarEfecto { get; protected set; }

		/// <summary>
		/// Funcion que aplica el <see cref="ModeloEfecto"/> desde cierto <see cref="ControladorPersonaje"/> a otros <see cref="ControladorPersonaje"/>
		/// </summary>
		public virtual ControladorFuncion_Efecto FnAplicarEfecto { get; protected set; }

		/// <summary>
		/// Funcion que quita el <see cref="ModeloEfecto"/> desde cierto <see cref="ControladorPersonaje"/> a otros <see cref="ControladorPersonaje"/>
		/// </summary>
		public virtual ControladorFuncion_Efecto FnQuitarEfecto { get; protected set; }

		#endregion

		#region Constructor

		public ControladorEfectoBase(TModelo _modelo)
			: base(_modelo) { }

		#endregion

		#region Metodos

		[AccesibleEnGuraScratch(nameof(ModificarComportamientoAcumulativo))]
		public abstract void ModificarComportamientoAcumulativo(EComportamientoAcumulativo nuevoComportamiento, EModoDeCambioDeComportamientoAcumulativo modoDeCambio);

		/// <summary>
		/// Inicializa <see cref="FnAplicarEfecto"/>, <see cref="FnQuitarEfecto"/> o <see cref="FnPuedeAplicarEfecto"/> segun
		/// corresponda en base al <paramref name="tipoFuncion"/>
		/// </summary>
		/// <param name="modeloFuncion">Modelo que se utilizara para construir el <see cref="ControladorFuncion{TipoFuncion}"/></param>
		/// <param name="tipoFuncion">Tipo de la funcion contenida por el <paramref name="modeloFuncion"/></param>
		protected ControladorFuncionBase InicializarFuncion(ModeloFuncion modeloFuncion, ETipoFuncionEfecto tipoFuncion)
		{
			switch (tipoFuncion)
			{
				case ETipoFuncionEfecto.FuncionPuedeAplicar:
					FnPuedeAplicarEfecto = SistemaPrincipal.ObtenerControlador<ControladorFuncion_Predicado, ModeloFuncion>(modeloFuncion, true);
					return FnPuedeAplicarEfecto;

				case ETipoFuncionEfecto.FuncionAplicar:
					FnAplicarEfecto = SistemaPrincipal.ObtenerControlador<ControladorFuncion_Efecto, ModeloFuncion>(modeloFuncion, true);
					return FnAplicarEfecto;

				case ETipoFuncionEfecto.FuncionQuitar:
					FnQuitarEfecto = SistemaPrincipal.ObtenerControlador<ControladorFuncion_Efecto, ModeloFuncion>(modeloFuncion, true);
					return FnQuitarEfecto;

				default:
					SistemaPrincipal.LoggerGlobal.Log($"{tipoFuncion} no soportado!", ESeveridad.Error);
					return null;
			}
		}

		#endregion
	}
}