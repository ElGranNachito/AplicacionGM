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
		#region Propiedades

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
		public bool SePuedeSeleccionTipoDelBloque => TipoCondicional != ETipoBloqueCondicional.If;

		/// <summary>
		/// Indica si se debe mostrar la lista de condiciones de este bloque
		/// </summary>
		public bool MostrarCondicion => TipoCondicional != ETipoBloqueCondicional.Else; 

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_bloqueContenedor"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene este bloque</param>
		public ViewModelBloqueCondicional(
			ViewModelBloqueFuncionBase _bloqueContenedor,
			ETipoBloqueCondicional _tipoCondicional = ETipoBloqueCondicional.NINGUNO)

			: base(_bloqueContenedor.Padre, _bloqueContenedor.IDBloque)
		{
			ArgumentosCondicion = new ViewModelSeccionesCondicion(this);

			TipoCondicional = _tipoCondicional;
		}

		/// <summary>
		/// Constructor que se utiliza para incializar una instancia a partir de datos existentes en <see cref="BloqueCondicional"/>
		/// </summary>
		/// <param name="_idBloque">id que se le asignara a este bloque</param>
		/// <param name="_operacionesLogicas">lista de las <see cref="EOperacionLogica"/> que se realizan en esta condicion</param>
		/// <param name="_tipoCondicional"><see cref="ETipoBloqueCondicional"/></param>
		/// <param name="_padre"><see cref="IContenedorDeBloques"/> que contiene a este bloque. Si se deja en null se asignara por defecto
		/// el <see cref="ViewModelCreacionDeFuncionBase"/> actualmente activo</param>
		public ViewModelBloqueCondicional(
			int _idBloque,
			List<ParametrosInicializarArgumentoDesdeBloque> _parametrosArgumentos,
			List<EOperacionLogica> _operacionesLogicas,
			ETipoBloqueCondicional _tipoCondicional,
			IContenedorDeBloques _padre = null)

			: base(_padre, _idBloque)
		{
			//Hacemos que el contenedor de los argumentos sea este VM
			_parametrosArgumentos.ForEach(arg => arg.contenedor = this);

			//Creamos el contenedor de los argumentos
			ArgumentosCondicion = new ViewModelSeccionesCondicion(this, _parametrosArgumentos, _operacionesLogicas);

			TipoCondicional = _tipoCondicional;
		} 

		#endregion

		#region Metodos

		public override void Inicializar()
		{
			DispararPropertyChanged(nameof(SePuedeSeleccionTipoDelBloque));

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(TipoCondicional)))
					DispararPropertyChanged(nameof(MostrarCondicion));
			};
		}

		public override BloqueCondicional GenerarBloque_Impl()
		{
			IEnumerable<BloqueArgumento> argumentos = ArgumentosCondicion.argumentos.Select(arg => arg.GenerarBloque_Impl());

			return new BloqueCondicional(IDBloque, argumentos.ToList(), ArgumentosCondicion.operaciones, TipoCondicional);
		} 

		#endregion
	}
}