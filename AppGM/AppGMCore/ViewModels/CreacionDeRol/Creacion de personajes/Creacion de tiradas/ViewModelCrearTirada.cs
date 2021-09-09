using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion de una tirada
	/// </summary>
	public class ViewModelCrearTirada : ViewModelConResultado<ViewModelCrearTirada>
	{
		/// <summary>
		/// Nombre de la tirada
		/// </summary>
		public string Nombre { get; set; }

		/// <summary>
		/// Descripcion de la tirada
		/// </summary>
		public string Descripcion { get; set; }

		/// <summary>
		/// Cadena que representa a la tirada
		/// </summary>
		public string Tirada { get; set; }

		/// <summary>
		/// VM para el combobox de seleccion de tipo de tirada
		/// </summary>
		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada { get; set; } =
			new ViewModelComboBox<ETipoTirada>(EnumHelpers.TiposDeTiradasDisponibles);

		/// <summary>
		/// VM para el combobox de seleccion de la stat de la tirada
		/// </summary>
		public ViewModelComboBox<EStat> ViewModelComboBoxStatTirada { get; set; } =
			new ViewModelComboBox<EStat>(EnumHelpers.StatsDisponibles);

		/// <summary>
		/// VM para el combobox de seleccion del tipo de daño de la tirada
		/// </summary>
		public ViewModelComboBox<ETipoDeDaño> ViewModelComboBoxTipoDeDañoTirada { get; set; } =
			new ViewModelComboBox<ETipoDeDaño>(EnumHelpers.TiposDeDañoDisponibles);

		public ViewModelCrearTirada()
		{
			
		}
	}
}
