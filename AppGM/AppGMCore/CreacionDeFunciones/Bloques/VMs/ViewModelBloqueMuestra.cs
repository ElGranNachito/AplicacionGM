using System;

namespace AppGM.Core
{
	/// <summary>
	/// VM que tendran los bloques de muestra
	/// </summary>
	public class ViewModelBloqueMuestra : ViewModelBloqueFuncionBase
	{
		/// <summary>
		/// Tipo del bloque mostrado
		/// </summary>
		public Type tipoBloque;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene este vm</param>
		/// <param name="_tipoBLoque"><see cref="Type"/> del bloque que muestra este vm</param>
		public ViewModelBloqueMuestra(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion, Type _tipoBLoque)
			:base(_vmCreacionDeFuncion)
		{
			tipoBloque = _tipoBLoque;
		}

		public override ViewModelBloqueFuncionBase Copiar(IContenedorDeBloques destino)
		{
			//Nos aseguramos de que el tipoBloque sea un ViemModelBloqueFuncionBase
			if (!typeof(ViewModelBloqueFuncionBase).IsAssignableFrom(tipoBloque))
				return null;

			return (ViewModelBloqueFuncionBase) Activator.CreateInstance(tipoBloque, destino ?? VMCreacionDeFuncion, -1);
		}
	}
}
