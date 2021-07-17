using System.Collections.Generic;
using System.Diagnostics;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa un evento 
	/// </summary>
	public class Drag : ViewModel
	{
		/// <summary>
		/// Indica si el usuario esta actualmente arrastrando algun elemento
		/// </summary>
		public bool HayUnDragActivo => ViewModelContenido != null;

		/// <summary>
		/// Pos del control del drag con respecto a su contenedor en el eje X
		/// </summary>
		public double PosX { get; set; }

		/// <summary>
		/// Pos del control del drag con respecto a su contenedor en el eje Y
		/// </summary>
		public double PosY { get; set; }

		public Grosor OffsetControl { get; set; }

		/// <summary>
		/// Viewmodel del contenido del control del drag
		/// </summary>
		public IDrageable ViewModelContenido { get; set; }

		/// <summary>
		/// Parametros de este drag
		/// </summary>
		public Dictionary<int, object> ParametrosDrag { get; set; } = new Dictionary<int, object>();

		/// <summary>
		/// <see cref="List{T}"/> de <see cref="IReceptorDeDrag"/> sobre los que se encuentra el drag
		/// </summary>
		public List<IReceptorDeDrag> ReceptoresActualmenteActivos = new List<IReceptorDeDrag>();

		/// <summary>
		/// Evento que se dispara cuando comienza un drag
		/// </summary>
		public event DDrag OnComienzoDrag = delegate{};

		/// <summary>
		/// Evento que se dispara cuan un drag finaliza
		/// </summary>
		public event DDrag OnFinDrag      = delegate {};

		/// <summary>
		/// Comienza un drag
		/// </summary>
		/// <param name="vm"><see cref="ViewModel"/> del contenido del control</param>
		public void ComenzarDrag(IDrageable vm, IEnumerable<KeyValuePair<int, object>> parametros)
		{
			ViewModelContenido = vm;

			foreach (var parametro in parametros)
				ParametrosDrag.Add(parametro.Key, parametro.Value);
			
			OnComienzoDrag(vm);

			EventoVentana eventoMouseSoltado = null;

			eventoMouseSoltado = ventana =>
			{
				OnFinDrag(ViewModelContenido);

				for (int i = 1; i <= ReceptoresActualmenteActivos.Count; ++i)
				{
					if (ReceptoresActualmenteActivos[i - 1].OnDrop(ViewModelContenido) && 
					    i != ReceptoresActualmenteActivos.Count)
					{
						ReceptoresActualmenteActivos.RemoveRange(i, ReceptoresActualmenteActivos.Count - i);

						break;
					}
				}

				ViewModelContenido.Soltado(ReceptoresActualmenteActivos);

				ReceptoresActualmenteActivos.Clear();

				ViewModelContenido = null;
				ParametrosDrag.Clear();

				SistemaPrincipal.Aplicacion.VentanaActual.OnMouseUp -= eventoMouseSoltado;
			};

			SistemaPrincipal.Aplicacion.VentanaActual.OnMouseUp += eventoMouseSoltado;
		}

		public void AñadirReceptorDrag(IReceptorDeDrag receptor)
		{
			ReceptoresActualmenteActivos.Add(receptor);

			ViewModelContenido.OnEntrarAElemento(receptor);

			receptor.OnDragEntro(ViewModelContenido);

			//Si solo hay un elemento no hace falta ordenar
			if (ReceptoresActualmenteActivos.Count == 1)
				return;

			//Colocamos el nuevo elemento donde corresponda
			int indiceActual = ReceptoresActualmenteActivos.Count - 1;

			while(indiceActual > 0 && ReceptoresActualmenteActivos[indiceActual - 1].IndiceZ < receptor.IndiceZ)
			{
				ReceptoresActualmenteActivos[indiceActual] = ReceptoresActualmenteActivos[indiceActual - 1];

				--indiceActual;
			}

			ReceptoresActualmenteActivos[indiceActual] = receptor;
		}

		public void QuitarReceptorDrag(IReceptorDeDrag receptor)
		{
			ViewModelContenido.OnSalirDeElemento(receptor);

			receptor.OnDragSalio(ViewModelContenido);

			ReceptoresActualmenteActivos.Remove(receptor);
		}

		public bool ParametroAsignado(int key) => ParametrosDrag.ContainsKey(key);

		public object this[int key]
		{
			get
			{
				if (!ParametrosDrag.ContainsKey(key))
					return null;

				return ParametrosDrag[key];
			}

			set
			{
				if (!ParametrosDrag.ContainsKey(key))
					ParametrosDrag.Add(key, value);
				else
					ParametrosDrag[key] = value;
			}
		}
	}

	/// <summary>
	/// Clase que contiene los valores de las keys para los distintos parametros
	/// que puede llegar a tener un <see cref="Drag"/>
	/// </summary>
	public static class KeysParametrosDrag
	{
		public const int IndicePrametroExtra = 0;

		public const int IndiceParametroPosicionBloque = 1;
	}
}