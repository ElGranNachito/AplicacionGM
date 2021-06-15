using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// View model base para la creacion de una funcion, ya sea para una habilidad, efecto, condicion.
	/// </summary>
	/// <typeparam name="TipoFuncion">Tipo de la funcion que sera creada</typeparam>
	public abstract class ViewModelCreacionDeFuncion<TipoFuncion> : ViewModelCreacionDeFuncionBase
	{
		/// <summary>
		/// Resultado de <see cref="CrearFuncion"/>
		/// </summary>
		public TipoFuncion resultado;

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton 'Compilar'
		/// </summary>
		public ICommand ComandoCompilar { get; set; }

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton 'Cancelar'
		/// </summary>
		public ICommand ComandoCancelar { get; set; }

		public ViewModelCreacionDeFuncion()
		{
			ComandoCompilar = new Comando(CrearFuncion);
		}

		/// <summary>
		/// Llama al compilar y devuelve la funcion que este genere
		/// </summary>
		/// <returns>Funcion generada</returns>
		public abstract void CrearFuncion();
	}
}