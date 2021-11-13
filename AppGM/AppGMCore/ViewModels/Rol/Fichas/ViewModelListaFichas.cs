using System.Collections.Generic;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// VM que representa una lista de <see cref="ViewModelFichaPersonaje"/>
    /// </summary>
    public class ViewModelListaFichas : ViewModel
    {
        public List<ViewModelFichaPersonaje> FichaItems { get; set; }
    }
}
