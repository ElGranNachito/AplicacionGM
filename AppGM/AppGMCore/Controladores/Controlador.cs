namespace AppGM.Core
{
	/// <summary>
	/// Clase abstracta base de todos los controladores
	/// </summary>
    public abstract class ControladorBase {}

	/// <summary>
	/// Clase base de todos los controladores
	/// </summary>
	/// <typeparam name="TipoModelo">Tipo del modelo que este controlador representa</typeparam>
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: ModeloBaseSK, new()
    {
        #region Campos

		/// <summary>
		/// Instancia del modelo que este controlador representa
		/// </summary>
        public TipoModelo modelo;

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor default
		/// </summary>
		public Controlador() {}

		/// <summary>
		/// Constructor default
		/// </summary>
		/// <param name="_modelo"></param>
		public Controlador(TipoModelo _modelo)
		{
			modelo = _modelo;
		} 

		#endregion

		#region Funciones

		/// <summary>
		/// Funcion encargada de eliminar el modelo de la base de datos
		/// </summary>
		public virtual void Eliminar() { } 

        #endregion
    }
}
