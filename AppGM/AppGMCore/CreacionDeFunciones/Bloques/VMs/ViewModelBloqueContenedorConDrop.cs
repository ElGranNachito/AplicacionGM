using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// clase base para <see cref="ViewModelBloqueFuncionBase"/> que contiene otros <see cref="ViewModelBloqueFuncionBase"/>
	/// </summary>
	public abstract class ViewModelBloqueContenedorConDrop<TipoBloque> : ViewModelBloqueContenedor<TipoBloque>
		where TipoBloque: BloqueBase
	{
		/// <summary>
		/// <see cref="ViewModelSeccionReceptoraDeDrag"/> encargado de recibir eventos de Drag & Drop
		/// para añadir bloques a este <see cref="ViewModelBloqueContenedorConDropConDrop{TipoBloque}"/>
		/// </summary>
		public ViewModelSeccionReceptoraDeDrag ReceptorAñadirBloque { get; set; }

		/// <summary>
		/// Margen izquierdo del contenido
		/// </summary>
		public Grosor MargenContenido { get; set; } = new Grosor(20, 0, 0, 0);

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene este bloque</param>
		public ViewModelBloqueContenedorConDrop(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion, int _idBloque = -1)
			:base(_vmCreacionDeFuncion, _idBloque)
		{
			//Handler para cuando el usuario suelta un elemento sobre el ReceptorAñadirBloque
			DDragHandlerElementoSoltado handlerElementoSoltado = contenido =>
			{
				if (contenido is ViewModelBloqueFuncionBase bloque)
				{
					AñadirBloque(bloque.Copiar(), -1);

					MostrarEspacioDrop = false;
					ReceptorAñadirBloque.EsVisible = false;

					return true;
				}

				return false;
			};

			ReceptorAñadirBloque = new ViewModelSeccionReceptoraDeDrag(handlerElementoSoltado, null, null);
		}

		#region Metodos
		public override void OnDragEntro_Impl(IDrageable vm)
		{
			base.OnDragEntro_Impl(vm);

			ReceptorAñadirBloque.EsVisible = true;
		}

		public override void OnDragSalio_Impl(IDrageable vm)
		{
			base.OnDragSalio_Impl(vm);

			ReceptorAñadirBloque.EsVisible = false;
		}

		public override bool OnDrop_Impl(IDrageable vm)
		{
			ReceptorAñadirBloque.EsVisible = false;

			return base.OnDrop_Impl(vm);
		}

		protected override void EstablecerIndiceZ(int nuevoIndice)
		{
			if (nuevoIndice == IndiceZ)
				return;

			mIndiceZ = nuevoIndice;

			//Actualizamos el indice z de los bloques que contiene
			foreach (var bloque in Bloques)
				bloque.IndiceZ = IndiceZ + 1;

			//Actualizamos el indice z del receptor de bloques
			ReceptorAñadirBloque.IndiceZ = IndiceZ + 1;
		}

		#endregion
	}
}