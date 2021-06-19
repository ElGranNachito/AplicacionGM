using System.Collections.Generic;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa un control para la creacion de una funcion
	/// </summary>
	public abstract class ViewModelCreacionDeFuncionBase: ViewModel, IReceptorDeDrag
	{
		#region Propiedades & Campos

		/// <summary>
		/// Bloques disponibles para el uso del usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> BloquesDisponibles { get; set; }

		/// <summary>
		/// Bloques colocados por el usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> BloquesColocados { get; set; } = new ViewModelListaDeElementos<ViewModelBloqueFuncionBase>();

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

		public void OnDragEnter(IDrageable vm)
		{
			GrosorBordesGridBloquesColocados = new Grosor(5);
		}

		public void OnDragLeave(IDrageable vm)
		{
			GrosorBordesGridBloquesColocados = new Grosor(1);
		}

		public bool OnDrop(IDrageable vm)
		{
			//Nos aseguramos de que vm dropeado sea del tipo adecuado
			if (vm is ViewModelBloqueFuncionBase vmBloque)
			{
				GrosorBordesGridBloquesColocados = new Grosor(1);

				ViewModelBloqueFuncionBase nuevoVMBloque = null;

				bool esBloqueDeMuestra = (bool)(SistemaPrincipal.Drag[KeysParametrosDrag.IndicePrametroExtra] ?? false);

				if (esBloqueDeMuestra)
				{
					switch (vm)
					{
						case ViewModelBloqueDeclaracionVariable:
							nuevoVMBloque = new ViewModelBloqueDeclaracionVariable(this);
							break;
						case ViewModelBloqueLlamarFuncion:
							nuevoVMBloque = new ViewModelBloqueLlamarFuncion(this);
							break;
						default:
							SistemaPrincipal.LoggerGlobal.Log(
								$"Se intento dropear un {vmBloque.GetType()} pero no esta soportado",
								ESeveridad.Advertencia);
							return false;
					}
				}
				else
				{
					BloquesColocados.Remove(vmBloque);

					nuevoVMBloque = vmBloque;
				}

				int indiceBloque = -1;

				if (SistemaPrincipal.Drag.ParametroAsignado(KeysParametrosDrag.IndiceParametroPosicionBloque))
					indiceBloque = (int)SistemaPrincipal.Drag[KeysParametrosDrag.IndiceParametroPosicionBloque];

				//Si el indice del bloque es -1 entonces no estamos colocando sobre otro bloque...
				if (indiceBloque == -1)
				{
					//Colocamos el indice al ultimo
					nuevoVMBloque.IndiceBloque = BloquesColocados.Count;

					BloquesColocados.Add(nuevoVMBloque);
				}
				//Sino entonces el drag fue soltado sobre un bloque existente
				else
				{
					//Hacemos que el indice del nuevo bloque sea igual al del bloque sobre el que fue sotado
					nuevoVMBloque.IndiceBloque = indiceBloque;

					BloquesColocados.Insert(nuevoVMBloque.IndiceBloque, nuevoVMBloque);

					ActualizarIndicesBloques(0);
				}
			}

			SistemaPrincipal.LoggerGlobal.Log($"Se intento dropear un {vm.GetType()} pero no esta soportado", ESeveridad.Advertencia);

			return false;
		}

		#endregion

		/// <summary>
		/// Actualiza los <see cref="ViewModelBloqueFuncionBase.indiceBloque"/> de todos los <see cref="ViewModelBloqueFuncionBase"/>
		/// en <see cref="BloquesColocados"/> a partir de <paramref name="indicePorElQueComenzar"/>
		/// </summary>
		/// <param name="indicePorElQueComenzar">IndiceZ a partir del cual comenzar a actualizar</param>
		public void ActualizarIndicesBloques(int indicePorElQueComenzar)
		{
			for (int i = indicePorElQueComenzar; i < BloquesColocados.Count; ++i)
				BloquesColocados[i].IndiceBloque = i;
		}

		#endregion
	}
}