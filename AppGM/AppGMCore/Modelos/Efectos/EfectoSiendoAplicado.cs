namespace AppGM.Core
{
	//En busca de un mejor nombre

	/// <summary>
	/// Modelo que representa un efecto siendo aplicado sobre un objetivo.
	/// </summary>
	public class ModeloEfectoSiendoAplicado : ModeloBase
	{
		/// <summary>
		/// Turnos que le restan al efecto
		/// </summary>
		public int TurnosRestantes { get; set; }

		/// <summary>
		/// Indica si esta siendo aplicado actualmente
		/// </summary>
		public bool EstaSiendoAplicado { get; set; }

		/// <summary>
		/// Cuenta el numero de acumulaciones de este efecto
		/// </summary>
		public int ContadorAcumulaciones { get; set; }

		/// <summary>
		/// Comportamiento acumulativo de esta aplicacion
		/// </summary>
		public EComportamientoAcumulativo ComportamientoAcumulativo { get; set; }

		/// <summary>
		/// Modelo del efecto
		/// </summary>
		public virtual TIEfectoSiendoAplicadoEfecto Efecto { get; set; }

		/// <summary>
		/// <see cref="ModeloPersonaje"/> que causo o instigo el efecto
		/// </summary>
		public virtual TIEfectoSiendoAplicadoPersonajeInstigador Instigador { get; set; }

		/// <summary>
		/// <see cref="ModeloPersonaje"/> a los que se les esta aplicando el efecto
		/// </summary>
		public virtual TIEfectoSiendoAplicadoPersonajeObjetivo Objetivo { get; set; }
	}
}