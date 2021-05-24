namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel para el control ComboBoxConDescripcion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewModelComboBoxConDescripcion<T> : BaseViewModel
    {
		#region Propiedades

		/// <summary>
		/// Opcion de la combobox seleccionada
		/// </summary>
		public T OpcionSeleccionada { get; set; } 

		#endregion
	}
}
