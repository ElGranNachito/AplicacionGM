using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una parte de un <see cref="ViewModelBloqueCondicionalCompleto"/>, es decir un If, Else o Else If
	/// </summary>
	public class ViewModelBloqueCondicional : ViewModelBloqueContenedorConDrop<BloqueCondicional>
	{
		/// <summary>
		/// Tipos de bloque que pueden ser seleccionado dentro de la combo box
		/// </summary>
		public List<ETipoBloqueCondicional> TiposBloqueSeleccionables => new List<ETipoBloqueCondicional>
		{
			ETipoBloqueCondicional.Else,
			ETipoBloqueCondicional.ElseIf
		};

		/// <summary>
		/// Contiene todos los <see cref="ViewModelArgumento"/> y <see cref="EOperacionLogica"/>
		/// que realizar con esos argumentos
		/// </summary>
		public ViewModelSeccionesCondicion ArgumentosCondicion { get; set; }

		/// <summary>
		/// Nombre de este bloque
		/// </summary>
		public ETipoBloqueCondicional TipoCondicional { get; set; }

		/// <summary>
		/// Indica si el <see cref="TipoCondicional"/> puede ser seleccionado por el usuario
		/// </summary>
		public bool SePuedeSeleccionTipoDelBloque { get; set; }

		/// <summary>
		/// Indica si se debe mostrar la lista de condiciones de este bloque
		/// </summary>
		public bool MostrarCondicion => TipoCondicional != ETipoBloqueCondicional.Else;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene este bloque</param>
		public ViewModelBloqueCondicional(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion, ETipoBloqueCondicional _tipoCondicional = ETipoBloqueCondicional.NINGUNO)
			: base(_vmCreacionDeFuncion)
		{
			ArgumentosCondicion = new ViewModelSeccionesCondicion(_vmCreacionDeFuncion, this);

			TipoCondicional = _tipoCondicional;

			SePuedeSeleccionTipoDelBloque = TipoCondicional != ETipoBloqueCondicional.If;

			PropertyChanged += (sender, args) =>
			{
				if(args.PropertyName.Equals(nameof(TipoCondicional)))
					DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MostrarCondicion)));
			};
		}

		public override BloqueCondicional GenerarBloque_Impl()
		{
			IEnumerable<BloqueArgumento> argumentos = ArgumentosCondicion.argumentos.Select(arg => arg.GenerarBloque_Impl());

			return new BloqueCondicional(IDBloque, argumentos.ToList(), ArgumentosCondicion.operaciones, TipoCondicional);
		}
	}
}
