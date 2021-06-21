using System.Collections.Generic;
using System.Linq;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// clase base para <see cref="ViewModelBloqueFuncionBase"/> que contiene otros <see cref="ViewModelBloqueFuncionBase"/>
	/// </summary>
	public abstract class ViewModelBloqueContenedor<TipoBloque> : ViewModelBloqueFuncion<TipoBloque>, IContenedorDeBloques
		where TipoBloque: BloqueBase
	{
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> Bloques { get; set; } =
			new ViewModelListaDeElementos<ViewModelBloqueFuncionBase>();

		/// <summary>
		/// <see cref="ViewModelBloqueDeclaracionVariable"/> creados en este contenedor
		/// </summary>
		public List<ViewModelBloqueDeclaracionVariable> VariablesCreadas = 
			new List<ViewModelBloqueDeclaracionVariable>();

		/// <summary>
		/// <see cref="ViewModelSeccionReceptoraDeDrag"/> encargado de recibir eventos de Drag & Drop
		/// para añadir bloques a este <see cref="ViewModelBloqueContenedor"/>
		/// </summary>
		public ViewModelSeccionReceptoraDeDrag ReceptorAñadirBloque { get; set; }

		/// <summary>
		/// Margen izquierdo del contenido
		/// </summary>
		public Grosor MargenContenido { get; set; } = new Grosor(20, 0, 0, 0);

		IContenedorDeBloques IContenedorDeBloques.Padre
		{
			get => Padre;
			set => Padre = value;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene este bloque</param>
		public ViewModelBloqueContenedor(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion)
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
			ReceptorAñadirBloque.IndiceZ = IndiceZ + 1;
		}

		#region Metodos

		public List<BloqueVariable> ObtenerVariables(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables)
		{
			//Obtenemos las variables del padre a las que podemos acceder
			var variablesPadre = mPadre.ObtenerVariables(this);

			//Añadimos a esas variables las declaradas en este mismo bloque
			variablesPadre.AddRange(
				VariablesCreadas.FindAll(elemento => elemento.EsValido)
					.Select(elemento => elemento.GenerarBloque_Impl()));

			return variablesPadre;
		}

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

		public void AñadirBloque(ViewModelBloqueFuncionBase bloque, int indice)
		{
			bloque.Padre = this;

			if (indice != -1)
			{
				((IContenedorDeBloques)this).InsertarBloque(bloque, indice);

				return;
			}

			bloque.IndiceBloque = Bloques.Count;
			Bloques.Add(bloque);
		}

		public void QuitarBloque(ViewModelBloqueFuncionBase bloque)
		{
			Bloques.Remove(bloque);

			((IContenedorDeBloques)this).ActualizarIndicesBloques(bloque.IndiceBloque);
		}

		#endregion
	}
}