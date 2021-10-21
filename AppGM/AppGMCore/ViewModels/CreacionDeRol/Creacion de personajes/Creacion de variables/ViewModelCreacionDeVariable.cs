using System;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion/edicion de un <see cref="ControladorVariable{TipoVariable}"/>
	/// </summary>
	public class ViewModelCreacionDeVariable : ViewModelConResultado<ViewModelCreacionDeVariable>
	{
		#region Campos & Propiedades

		//----------------------------------------CAMPOS---------------------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="EsLista"/>
		/// </summary>
		private bool mEsLista;

		/// <summary>
		/// Controlador de la variable que esta siendo editada
		/// </summary>
		public readonly ControladorVariableBase variableSiendoEditada;

		//-------------------------------------PROPIEDADES-------------------------------------

		/// <summary>
		/// Modelo de variable creado
		/// </summary>
		public ModeloVariableBase ModeloVariable { get; private set; }

		/// <summary>
		/// Indica si la variable ya puede ser creada
		/// </summary>
		public bool EsValido { get; private set; }

		/// <summary>
		/// Indica si se esta editando un <see cref="ModeloVariable"/> existente
		/// </summary>
		public bool EstaEditando => variableSiendoEditada != null;

		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string NombreVariable
		{
			get => ModeloVariable.NombreVariable;
			set
			{
				if (value == ModeloVariable.NombreVariable)
					return;

				ModeloVariable.NombreVariable = value;

				ActualizarValidez();
			}
		}

		/// <summary>
		/// Descripcion de la variable
		/// </summary>
		public string DescripcionVariable
		{
			get => ModeloVariable.DescripcionVariable;
			set => ModeloVariable.DescripcionVariable = value;
		}

		/// <summary>
		/// Indica si esta variable es una lista
		/// </summary>
		public bool EsLista
		{
			get => mEsLista;
			set
			{
				if (value == mEsLista)
					return;

				mEsLista = value;

				VMIngresoVariable.EsLista = mEsLista;
			}
		}

		/// <summary>
		/// Indica si esta variable puede ser una lista
		/// </summary>
		public bool PuedeSerLista => TipoSeleccionado == typeof(ControladorPersonaje) ||
		                             TipoSeleccionado == typeof(ControladorUtilizable);

		/// <summary>
		/// Tipos disponibles para que seleccione el usuario
		/// </summary>
		public ViewModelComboBox<Type> ComboBoxTiposDisponibles { get; set; } =
			new ViewModelComboBox<Type>(new List<ViewModelItemComboBoxBase<Type>>
			{
				new ViewModelItemComboBoxBase<Type>(typeof(int), "Int"),
				new ViewModelItemComboBoxBase<Type>(typeof(float), "Float"),
				new ViewModelItemComboBoxBase<Type>(typeof(string), "String"),
				new ViewModelItemComboBoxBase<Type>(typeof(ControladorPersonaje), "Personaje"),
				new ViewModelItemComboBoxBase<Type>(typeof(ControladorUtilizable), "Item")
			});

		/// <summary>
		/// Tipo actualmente seleccionado
		/// </summary>
		public Type TipoSeleccionado
		{
			get => ModeloVariable.TipoVariable;
			set
			{
				if(value == ModeloVariable.TipoVariable)
					return;

				ModeloVariable.TipoVariableString = value.AssemblyQualifiedName;

				if (VMIngresoVariable != null)
					VMIngresoVariable.OnEsValidoCambio -= ActualizarValidez;

				VMIngresoVariable = new ViewModelIngresoVariable(ModeloVariable.TipoVariable, EsLista, true);

				VMIngresoVariable.OnEsValidoCambio += ActualizarValidez;

				DispararPropertyChanged(nameof(PuedeSerLista));
			}
		}

		/// <summary>
		/// Viewmodel del control de ingreso de variable
		/// </summary>
		public ViewModelIngresoVariable VMIngresoVariable { get; set; }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecutara al salir del control representado por este view model</param>
		/// <param name="_variableEditar">Controlador de la variable que estamos editando</param>
		public ViewModelCreacionDeVariable(Action<ViewModelCreacionDeVariable> _accionSalir, ControladorVariableBase _variableEditar = null)
			:base(_accionSalir)
		{
			variableSiendoEditada = _variableEditar;

			ComboBoxTiposDisponibles.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				TipoSeleccionado = actual.valor;
			};

			if (EstaEditando)
			{
				ModeloVariable = variableSiendoEditada.modelo.CrearCopiaProfundaEnSubtipo(variableSiendoEditada.modelo.GetType()) as ModeloVariableBase;

				TipoSeleccionado = variableSiendoEditada.TipoVariable;
			}
			else
			{
				ModeloVariable = new ModeloVariableInt();

				//Colocamos como valor por defecto el primer elemento de los tipos posibles
				ComboBoxTiposDisponibles.ValorSeleccionado = ComboBoxTiposDisponibles.ValoresPosibles.First();
			}

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(EsValido))
					return;

				ActualizarValidez();
			};

			ComandoAceptar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Aceptar;

				mAccionSalir(this);
			});

			ComandoCancelar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Cancelar;

				mAccionSalir(this);
			});
		}

		#region Metodos

		/// <summary>
		/// Crea un <see cref="ModeloVariableBase"/> que representa a la variable creada
		/// </summary>
		/// <returns><see cref="ModeloVariableBase"/> que representa a la variable creada o null si <see cref="EsValido"/> es false</returns>
		public ControladorVariableBase CrearVariable()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			//Controlador que sera devuelto por la funcion
			ControladorVariableBase controladorVariableFinal;

			//Si estamos editando...
			if (EstaEditando)
			{
				ModeloVariable.CrearCopiaProfundaEnSubtipo(ModeloVariable.GetType(), variableSiendoEditada.modelo);

				controladorVariableFinal = variableSiendoEditada;
			}
			//Sino
			else
			{
				var modeloCreado = ControladorVariableBase.CrearModeloCorrespondiente(TipoSeleccionado, -1, NombreVariable);

				modeloCreado.DescripcionVariable = DescripcionVariable;

				controladorVariableFinal = ControladorVariableBase.CrearControladorCorrespondiente(modeloCreado);
			}

			//Guardamos el valor ingresado por el usuario
			controladorVariableFinal.GuardarValorVariable(VMIngresoVariable.ObtenerValor());

			return controladorVariableFinal;
		}

		/// <summary>
		/// Actualiza el valor de <see cref="EsValido"/>
		/// </summary>
		private void ActualizarValidez()
		{
			if (VMIngresoVariable is null or { EsValido: false })
			{
				EsValido = false;

				return;
			}

			if (NombreVariable.IsNullOrWhiteSpace())
			{
				EsValido = false;

				return;
			}

			EsValido = true;
		} 

		#endregion
	}
}