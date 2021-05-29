using System;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel para el control ComboBoxConDescripcion
    /// </summary>
    /// <typeparam name="T">Tipo del <see cref="Enum"/></typeparam>
    public class ViewModelComboBoxConDescripcion<T> : BaseViewModel
		where T: Enum
    {
		#region Propiedades

		/// <summary>
		/// Opcion de la combobox seleccionada
		/// </summary>
		public T OpcionSeleccionada { get; set; } 

		#endregion
	}
}
