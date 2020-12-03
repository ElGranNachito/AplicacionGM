using System;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelListaItems
    {
        #region Propiedades

        public string Titulo { get; set; }

        public bool PuedeAñadirItems { get; set; }

        public ICommand ComandoAñadirItem { get; set; } 

        #endregion

        #region Constructor

        public ViewModelListaItems(Action delegadoAñadirItem, bool _puedeAñadirItems, string _titulo)
        {
            ComandoAñadirItem = new Comando(delegadoAñadirItem);

            PuedeAñadirItems = _puedeAñadirItems;

            Titulo = _titulo;
        } 

        #endregion
    }
}
