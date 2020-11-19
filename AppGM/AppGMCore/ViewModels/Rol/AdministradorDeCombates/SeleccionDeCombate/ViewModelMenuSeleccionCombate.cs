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

            GloboInfoCombate = new ViewModelGlobo<ViewModelInfoCombateGlobo>
            {
                ViewModelContenido = new ViewModelInfoCombateGlobo(),
                GloboVisible = false
            };
        }
        #endregion
    }
}