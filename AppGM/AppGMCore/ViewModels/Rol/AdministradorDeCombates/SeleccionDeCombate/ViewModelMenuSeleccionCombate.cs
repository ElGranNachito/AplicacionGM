

using System.Collections.Generic;

namespace AppGM.Core
{
    public class ViewModelMenuSeleccionCombate : BaseViewModel
    {
        #region Propiedades
        public ViewModelListaCombates Combates { get; set; }
        public ViewModelGlobo<ViewModelInfoCombateGlobo> GloboInfoCombate { get; set; }
        #endregion

        #region Constructores

        public ViewModelMenuSeleccionCombate(List<ControladorAdministradorDeCombate> _combates)
        {
            Combates = new ViewModelListaCombates(_combates);
        }
        public ViewModelMenuSeleccionCombate()
        {
            Combates = new ViewModelListaCombates
            {
                Combates = new List<ViewModelCombateItem>
                {
                    new ViewModelCombateItem(),
                    new ViewModelCombateItem(),
                    new ViewModelCombateItem()
                }
            };

            GloboInfoCombate = new ViewModelGlobo<ViewModelInfoCombateGlobo>
            {
                ViewModelContenido = new ViewModelInfoCombateGlobo()
            };
        } 
        #endregion
    }
}
