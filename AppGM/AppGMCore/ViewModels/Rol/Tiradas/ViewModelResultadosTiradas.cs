using System.Collections.Generic;
using System.Windows.Input;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que contiene uno o mas <see cref="ViewModelResultadoTirada"/>
	/// </summary>
	public class ViewModelResultadosTiradas : ViewModel
	{
		/// <summary>
		/// <see cref="List{T}"/> con los <see cref="ViewModelResultadoTirada"/> contenidos por este <see cref="ViewModelResultadosTiradas"/>
		/// </summary>
		private readonly List<ViewModelResultadoTirada> resultados;

		/// <summary>
		/// Indice del <see cref="ViewModelResultadoTirada"/> actualmente seleccionado
		/// </summary>
		public int Indice { get; private set; } = 0;

		/// <summary>
		/// Obtiene un texto que muestra la posicion actual del <see cref="Indice"/>
		/// </summary>
		public string TextoPosicionActual => $"{Indice + 1}/{resultados.Count}";

		/// <summary>
		/// Obtiene una <see cref="IReadOnlyList{T}"/> con los <see cref="ViewModelResultadoTirada"/>
		/// contenidos por este <see cref="ViewModelResultadosTiradas"/>
		/// </summary>
		public IReadOnlyList<ViewModelResultadoTirada> Resultados => resultados.AsReadOnly();

		/// <summary>
		/// <see cref="ViewModelResultadoTirada"/> actualmente seleccionado por el <see cref="Indice"/>
		/// </summary>
		public ViewModelResultadoTirada ResultadoActual => Indice < resultados.Count ? resultados[Indice] : null;

		/// <summary>
		/// Aumenta el <see cref="Indice"/>
		/// </summary>
		public ICommand ComandoIncrementarIndice { get; init; } 

		/// <summary>
		/// Disminuce el <see cref="Indice"/>
		/// </summary>
		public ICommand ComandoDisminuirIndice { get; init; }

		/// <summary>
		/// Construye una nueva instancia de esta clase a partir de una <see cref="List{T}"/> de resultados
		/// </summary>
		/// <param name="_resultados"></param>
		public ViewModelResultadosTiradas(IEnumerable<ViewModelResultadoTirada> _resultados)
		{
			resultados = new List<ViewModelResultadoTirada>(_resultados);

			if(resultados.Count == 0)
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(_resultados)} esta vacio", ESeveridad.Advertencia);

			ComandoIncrementarIndice = new Comando(() =>
			{
				Indice = ++Indice % resultados.Count;

				DispararPropertyChanged(nameof(ResultadoActual));
				DispararPropertyChanged(nameof(TextoPosicionActual));
			});

			ComandoDisminuirIndice = new Comando(() =>
			{
				--Indice;

				if (Indice < 0)
					Indice = resultados.Count - 1;

				DispararPropertyChanged(nameof(ResultadoActual));
				DispararPropertyChanged(nameof(TextoPosicionActual));
			});
		}

		/// <summary>
		/// Construye una nueva instancia de esta clase a partir de un solo <paramref name="_resultado"/>
		/// </summary>
		/// <param name="_resultado">Resultado de la tirada</param>
		public ViewModelResultadosTiradas(ViewModelResultadoTirada _resultado)
			:this(new List<ViewModelResultadoTirada>(new []{_resultado}))
		{}

	}
}