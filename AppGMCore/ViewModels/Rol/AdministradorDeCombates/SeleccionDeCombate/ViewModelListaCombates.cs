using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ViewModelListaCombates : BaseViewModel
    {
        #region Propiedades
        public List<ViewModelCombateItem> Combates { get; set; }
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
