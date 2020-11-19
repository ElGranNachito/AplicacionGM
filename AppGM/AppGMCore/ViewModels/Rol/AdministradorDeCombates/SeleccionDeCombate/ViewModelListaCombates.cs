using System.Collections.Generic;

namespace AppGM.Core
{
    public class ViewModelListaCombates : BaseViewModel
    {
        #region Propiedades
        public List<ViewModelCombateItem> Combates { get; set; } = new List<ViewModelCombateItem>();
        #endregion

        #region Constructores
        public ViewModelListaCombates(List<ControladorAdministradorDeCombate> _combates)
        {
            for (int i = 0; i < _combates.Count; ++i)
                Combates.Add(new ViewModelCombateItem(_combates[i]));
        }
        public ViewModelListaCombates() { } 

        #endregion
    }
}
