using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa una cadena de If y Elses
	/// </summary>
	public class ViewModelBloqueCondicionalCompleto : ViewModelBloqueContenedor<BloqueCondicionalCompleto>
	{
		#region Propiedades

		/// <summary>
		/// Comando que se ejecuta al presionar el boton para añadir un nuevo <see cref="ViewModelBloqueCondicional"/> a
		/// <see cref="CondicionesConsecuentes"/>
		/// </summary>
		public ICommand ComandoAñadirBloque { get; set; } 

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_padre"><see cref="IContenedorDeBloques"/> que contiene a este bloque. Si se deja en null se asignara por defecto
		/// el <see cref="ViewModelCreacionDeFuncionBase"/> actualmente activo</param>
		/// <param name="_idBloque">id que sera asignada a este bloque. Si se deja en -1, la id se asignara automaticamente</param>
		public ViewModelBloqueCondicionalCompleto(IContenedorDeBloques _padre = null, int _idBloque = -1)
			: base(_padre, _idBloque)
		{
			ComandoAñadirBloque = new Comando(() => AñadirBloque(new ViewModelBloqueCondicional(this, ETipoBloqueCondicional.Else)));

			AñadirBloque(new ViewModelBloqueCondicional(this, ETipoBloqueCondicional.If));
		}

		/// <summary>
		/// Constructor que inicializa esta instancia a partir de datos existentes en <see cref="BloqueCondicionalCompleto"/>
		/// </summary>
		/// <param name="_idBloque">id que se le asignara al bloque</param>
		/// <param name="_condiciones">lista de <see cref="BloqueCondicional"/> que hay en este condicional</param>
		/// <param name="_acciones">lista de listas que contienen los <see cref="BloqueBase"/> que corresponden a las respectivas <paramref name="_condiciones"/></param>
		/// <param name="_padre"><see cref="IContenedorDeBloques"/> que contiene a este bloque. Si se deja en null se asignara por defecto
		/// el <see cref="ViewModelCreacionDeFuncionBase"/> actualmente activo</param>
		public ViewModelBloqueCondicionalCompleto(
			int _idBloque,
			List<BloqueCondicional> _condiciones, 
			List<List<BloqueBase>> _acciones, 
			IContenedorDeBloques _padre = null)

			:base(_padre, _idBloque)
		{
			//Nos aseguramos de que la cantidad de elementos en las colecciones sea igual
			if (_condiciones.Count != _acciones.Count)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Cantidad de elementos en {nameof(_condiciones)} y en {nameof(_acciones)} es distinto", ESeveridad.Error);

				return;
			}

			for (int i = 0; i < _condiciones.Count; ++i)
			{
				//Obtenemos el VM de la condicion y lo casteamos al tipo correspondiente
				var nuevoVM = _condiciones[i].ObtenerViewModel(this) as ViewModelBloqueCondicional;

				//Lo añadimos a la lista de condiciones
				AñadirBloque(nuevoVM);

				//Recorremos la lista de bloques correspondientes a la condicion actual
				for (int j = 0; j < _acciones[i].Count; j++)
				{
					//Obtenemos el VM del bloque actual y lo añadimos a la lista de bloques de la condicion actual
					nuevoVM.AñadirBloque(_acciones[i][j].ObtenerViewModel(nuevoVM));
				}
			}

			ComandoAñadirBloque = new Comando(() => AñadirBloque(new ViewModelBloqueCondicional(this, ETipoBloqueCondicional.Else)));
		}

		#endregion

		#region Metodos

		public override BloqueCondicionalCompleto GenerarBloque_Impl()
		{
			IEnumerable<ViewModelBloqueCondicional> vmsCondiciones = Bloques.Elementos.Cast<ViewModelBloqueCondicional>();

			List<BloqueCondicional> condiciones = new List<BloqueCondicional>(vmsCondiciones.Select(condicion => condicion.GenerarBloque_Impl()));

			List<List<BloqueBase>> acciones = new List<List<BloqueBase>>();

			foreach (var condicion in vmsCondiciones)
			{
				acciones.Add(condicion.Bloques.Elementos.Select(bloque => bloque.GenerarBloque()).ToList());
			}

			var resultado = new BloqueCondicionalCompleto(IDBloque, condiciones, acciones);

			return resultado;
		}

		public override void OnDragSalio_Impl(IDrageable vm)
		{
			IEnumerable<ViewModelBloqueCondicional> vmsCondiciones = Bloques.Elementos.Cast<ViewModelBloqueCondicional>();

			foreach (var condicion in vmsCondiciones)
			{
				if (condicion.ReceptorAñadirBloque.EsVisible)
					return;
			}

			MostrarEspacioDrop = false;
		}

		public override bool OnDrop_Impl(IDrageable vm)
		{
			foreach (var bloque in Bloques)
			{
				if (bloque is ViewModelBloqueCondicional condicion)
					condicion.ReceptorAñadirBloque.EsVisible = false;
			}

			return base.OnDrop_Impl(vm);
		}

		public override bool VerificarValidez()
		{
			return Bloques.Elementos.All(b =>
			{
				b.ActualizarValidez();

				return b.EsValido;
			});
		}

		#endregion
	}
}