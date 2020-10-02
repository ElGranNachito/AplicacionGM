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

        public ModeloPersonaje ModeloPersonaje { get; set; }

        public bool EsServant =>
            SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionTipoFicha>().ETipoPersonajeSeleccionado ==
            ETipoPersonaje.Servant;
        public bool EsMaster =>
            SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionTipoFicha>().ETipoPersonajeSeleccionado ==
            ETipoPersonaje.Master;
        public bool EsInvocacion =>
            SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionTipoFicha>().ETipoPersonajeSeleccionado ==
            ETipoPersonaje.Invocacion;
        public bool EsNPC =>
            SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionTipoFicha>().ETipoPersonajeSeleccionado ==
            ETipoPersonaje.NPC;

        public bool EsServantOMaster =>
            (SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionTipoFicha>().
                ETipoPersonajeSeleccionado & (ETipoPersonaje.Servant | ETipoPersonaje.Master)) != 0;
        public BaseViewModel ViewModelConBotonSeleccionado { get; set; }

        #endregion

        #region Constructores
        public ViewModelFichaItem(ModeloPersonaje modeloPersonaje)
        {
            ModeloPersonaje = modeloPersonaje;
            ViewModelConBotonSeleccionado = SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>();
        }

        #endregion
    }
}
