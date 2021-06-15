using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa una lista de combates
    /// </summary>
    public class ViewModelListaCombates : ViewModel
    {
        #region Propiedades

        //Lista de combates
        public List<ViewModelCombateItem> Combates { get; set; } = new List<ViewModelCombateItem>();

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_combates">Lista con los controladores de los combates</param>
        public ViewModelListaCombates(List<ControladorAdministradorDeCombate> _combates)
        {
            for (int i = 0; i < _combates.Count; ++i)
                Combates.Add(new ViewModelCombateItem(_combates[i]));
        }

        /// <summary>
        /// Constructor default
        /// </summary>
        /// TODO: Eliminar
        public ViewModelListaCombates() { } 

        #endregion
    }
}
