using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Viewmodel de las cartas del menu principal
    /// </summary>
    public class ViewModelCarta : BaseViewModel
    {
		#region Propiedades

		/// <summary>
		/// Indice Z
		/// </summary>
		public int ZIndex { get; set; } = 0;

		/// <summary>
		/// Comando que se ejecuta al hacer click sobre ellas
		/// </summary>
		public ICommand Comando { get; set; }

		/// <summary>
		/// Comando que se ejecuta cuando el mouse se coloca sobre la carta
		/// </summary>
		public ICommand ComandoMouseEnter { get; set; }

		/// <summary>
		/// Comando que se ejecuta cuando el mouse deja de estar sobre la carta
		/// </summary>
		public ICommand ComandoMouseLeave { get; set; } 

		#endregion
	}
}
