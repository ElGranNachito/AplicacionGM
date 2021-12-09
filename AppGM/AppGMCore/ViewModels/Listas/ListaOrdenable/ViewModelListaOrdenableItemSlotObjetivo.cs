namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ControladorSlot"/> en una <see cref="ViewModelListaOrdenable{TItems}"/> de objetivos de daño
	/// </summary>
	public class ViewModelListaOrdenableItemSlotObjetivo : ViewModelListaOrdenableItem<ViewModelListaOrdenableItemSlotObjetivo, ControladorSlot>
	{
		/// <summary>
		/// Contiene el valor de <see cref="ProfundidadTexto"/> parseado a un int
		/// </summary>
		public int profundidad;

		/// <summary>
		/// Numero de objetos almacenados en el <see cref="ControladorSlot"/> contenido
		/// </summary>
		public string NumeroDeObjetosAlmacenados => (Contenido.ControladoresItemsAlmacenados.Count + (Contenido.ParteDelCuerpoAlmacenada is not null ? 1 : 0)).ToString();

		/// <summary>
		/// Indica si tambien se debe dañar al contenido de este slot
		/// </summary>
		public bool DañarContenido { get; set; } = true;

		/// <summary>
		/// Propiedad bindeada al campo de texto de ingreso de profundidad
		/// </summary>
		public string ProfundidadTexto
		{
			get => profundidad.ToString();
			set => value.ParseToIntIfValid();
		}

		public ViewModelListaOrdenableItemSlotObjetivo(
			ControladorSlot _contenido,
			ViewModelListaOrdenable<ViewModelListaOrdenableItemSlotObjetivo, ControladorSlot> _contenedor) 
			
			: base(_contenido, _contenedor)
		{}
	}
}
