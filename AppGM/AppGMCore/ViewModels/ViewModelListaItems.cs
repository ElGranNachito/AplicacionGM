using System;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel que representa una lista de items con titulo
    /// </summary>
    public class ViewModelListaItems
    {
        #region Propiedades

        /// <summary>
        /// Titulo de la lista
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// ¿Puede el usuario añadir mas items a esta lista?
        /// </summary>
        public bool PuedeAñadirItems { get; set; }

        /// <summary>
        /// Delegado que se ejecutara cuando se presione el boton añadir
        /// </summary>
        public Action DelegadoAñadirItem { get; set; }

        /// <summary>
        /// Comando que se llamara cuando se presione el boton añadir
        /// </summary>
        public ICommand ComandoAñadirItem { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_delegadoAñadirItem">Delegado que se ejecuta al presionar el boton añadir item</param>
        /// <param name="_puedeAñadirItems">¿Puede el usuarui agregar mas items?</param>
        /// <param name="_titulo">Titulo de la lista</param>
        public ViewModelListaItems(Action _delegadoAñadirItem, bool _puedeAñadirItems, string _titulo)
        {
	        DelegadoAñadirItem = _delegadoAñadirItem;

	        PuedeAñadirItems = _puedeAñadirItems;

	        Titulo = _titulo;

            ComandoAñadirItem = new Comando(() =>
            {
                //Solo ejecutamos el delegado si se pueden añadir mas items
	            if (PuedeAñadirItems)
		            DelegadoAñadirItem();
            });
        }

        #endregion
    }
}
