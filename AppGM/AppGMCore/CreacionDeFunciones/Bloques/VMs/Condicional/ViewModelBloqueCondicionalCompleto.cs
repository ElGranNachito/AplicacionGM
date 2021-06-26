using System.Collections.Generic;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa una cadena de If y Elses
	/// </summary>
	public class ViewModelBloqueCondicionalCompleto : ViewModelBloqueCondicionalBase
	{
		/// <summary>
		/// <see cref="ViewModelBloqueCondicionalCompleto"/> que proceden a este, es decir Else Ifs y Elses
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueCondicionalBase> CondicionesConsecuentes { get; set; } =
			new ViewModelListaDeElementos<ViewModelBloqueCondicionalBase>();

		public ICommand ComandoAñadirBloque { get; set; }

		public ViewModelBloqueCondicionalCompleto(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion)
		{
			ComandoAñadirBloque = new Comando(AñadirBloque);

			CondicionesConsecuentes.Add(new ViewModelBloqueCondicionalBase());
		}

		public override BloqueCondicional GenerarBloque_Impl()
		{
			return null;
		}

		private void AñadirBloque()
		{

		}
	}
}
