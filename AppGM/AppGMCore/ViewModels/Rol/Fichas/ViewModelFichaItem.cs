using System.ComponentModel;
using System.Windows.Input;
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
        public int AnchoBorde { get; set; } = 0;
        public string ColorFondo { get; set; } = "FFFFFF";
        public ICommand ComandoMouseClick { get; set; }

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

        #endregion

        #region Constructores
        public ViewModelFichaItem(ModeloPersonaje modeloPersonaje)
        {
            ModeloPersonaje = modeloPersonaje;

            ComandoMouseClick = new Comando(
                ()=>
                {
                    //Obtenemos una referencia al view model del menu que contiene las fichas y las muestra
                    ViewModelListaFichasVistaFichas ViewModelListaFichasVistaFichasTemp = SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>();

                    //Establecemos esta ficha como la actualmente seleccionada
                    ViewModelListaFichasVistaFichasTemp.FichaSeleccionada = this;

                    //Mostramos el borde
                    AnchoBorde = 1;
                    ColorFondo = "91EAFF";

                    PropertyChangedEventHandler PropertyChangedListener = null;

                    //Subscribimos un evento a property changed para que cuando esta deje de ser la ficha seleccionada el borde vuelva a desaparecer
                    PropertyChangedListener = (o, e) =>
                    {
                        //Revisamos que la propiedad que cambio sea la de ficha seleccionada
                        if (!e.PropertyName.Equals(nameof(ViewModelListaFichasVistaFichasTemp.FichaSeleccionada)))
                            return;

                        AnchoBorde = 0;
                        ColorFondo = "FFFFFF";

                        //Nos desuscribimos del evento
                        ViewModelListaFichasVistaFichasTemp.PropertyChanged -= PropertyChangedListener;
                    };

                    ViewModelListaFichasVistaFichasTemp.PropertyChanged += PropertyChangedListener;

                });
        } 
        #endregion
    }
}
