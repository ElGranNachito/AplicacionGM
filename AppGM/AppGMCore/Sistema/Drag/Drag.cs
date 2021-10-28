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
		#region Campos & Propiedades

		/// <summary>
		/// Argumentos del evento actualmente activo
		/// </summary>
		private ArgumentosDragAndDropBase mArgumentosEventoActual = null;

		/// <summary>
		/// Indica si el usuario esta actualmente arrastrando algun elemento
		/// </summary>
		public bool HayUnDragActivo => TipoDragActivo != ETipoDrag.Ninguno;

		/// <summary>
		/// Pos del control del drag con respecto a su contenedor en el eje X
		/// </summary>
		public double PosX { get; set; }

		/// <summary>
		/// Pos del control del drag con respecto a su contenedor en el eje Y
		/// </summary>
		public double PosY { get; set; }

		/// <summary>
		/// Tipo del drag actualmente activo
		/// </summary>
		public ETipoDrag TipoDragActivo { get; private set; }

		/// <summary>
		/// Offset del widget de drag
		/// </summary>
		public Grosor OffsetControl { get; set; }

		/// <summary>
		/// Viewmodel del contenido del control del drag
		/// </summary>
		public List<IDrageable> DatosDrag { get; set; } = new List<IDrageable>();

		/// <summary>
		/// Parametros de este drag
		/// </summary>
		public Dictionary<int, object> ArgumentosExtraDrag { get; set; } = new Dictionary<int, object>();

		/// <summary>
		/// <see cref="List{T}"/> de <see cref="IReceptorDeDrag"/> sobre los que se encuentra el drag
		/// </summary>
		public List<IReceptorDeDrag> ReceptoresActualmenteActivos = new List<IReceptorDeDrag>();

		/// <summary>
		/// Evento que se dispara cuando comienza un drag
		/// </summary>
		public event DDrag OnComienzoDrag = delegate { };

		/// <summary>
		/// Evento que se dispara cuando comienza un drag
		/// </summary>
		public event DDrag OnComienzoDragMultiple = delegate { };

		/// <summary>
		/// Evento que se dispara cuan un drag finaliza
		/// </summary>
		public event DDrag OnFinDrag = delegate { };

		#endregion

		#region Metodos

		#region Publicos

		/// <summary>
		/// Comienza un drag de elemento unico
		/// </summary>
		/// <param name="contenido">Contenido del drag</param>
		/// <param name="argumentosExtra">Coleccion de <see cref="KeyValuePair"/> con los argumentos extra de este drag</param>
		public void ComenzarDrag(IDrageable contenido, IEnumerable<KeyValuePair<int, object>> argumentosExtra)
		{
			DatosDrag.Add(contenido);

			ArgumentosExtraDrag.Clear();

			foreach (var parametro in argumentosExtra)
				ArgumentosExtraDrag.Add(parametro.Key, parametro.Value);

			mArgumentosEventoActual = new ArgumentosDragAndDropUnico(DatosDrag[0], ArgumentosExtraDrag);

			ComenzarDrag_Interno();

			TipoDragActivo = ETipoDrag.Unico;
		}

		/// <summary>
		/// Comeinza un drag
		/// </summary>
		/// <param name="contenido">Contenido del drag</param>
		/// <param name="argumentosExtra">Coleccion de <see cref="KeyValuePair"/> con los argumentos extra de este drag</param>
		public void ComenzarDragMultiple(List<IDrageableMultiple> contenido, IEnumerable<KeyValuePair<int, object>> argumentosExtra)
		{
			DatosDrag.AddRange(contenido);

			ArgumentosExtraDrag.Clear();

			foreach (var parametro in argumentosExtra)
				ArgumentosExtraDrag.Add(parametro.Key, parametro.Value);

			mArgumentosEventoActual = new ArgumentosDragAndDropMultiple(DatosDrag, ArgumentosExtraDrag);

			ComenzarDrag_Interno();

			TipoDragActivo = ETipoDrag.Multiple;
		}

		/// <summary>
		/// Añade un <see cref="IReceptorDeDrag"/> a <see cref="ReceptoresActualmenteActivos"/>
		/// </summary>
		/// <param name="receptor">Receptor que añadir</param>
		public void AñadirReceptorDrag(IReceptorDeDrag receptor)
		{
			ReceptoresActualmenteActivos.Add(receptor);

			foreach (var vm in DatosDrag)
			{
				vm.OnEntrarAElemento(receptor, mArgumentosEventoActual);
			}

			receptor.OnDragEntro(mArgumentosEventoActual);

			//Si solo hay un elemento no hace falta ordenar
			if (ReceptoresActualmenteActivos.Count == 1)
				return;

			//Colocamos el nuevo elemento donde corresponda
			int indiceActual = ReceptoresActualmenteActivos.Count - 1;

			while (indiceActual > 0 && ReceptoresActualmenteActivos[indiceActual - 1].IndiceZ < receptor.IndiceZ)
			{
				ReceptoresActualmenteActivos[indiceActual] = ReceptoresActualmenteActivos[indiceActual - 1];

				--indiceActual;
			}

			ReceptoresActualmenteActivos[indiceActual] = receptor;
		}

		/// <summary>
		/// Quita un <see cref="IReceptorDeDrag"/> de <see cref="ReceptoresActualmenteActivos"/>
		/// </summary>
		/// <param name="receptor">Receptor que quitar</param>
		public void QuitarReceptorDrag(IReceptorDeDrag receptor)
		{
			foreach (var vm in DatosDrag)
			{
				vm.OnSalirDeElemento(receptor, mArgumentosEventoActual);
			}

			receptor.OnDragSalio(mArgumentosEventoActual);

			ReceptoresActualmenteActivos.Remove(receptor);
		}

		/// <summary>
		/// Indica si un parametro con determinada <paramref name="key"/> se encuentra asignado
		/// </summary>
		/// <param name="key">Key del argumento</param>
		/// <returns><see cref="bool"/> indicando si el parametro ha sido asignado</returns>
		public bool ParametroAsignado(int key) => ArgumentosExtraDrag.ContainsKey(key);

		public object this[int key]
		{
			get
			{
				if (!ArgumentosExtraDrag.ContainsKey(key))
					return null;

				return ArgumentosExtraDrag[key];
			}

			set
			{
				if (!ArgumentosExtraDrag.ContainsKey(key))
					ArgumentosExtraDrag.Add(key, value);
				else
					ArgumentosExtraDrag[key] = value;
			}
		} 

		#endregion

		#region Privados

		/// <summary>
		/// Metodo que contiene la logica compartida de <see cref="ComenzarDrag"/> y <see cref="ComenzarDragMultiple"/>
		/// </summary>
		private void ComenzarDrag_Interno()
		{
			OnComienzoDrag(mArgumentosEventoActual);

			foreach (var drageable in DatosDrag)
			{
				drageable.OnComienzoDrag(mArgumentosEventoActual);
			}

			EventoVentana eventoMouseSoltado = null;

			eventoMouseSoltado = ventana =>
			{
				OnFinDrag(mArgumentosEventoActual);

				for (int i = 1; i <= ReceptoresActualmenteActivos.Count; ++i)
				{
					if (ReceptoresActualmenteActivos[i - 1].OnDrop(mArgumentosEventoActual) &&
						i != ReceptoresActualmenteActivos.Count)
					{
						ReceptoresActualmenteActivos.RemoveRange(i, ReceptoresActualmenteActivos.Count - i);

						break;
					}
				}

				foreach (var vm in DatosDrag)
				{
					vm.Soltado(ReceptoresActualmenteActivos, mArgumentosEventoActual);
				}

				ReceptoresActualmenteActivos.Clear();
				DatosDrag.Clear();
				ArgumentosExtraDrag.Clear();

				mArgumentosEventoActual = null;

				TipoDragActivo = ETipoDrag.Ninguno;

				SistemaPrincipal.Aplicacion.VentanaActual.OnMouseUp -= eventoMouseSoltado;
			};

			SistemaPrincipal.Aplicacion.VentanaActual.OnMouseUp += eventoMouseSoltado;

			DispararPropertyChanged(nameof(DatosDrag));
		} 

		#endregion

		#endregion
	}

	/// <summary>
	/// Clase que contiene los valores de las keys para los distintos argumentosExtra
	/// que puede llegar a tener un <see cref="Drag"/>
	/// </summary>
	public static class KeysParametrosDrag
	{
		public const int IndicePrametroExtra = 0;

		public const int IndiceParametroPosicionBloque = 1;
	}
}