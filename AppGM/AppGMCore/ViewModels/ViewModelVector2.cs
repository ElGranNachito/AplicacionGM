namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel que representa un vector de dos valores
    /// </summary>
    public class ViewModelVector2 : BaseViewModel
    {
		#region Propiedades

		/// <summary>
		/// Controlador del vector
		/// </summary>
		public Vector2 Posicion { get; set; }

		/// <summary>
		/// Valor del eje x
		/// </summary>
		public double X
		{
			get => Posicion.X;
			set => Posicion.X = value;
		}

		/// <summary>
		/// Valor del eje y
		/// </summary>
		public double Y
		{
			get => Posicion.Y;
			set => Posicion.Y = value;
		}

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="v">Controlador del vector</param>
		public ViewModelVector2(Vector2 v)
		{
			Posicion = v;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_x">Valor para el eje x</param>
		/// <param name="_y">Valor para el eje y</param>
		public ViewModelVector2(double _x = 0, double _y = 0)
		{
			Posicion = new Vector2(_x, _y);
		} 

		#endregion
	}
}