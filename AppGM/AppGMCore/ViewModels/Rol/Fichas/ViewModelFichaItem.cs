using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// View model para un item en un ItemControl que contiene datos basicos de una ficha de un personaje
    /// </summary>
    public class ViewModelFichaItem : BaseViewModel
    {
        #region Miembros

        public ModeloPersonaje ModeloPersonaje { get; set; }
        public string Nombre { get; set; }
        public EClaseServant ClaseServant { get; set; } = EClaseServant.NINGUNO;

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

        #endregion

        #region Constructores
        public ViewModelFichaItem(ModeloPersonaje modeloPersonaje)
        {
            ModeloPersonaje = modeloPersonaje;

            Nombre = ModeloPersonaje.Nombre;

            if (modeloPersonaje is ModeloServant modeloServant)
                ClaseServant = modeloServant.mEClaseDeServant;
        } 
        #endregion
    }
}
