using System.Collections.Generic;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa un control para la creacion de una funcion
	/// </summary>
	public abstract class ViewModelCreacionDeFuncionBase: ViewModel, IReceptorDeDrag, IContenedorDeBloques
	{
		#region Propiedades & Campos

		/// <summary>
		/// El padre de una instancia de este <see cref="ViewModel"/> siempre es null
		/// </summary>
		public IContenedorDeBloques Padre => null;

		/// <summary>
		/// Bloques disponibles para el uso del usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> BloquesDisponibles { get; set; }

		/// <summary>
		/// Bloques colocados por el usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> Bloques { get; set; } = new ViewModelListaDeElementos<ViewModelBloqueFuncionBase>();

		/// <summary>
		/// Variables base, es decir variables por defecto
		/// </summary>
		public List<BloqueVariable> VariablesBase { get; set; }

		/// <summary>
		/// Variables creadas por el usuario
		/// </summary>
		public List<ViewModelBloqueDeclaracionVariable> VariablesCreadas { get; set; } = new List<ViewModelBloqueDeclaracionVariable>();

		/// <summary>
		/// Ventana de autocompletado
		/// </summary>
		public ViewModelVentanaAutocompletado Autocompletado { get; set; } = new ViewModelVentanaAutocompletado();

		/// <summary>
		/// <see cref="Grosor"/> de los bordes del grid que contiene los <see cref="BloquesColocados"/>
		/// </summary>
		public Grosor GrosorBordesGridBloquesColocados { get; set; }

		public int IndiceZ { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor base
		/// </summary>
		public ViewModelCreacionDeFuncionBase()
		{
			BloquesDisponibles = new ViewModelListaDeElementos<ViewModelBloqueFuncionBase>(AsignarListaDeBloques());
			VariablesBase = AsignarVariablesBase();
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Lista <see cref="VariablesBase"/> y <see cref="VariablesCreadas"/>
		/// </summary>
		/// <param name="bloqueQueIntentaObtenerLasVariables"><see cref="ViewModelBloqueFuncionBase"/> desde el cual se llamo la funcion</param>
		/// <returns><see cref="List{T}"/> de <see cref="BloqueVariable"/> que abarca todas las variables</returns>
		public List<BloqueVariable> ObtenerVariables(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables)
		{
			var variables = VariablesBase;

			var variablesCreadasValidas = VariablesCreadas;

			variablesCreadasValidas.RemoveAll(variable => !variable.EsValido || bloqueQueIntentaObtenerLasVariables.IndiceBloque > variable.IndiceBloque);

			variables = variables.Concat(variablesCreadasValidas.Select(elemento => elemento.GenerarBloque_Impl())).ToList();

			return variables;
		}

		public void AñadirBloque(ViewModelBloqueFuncionBase bloque, int indice)
		{
			bloque.Padre = this;

			if(SistemaPrincipal.Drag.HayUnDragActivo)
				GrosorBordesGridBloquesColocados = new Grosor(1);

			if (indice != -1)
			{
				Base<IContenedorDeBloques>()?.InsertarBloque(bloque, indice);

				return;
			}

			bloque.IndiceBloque = Bloques.Count;
			bloque.IndiceZ      = 1;

			Bloques.Add(bloque);
		}

		public void QuitarBloque(ViewModelBloqueFuncionBase bloque)
		{
			Bloques.Remove(bloque);

			((IContenedorDeBloques)this).ActualizarIndicesBloques(bloque.IndiceBloque);
		}

		/// <summary>
		/// Metodo que debe asignar una lista de los <see cref="ViewModelBloqueFuncionBase"/> disponibles a
		/// <see cref="BloquesDisponibles"/>.
		/// No hacer nada extravagante en esta funcion puesto que se la llama desde el constructor base,
		/// es decir no llamar propiedades de la clase cuya inicializacion dependa del constructor
		/// </summary>
		/// <returns><see cref="List{T}"/> que se asignara a <see cref="BloquesDisponibles"/></returns>
		protected abstract List<ViewModelBloqueFuncionBase> AsignarListaDeBloques();

		/// <summary>
		/// Metodo que devuelve una <see cref="List{T}"/> de <see cref="BloqueVariable"/>
		/// que compondran las variables base
		/// </summary>
		/// <returns><see cref="List{T}"/> que se asignara a <see cref="VariablesBase"/></returns>
		protected abstract List<BloqueVariable> AsignarVariablesBase();

		#region Implementacion IReceptorDeDrag

		public void OnDragEntro_Impl(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase)
				GrosorBordesGridBloquesColocados = new Grosor(5);
		}

		public void OnDragSalio_Impl(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase)
				GrosorBordesGridBloquesColocados = new Grosor(1);
		}

		public bool OnDrop_Impl(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase bloque)
			{
				AñadirBloque(bloque.Copiar(), -1);

				return true;
			}

			SistemaPrincipal.LoggerGlobal.Log($"Se intento dropear un {vm.GetType()} pero no esta soportado", ESeveridad.Advertencia);

			return false;
		}

		#endregion

		#endregion
	}
}