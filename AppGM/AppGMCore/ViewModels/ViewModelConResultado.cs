
namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que produce un resultado cuando el usuario sale del control que representa
	/// </summary>
	public abstract class ViewModelConResultado : ViewModel
	{
		public EResultadoViewModel Resultado { get; protected set; }
	}
}
