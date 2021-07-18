using System.Collections.Generic;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un metodo que lidia con eventos de modificacion de la lista de bloques contenidos
	/// </summary>
	/// <param name="bloque"><see cref="ViewModelBloqueFuncionBase"/> que fue modificado</param>
	/// <param name="padre"><see cref="IContenedorDeBloques"/> del <paramref name="bloque"/></param>
	public delegate void BloquesContenidosModificados(ViewModelBloqueFuncionBase bloque, IContenedorDeBloques padre);

	/// <summary>
	/// <see cref="ViewModel"/> que representa un control para la creacion de una funcion
	/// </summary>
	public abstract class ViewModelCreacionDeFuncionBase: ViewModel, IReceptorDeDrag, IContenedorDeBloques
	{
		/// <summary>
		/// Evento que se dispara cuando un bloque es removido
		/// </summary>
		public event BloquesContenidosModificados OnBloqueRemovido = delegate {};

		/// <summary>
		/// Evento que se dispara cuando un bloque es añadido
		/// </summary>
		public event BloquesContenidosModificados OnBloqueAñadido = delegate {};

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

		public List<BloqueVariable> ObtenerVariables(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables)
		{
			var variables = VariablesBase;

			var variablesCreadasValidas = VariablesCreadas;

			if(bloqueQueIntentaObtenerLasVariables != null)
				variablesCreadasValidas.RemoveAll(variable => !variable.EsValido || bloqueQueIntentaObtenerLasVariables.IndiceBloque < variable.IndiceBloque);

			variables = variables.Concat(variablesCreadasValidas.Select(elemento => elemento.GenerarBloque_Impl())).ToList();

			return variables;
		}

		public void AñadirBloque(ViewModelBloqueFuncionBase bloque, int indice)
		{
			bloque.Padre = this;

			if(SistemaPrincipal.Drag.HayUnDragActivo)
				GrosorBordesGridBloquesColocados = new Grosor(1);

			if (bloque is ViewModelBloqueDeclaracionVariable var)
				VariablesCreadas.Add(var);

			if (indice != -1)
			{
				Base<IContenedorDeBloques>()?.InsertarBloque(bloque, indice);

				DispararBloqueAñadido(bloque, this);

				return;
			}

			bloque.IndiceBloque = Bloques.Count;
			bloque.IndiceZ      = 1;

			Bloques.Add(bloque);

			DispararBloqueAñadido(bloque, this);
		}

		public void QuitarBloque(ViewModelBloqueFuncionBase bloque)
		{
			Bloques.Remove(bloque);

			DispararBloqueRemovido(bloque, this);

			Base<IContenedorDeBloques>().ActualizarIndicesBloques(bloque.IndiceBloque);
		}

		/// <summary>
		/// Dispara <see cref="OnBloqueAñadido"/>
		/// </summary>
		/// <param name="bloqueAñadido"><see cref="ViewModelBloqueFuncionBase"/> que fue añadido</param>
		/// <param name="padre"><see cref="IContenedorDeBloques"/> del <paramref name="bloqueAñadido"/></param>
		public void DispararBloqueAñadido(ViewModelBloqueFuncionBase bloqueAñadido, IContenedorDeBloques padre) => OnBloqueAñadido(bloqueAñadido, padre);

		/// <summary>
		/// Dispara <see cref="OnBloqueRemovido"/>
		/// </summary>
		/// <param name="bloqueRemovido"><see cref="ViewModelBloqueFuncionBase"/> que fue removido</param>
		/// <param name="padre"><see cref="IContenedorDeBloques"/> del <paramref name="bloqueRemovido"/></param>
		public void DispararBloqueRemovido(ViewModelBloqueFuncionBase bloqueRemovido, IContenedorDeBloques padre) => OnBloqueRemovido(bloqueRemovido, padre);

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