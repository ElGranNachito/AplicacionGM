using System;
using System.ComponentModel;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{

    /// <summary>
    /// View model para un item en un ItemControl que contiene datos basicos de una ficha de un personaje
    /// </summary>
    public class ViewModelFichaItem : BaseViewModel, IViewModelConBotonSeleccionado
    {
        #region Propiedades

        public ModeloPersonaje Personaje { get; set; }

        public bool EsServant    => Personaje.GetType() == typeof(ModeloServant);
        public bool EsMaster     => Personaje.GetType() == typeof(ModeloMaster);
        public bool EsInvocacion => Personaje.GetType() == typeof(ModeloInvocacion);
        public bool EsNPC        => Personaje.GetType() == typeof(ModeloPersonaje);
        public bool EsServantOMaster => EsServant || EsMaster;

        public BaseViewModel ViewModelConBotonSeleccionado { get; set; }

        #endregion

        #region Constructores
        public ViewModelFichaItem(ModeloPersonaje _personaje)
        {
            Personaje = _personaje;
            ViewModelConBotonSeleccionado = SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>();
        }

        #endregion
    }
}
