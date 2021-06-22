using System.Collections.Generic;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// VM que representa una lista de <see cref="ViewModelFichaItem"/>
    /// </summary>
    public class ViewModelListaFichas : ViewModel
    {
        public List<ViewModelFichaItem> FichaItems { get; set; }
    }
}
