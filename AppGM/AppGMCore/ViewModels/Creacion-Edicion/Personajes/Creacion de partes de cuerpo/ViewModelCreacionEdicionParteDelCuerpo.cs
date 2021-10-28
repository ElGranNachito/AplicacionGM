using System;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel utilizado para crear o editar un <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	class ViewModelCreacionEdicionParteDelCuerpo : ViewModelCreacionEdicionDeModelo<ModeloParteDelCuerpo, ControladorParteDelCuerpo, ViewModelCreacionEdicionParteDelCuerpo>
	{
		public ViewModelCreacionEdicionParteDelCuerpo(
			Action<ViewModelCreacionEdicionParteDelCuerpo> _accionSalir, 
			ControladorParteDelCuerpo _controladorParaEditar, 
			Type tipoValorPorDefectoModelo = null) 
			
			: base(_accionSalir, _controladorParaEditar, tipoValorPorDefectoModelo)
		{
		}

		public override ModeloParteDelCuerpo CrearModelo()
		{
			throw new NotImplementedException();
		}

		public override ControladorParteDelCuerpo CrearControlador()
		{
			throw new NotImplementedException();
		}
	}
}
