﻿using System;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel que representa una lista de items con titulo
    /// </summary>
    public class ViewModelListaItems<TItem>
        where TItem: ViewModel
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
        /// Cantidad maxima de items que puede contener esta lista
        /// </summary>
        public int CantidadMaximaDeItems { get; init; }

        /// <summary>
        /// Delegado que se ejecutara cuando se presione el boton añadir
        /// </summary>
        public Action DelegadoAñadirItem { get; set; }

        /// <summary>
        /// Items contenidos
        /// </summary>
        public ViewModelListaDeElementos<TItem> Items { get; set; } = new ViewModelListaDeElementos<TItem>();

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
        public ViewModelListaItems(Action _delegadoAñadirItem, bool _puedeAñadirItems, string _titulo, int _cantidadMaximaDeItems = -1)
        {
	        DelegadoAñadirItem = _delegadoAñadirItem;

	        PuedeAñadirItems = _puedeAñadirItems;

	        Titulo = _titulo;

	        CantidadMaximaDeItems = _cantidadMaximaDeItems;

            ComandoAñadirItem = new Comando(() =>
            {
                //Solo ejecutamos el delegado si se pueden añadir mas items
	            if (PuedeAñadirItems)
		            DelegadoAñadirItem();
            });

            if (_cantidadMaximaDeItems > 0)
            {
	            Items.Elementos.CollectionChanged += (sender, args) =>
	            {
		            PuedeAñadirItems = Items.Count < CantidadMaximaDeItems;
	            };
            }
        }

        #endregion
    }
}
