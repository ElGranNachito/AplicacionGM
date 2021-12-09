using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica para <see cref="ModeloSlot"/>
	/// </summary>
	public partial class ModeloSlot
	{
		/// <summary>
		/// Contiene el valor de <see cref="PersonajeContenedor"/>
		/// </summary>
		private ModeloPersonaje mPersonajeContenedor;

		/// <summary>
		/// Espacio disponible en el slot
		/// </summary>
		[NotMapped]
		public decimal EspacioDisponible => EspacioTotal - EspacioOcupado;

		/// <summary>
		/// Espacio ocupado del slot
		/// </summary>
		[NotMapped]
		public decimal EspacioOcupado => ItemsAlmacenados.Sum(i => i.EspacioQueOcupa);

		IModeloConSlots ObtenerModeloDueño() => ItemDueño == null ? ParteDelCuerpoDueña : ItemDueño;

		/// <summary>
		/// Obtiene la profundidad de este slot en el inventario del <see cref="PersonajeContenedor"/>
		/// </summary>
		/// <param name="profundidadActual">Nivel actual de profundidad. Utilizado para recursion</param>
		/// <returns>Profundidad de este slot</returns>
		public int ObtenerProfundidad(int profundidadActual = 0)
		{
			var dueñoActual = ObtenerModeloDueño();

			if (dueñoActual != null)
				return dueñoActual.ObtenerProfundidad(++profundidadActual);

			return profundidadActual;
		}

		/// <summary>
		/// Obtiene todos los <see cref="ModeloItem"/> almacenados por este slot y subslots
		/// </summary>
		/// <param name="incluirTodoElArbol">Si es verdadero, devuelve el arbol de items completo</param>
		/// <param name="profundidadMaxima">Si es distinto de -1, indica hasta que nivel del arbol debemos llegar</param>
		/// <returns><see cref="List{T}"/> de <see cref="ModeloItem"/> contenidos por este slot</returns>
		public List<ModeloItem> ObtenerItems(bool incluirTodoElArbol, int profundidadMaxima = -1)
		{
			var resultado = ObtenerItems_Interno(incluirTodoElArbol, profundidadMaxima, new List<ModeloItem>());

			resultado.TrimExcess();

			return resultado;
		}

		/// <summary>
		/// Obtiene todas las <see cref="ModeloParteDelCuerpo"/> almacenados por este slot y subslots
		/// </summary>
		/// <param name="incluirTodoElArbol">Si es verdadero, devuelve el arbol de partes del cuerpo completo</param>
		/// <param name="profundidadMaxima">Si es distinto de -1, indica hasta que nivel del arbol debemos llegar</param>
		/// <returns><see cref="List{T}"/> de <see cref="ModeloItem"/> contenidos por este slot</returns>
		public List<ModeloParteDelCuerpo> ObtenerPartesDelCuerpo(bool incluirTodoElArbol, int profundidadMaxima = -1)
		{
			var resultado = ObtenerPartesDelCuerpo_Interno(incluirTodoElArbol, profundidadMaxima, new List<ModeloParteDelCuerpo>());

			resultado.TrimExcess();

			return resultado;
		}

		/// <summary>
		/// Implementacion de <see cref="ObtenerItems"/>
		/// </summary>
		private List<ModeloItem> ObtenerItems_Interno(bool incluirTodoElArbol, int profundidadMaxima, List<ModeloItem> resultado)
		{
			//Si no debemos incluir el arbol completo, o la profundidad maxima es cero, devolvemos tan solo los items del slot actual
			if (!incluirTodoElArbol || profundidadMaxima == 0)
			{
				resultado.AddRange(ItemsAlmacenados);

				return resultado;
			}

			foreach (var item in ItemsAlmacenados)
			{
				resultado.Add(item);

				foreach (var slot in item.Slots)
				{
					resultado.AddRange(slot.ObtenerItems_Interno(true, profundidadMaxima > 0 ? profundidadMaxima - 1 : -1, resultado));
				}
			}

			if (ParteDelCuerpoAlmacenada is not null)
			{
				foreach (var slot in ParteDelCuerpoAlmacenada.Slots)
				{
					resultado.AddRange(slot.ObtenerItems_Interno(true, profundidadMaxima > 0 ? profundidadMaxima - 1 : -1, resultado));
				}
			}

			return resultado;
		}

		/// <summary>
		/// Implementacion de <see cref="ObtenerPartesDelCuerpo"/>
		/// </summary>
		private List<ModeloParteDelCuerpo> ObtenerPartesDelCuerpo_Interno(bool incluirTodoElArbol, int profundidadMaxima, List<ModeloParteDelCuerpo> resultado)
		{
			//Si no debemos incluir el arbol completo, o la profundidad maxima es cero, devolvemos tan solo los items del slot actual
			if ((!incluirTodoElArbol || profundidadMaxima == 0) && ParteDelCuerpoAlmacenada is not null)
			{
				if (ParteDelCuerpoAlmacenada is not null)
				{
					resultado.Add(ParteDelCuerpoAlmacenada);

					return resultado;
				}
			}

			if (ParteDelCuerpoAlmacenada is null)
				return resultado;

			resultado.Add(ParteDelCuerpoAlmacenada);

			foreach (var slot in ParteDelCuerpoAlmacenada.Slots)
			{
				resultado.AddRange(slot.ObtenerPartesDelCuerpo_Interno(true, profundidadMaxima > 0 ? profundidadMaxima - 1 : -1, resultado));
			}

			return resultado;
		}
	}
}