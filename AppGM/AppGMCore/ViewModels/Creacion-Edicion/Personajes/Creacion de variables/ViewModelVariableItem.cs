﻿using System;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ModeloVariable{TipoVariable}"/> en una lista
	/// </summary>
	public class ViewModelVariableItem : ViewModelItemListaControlador<ViewModelVariableItem, ControladorVariableBase>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_variable">Controlador de la variable que sera representada por este vm</param>
		public ViewModelVariableItem(ControladorVariableBase _variable)
			:base(_variable){}

		#region Metodos

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.Elementos.Clear();

			CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>(new[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre variable",
					Valor = ControladorGenerico.NombreVariable
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo variable",
					Valor = ControladorGenerico.TipoVariable.ToString(),
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Valor actual",
					Valor = ControladorGenerico.ObtenerValorVariable()?.ToString() ?? "No disponible"
				}
			});
		}

		protected override void ActualizarGruposDeBotones()
		{
			Action accionBotonEditar = async () =>
			{
				SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionDeVariable, ModeloVariableBase, ControladorVariableBase>(await new ViewModelCreacionDeVariable(vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
                    {
						vm.CrearControlador();

						ActualizarCaracteristicas();
                    }

				}, ControladorGenerico).Inicializar(typeof(ModeloVariableInt)));
			};

			Action accionBotonEliminar = () =>
			{
				ControladorGenerico.Eliminar(true);
			};

			CrearBotonesParaEditarYEliminar(accionBotonEditar, accionBotonEliminar);

			IndiceGrupoDeBotonesActivo = 0;
		}

		#endregion
	}
}