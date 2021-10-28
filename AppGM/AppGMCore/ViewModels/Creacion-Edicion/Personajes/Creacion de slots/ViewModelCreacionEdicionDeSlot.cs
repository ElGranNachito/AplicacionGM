using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para la creacion o edicion de un <see cref="ModeloSlot"/>
	/// </summary>
	class ViewModelCreacionEdicionDeSlot : ViewModelCreacionEdicionDeModelo<ModeloSlot, ControladorSlot, ViewModelCreacionEdicionDeSlot>
	{

		public ViewModelCreacionEdicionDeSlot(
			Action<ViewModelCreacionEdicionDeSlot> _accionSalir,
			ControladorSlot _controladorParaEditar,
			Type tipoValorPorDefectoModelo = null) 
			
			: base(_accionSalir, _controladorParaEditar, tipoValorPorDefectoModelo)
		{
		}

		public override ModeloSlot CrearModelo()
		{
			throw new NotImplementedException();
		}

		public override ControladorSlot CrearControlador()
		{
			throw new NotImplementedException();
		}
	}
}
