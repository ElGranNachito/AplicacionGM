using System;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	//TODO:Agregar funcion para resetear indice

	public delegate void DValorSeleccionado(ViewModelItemAutocompletadoBase itemSeleccionado);

	/// <summary>
	/// Representa una ventana para el autocompletado de un campo
	/// </summary>
	public class ViewModelVentanaAutocompletado : ViewModel
	{
		#region Campos & Propiedades

		//----------------------------CAMPOS-----------------------------


		/// <summary>
		/// Total de los valores
		/// </summary>
		private List<ViewModelItemAutocompletadoBase> mValoresExistentes = new List<ViewModelItemAutocompletadoBase>();


		//-------------------------PROPIEDADES---------------------------

		/// <summary>
		/// Posicion de la ventana en el eje X
		/// </summary>
		public double PosicionX { get; set; }

		/// <summary>
		/// Posicion de la ventana en el eje Y
		/// </summary>
		public double PosicionY { get; set; }

		/// <summary>
		/// Texto actualmente ingresado por el usuario
		/// </summary>
		public string TextoActual { get; set; }

		/// <summary>
		/// IndiceZ del elemento actualmente seleccionado
		/// </summary>
		public int IndiceActual { get; set; } = -1;

		/// <summary>
		/// Indica si la ventana es visible
		/// </summary>
		public bool EsVisible { get; set; }

		/// <summary>
		/// <see cref="mValoresExistentes"/> que cumplen con el nombre y son seleccionables
		/// </summary>
		public ViewModelListaItemsAutocompletado Posibilidades { get; set; } = new ViewModelListaItemsAutocompletado();

		/// <summary>
		/// Totalidad de valores posibles con los que completar
		/// </summary>
		public List<ViewModelItemAutocompletadoBase> ValoresExistentes => mValoresExistentes; 

		/// <summary>
		/// <see cref="ViewModelItemAutocompletadoBase"/> actualmente seleccionado
		/// </summary>
		public ViewModelItemAutocompletadoBase ValorSeleccionado => 
			Posibilidades.Items[Math.Clamp(IndiceActual, 0, Posibilidades.Items.Count - 1)];

		#endregion

		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando se selecciona un valor
		/// </summary>
		public event DValorSeleccionado OnValorSeleccionado = delegate { };

		#endregion

		#region Metodos

		/// <summary>
		/// Actualiza <see cref="Posibilidades"/> en base al <paramref name="nuevoTexto"/> que el usuario
		/// ha ingresado
		/// </summary>
		/// <param name="nuevoTexto">Texto que ha ingresado el usuario</param>
		public void ActualizarTextoActual(string nuevoTexto)
		{
			TextoActual = nuevoTexto;

			//Si no hay texto...
			if (TextoActual.Length <= 0)
			{
				//Borramos los items posibles
				Posibilidades.Items.Clear();

				//Hacemos invisible el menu
				EsVisible = false;

				//Salimos
				return;
			}

			//Actualizamos las posibilidades
			if (ActualizarPosibilidades())
			{
				//Si no hay posibilidades...
				if (Posibilidades.Items.Count == 0)
				{
					//Hacemos invisible la ventana
					EsVisible = false;

					return;
				}

				if (!EsVisible)
					EsVisible = true;
			}
		}

		/// <summary>
		/// Actualiza <see cref="mValoresExistentes"/>
		/// </summary>
		/// <param name="nuevosValores">nueva coleccion de valores</param>
		public void ActualizarValoresExistentes(List<ViewModelItemAutocompletadoBase> nuevosValores)
		{
			if (nuevosValores != null)
				mValoresExistentes = nuevosValores;
			else
				mValoresExistentes.Clear();

			ResetearIndice();
		}

		/// <summary>
		/// Selecciona el valor señalado por el <see cref="IndiceActual"/>
		/// y levanta el evento <see cref="OnValorSeleccionado"/>
		/// </summary>
		public void SeleccionarValor()
		{
			OnValorSeleccionado(ValorSeleccionado);

			//Deseleccionamos el item
			ValorSeleccionado.EstaSeleccionado = false;

			//Hacemos insible la ventana
			EsVisible = false;

			mValoresExistentes.Clear();
			Posibilidades.Items.Clear();
		}

		/// <summary>
		/// Cambia el <see cref="IndiceActual"/>
		/// </summary>
		/// <param name="nuevoIndice">Nuevo valor</param>
		public void CambiarIndice(int nuevoIndice)
		{
			//Si hay uno o menos items o el indice es el mismo al actual entonces de nada sirve cambiar el indice
			if (nuevoIndice == IndiceActual || Posibilidades.Items.Count == 0)
				return;

			//Si el nuevo indice se sale del tamaño de la lista entonces lo hacemos 'dar la vuelta'
			if (nuevoIndice > Posibilidades.Items.Count - 1)
				nuevoIndice = 0;
			else if (nuevoIndice < 0)
				nuevoIndice = Posibilidades.Items.Count - 1;

			//Deseleccionamos el elemento actual
			ValorSeleccionado.EstaSeleccionado = false;

			//Cambiamos el indice
			IndiceActual = nuevoIndice;

			//Seleccionamos el nuevo elemento
			ValorSeleccionado.EstaSeleccionado = true;
		}

		/// <summary>
		/// Incrementa el indice actual
		/// </summary>
		public void IncrementarIndice() => CambiarIndice(IndiceActual + 1);

		/// <summary>
		/// Disminuye el indice actuañ
		/// </summary>
		public void DisminuirIndice() => CambiarIndice(IndiceActual - 1);

		/// <summary>
		/// Actualiza las <see cref="Posibilidades"/> dentro de <see cref="mValoresExistentes"/> en base al <see cref="TextoActual"/>
		/// </summary>
		protected bool ActualizarPosibilidades()
		{
			//Obtiene los valores que cumplen
			var posibilidades = (
				from vm in mValoresExistentes 
				where vm.Comparar(TextoActual)
				select vm).ToList();

			ViewModelItemAutocompletadoBase itemSeleccionadoActualmente = null;

			//Obtenemos el item seleccionado actualmente si la lista no esta vacia
			if (Posibilidades.Items.Count > 0)
				itemSeleccionadoActualmente = ValorSeleccionado;

			//Actualiza los valores de la lista de posibilidades
			Posibilidades.ActualizarItems(posibilidades);

			//Si luego de actualizar hay items posibles...
			if (Posibilidades.Items.Count > 0)
			{
				//Obtenemos el nuevo indice del item seleccionado
				int nuevoIndiceItem = Posibilidades.Items.IndexOf(itemSeleccionadoActualmente);

				if (nuevoIndiceItem == -1)
				{
					//El item ya no existe asi que seleccionamos el primer elemento
					CambiarIndice(0);

					return true;
				}

				//Si el indice cambio...
				if (nuevoIndiceItem != IndiceActual)
				{
					//Actualizamos el indice actual
					IndiceActual = nuevoIndiceItem;

					return true;
				}
			}

			//Si ya no hay items...
			if (Posibilidades.Items.Count == 0)
			{
				ResetearIndice();

				return true;
			}

			//No cambio la lista
			return false;
		}

		/// <summary>
		/// Coloca el indice en -1 para que la proxima vez que se abra
		/// la ventana el primer indice aparezca seleccionado
		/// </summary>
		private void ResetearIndice() => IndiceActual = -1;

		#endregion
	}
}