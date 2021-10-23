﻿using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ControladorEfecto"/> en un <see cref="ViewModelCrearHabilidad"/>
	/// </summary>
	public class ViewModelEfectoItem : ViewModelItemListaControlador<ViewModelEfectoItem, ControladorEfecto>
	{
		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorEfecto">Controlador del efecto que representara esta instancia</param>
		public ViewModelEfectoItem(ControladorEfecto _controladorEfecto, bool _mostrarBotonesLaterales = true)
			: base(_controladorEfecto)
		{
			ControladorGenerico = _controladorEfecto;
		} 

		#endregion

		#region Metodos

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.AddRange(new ViewModelCaracteristicaItem[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = ControladorGenerico.NombreEfecto
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorGenerico.TipoEfecto.ToString()
				}
			});
		} 

		#endregion
	}
}
