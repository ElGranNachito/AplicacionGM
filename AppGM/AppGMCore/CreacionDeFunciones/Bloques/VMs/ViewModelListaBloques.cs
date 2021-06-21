using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// VM que representa una <see cref="ObservableCollection{T}"/> de <see cref="ViewModelBloqueFuncionBase"/>
	/// </summary>
	public class ViewModelListaBloques : ViewModel
	{
		/// <summary>
		/// Lista de bloques
		/// </summary>
		public ObservableCollection<ViewModelBloqueFuncionBase> Bloques { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public ViewModelListaBloques()
		{
			Bloques = new ObservableCollection<ViewModelBloqueFuncionBase>();
		}

		/// <summary>
		/// Inicializa <see cref="Bloques"/> utilizando <paramref name="bloques"/>
		/// </summary>
		/// <param name="bloques"><see cref="IEnumerable{T}"/> que contiene <see cref="ViewModelBloqueFuncionBase"/></param>
		public ViewModelListaBloques(IEnumerable<ViewModelBloqueFuncionBase> bloques)
		{
			Bloques = new ObservableCollection<ViewModelBloqueFuncionBase>(bloques);
		}
	}
}
