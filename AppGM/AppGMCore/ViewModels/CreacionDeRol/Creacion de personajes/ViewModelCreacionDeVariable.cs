using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	public class ViewModelCreacionDeVariable : ViewModelConResultado<ViewModelCreacionDeVariable>
	{
		#region Propiedades

		public ControladorVariableBase VariableSiendoEditada { get; set; }

		public ModeloVariableBase VariableCreada { get; set; }

		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string NombreVarialbe { get; set; }

		/// <summary>
		/// Descripcion de la variable
		/// </summary>
		public string DescripcionVariable { get; set; }

		/// <summary>
		/// Indica si esta variable es una lista
		/// </summary>
		public bool EsLista { get; set; }

		/// <summary>
		/// Tipos disponibles para que seleccione el usuario
		/// </summary>
		public ViewModelComboBox<ViewModelItemComboBoxBase<Type>> ComboBoxTiposDisponibles { get; set; } =
			new ViewModelComboBox<ViewModelItemComboBoxBase<Type>>(new List<ViewModelItemComboBoxBase<Type>>
			{
				new ViewModelItemComboBoxBase<Type>(typeof(int), "Int"),
				new ViewModelItemComboBoxBase<Type>(typeof(float), "Float"),
				new ViewModelItemComboBoxBase<Type>(typeof(ControladorPersonaje), "Personaje"),
				new ViewModelItemComboBoxBase<Type>(typeof(ControladorUtilizable), "Item")
			}); 

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecutara al salir del control representado por este view model</param>
		/// <param name="_variableEditar">Controlador de la variable que estamos editando</param>
		public ViewModelCreacionDeVariable(Action<ViewModelCreacionDeVariable> _accionSalir, ControladorVariableBase _variableEditar = null)
			:base(_accionSalir)
		{
			VariableSiendoEditada = _variableEditar;
		}
	}
}
