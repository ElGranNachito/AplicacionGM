namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que deben implementar los <see cref="ViewModel"/> que necesiten ser capaces de responder a un
	/// evento de <see cref="Drag"/>
	/// </summary>
	public interface IReceptorDeDrag
	{
		/// <summary>
		/// Funcion que se llamara cuando el cursor entre con un <see cref="Drag"/>
		/// </summary>
		public void OnDragEnter(ViewModel vm);

		/// <summary>
		/// Funcion que se llamara cuando el cursor salga con un <see cref="Drag"/>
		/// </summary>
		public void OnDragLeave(ViewModel vm);

		/// <summary>
		/// Funcion que se llamara cuando el usuario suelte el <see cref="Drag"/> sobre este <see cref="IReceptorDeDrag"/>
		/// </summary>
		public void OnDrop(ViewModel vm);
	}
}