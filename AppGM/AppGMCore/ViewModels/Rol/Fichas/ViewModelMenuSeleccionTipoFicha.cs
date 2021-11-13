using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// VM que representa un control que permite seleccionar el tipo de fichas que explorar
    /// </summary>
    public class ViewModelMenuSeleccionTipoFicha : ViewModel
    {
        #region Miembros

        // Campos ---


        // Propiedades ---


        /// <summary>
        /// Margen de las cartas que representan los tipos de las fichas disponibles
        /// </summary>
        public Grosor AnchoMargenCartas 
	        => SistemaPrincipal.Aplicacion.VentanaPrincipal.EstaMaximizada() 
		        ? new Grosor(20) 
		        : new Grosor(5, 20);

        /// <summary>
        /// Comando a ejecutar cuando el usuario presiona la carte que representa las fichas de servants
        /// </summary>
        public ICommand ComandoBotonFichasServants { get; set; }

        /// <summary>
        /// Comando a ejecutar cuando el usuario presiona la carte que representa las fichas de masters
        /// </summary>
        public ICommand ComandoBotonFichasMasters { get; set; }

        /// <summary>
        /// Comando a ejecutar cuando el usuario presiona la carte que representa las fichas de invocaciones
        /// </summary>
        public ICommand ComandoBotonFichasInvocaciones { get; set; }

        /// <summary>
        /// Comando a ejecutar cuando el usuario presiona la carte que representa las fichas de NPCs
        /// </summary>
        public ICommand ComandoBotonFichasNPCs { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_servants"></param>
        /// <param name="_masters"></param>
        /// <param name="_invocaciones"></param>
        /// <param name="_npcs"></param>
        public ViewModelMenuSeleccionTipoFicha(
            List<ModeloServant> _servants,
            List<ModeloMaster> _masters,
            List<ModeloInvocacion> _invocaciones,
            List<ModeloPersonaje> _npcs)
        {

            EstablecerComandos();

            SistemaPrincipal.Aplicacion.VentanaPrincipal.OnEstadoModificado += v =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AnchoMargenCartas)));
            };
        }

        /// <summary>
        /// Solo para pruebas
        /// </summary>
        public ViewModelMenuSeleccionTipoFicha()
        {
            SistemaPrincipal.Aplicacion.VentanaPrincipal.OnEstadoModificado += v =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AnchoMargenCartas)));
            };

            EstablecerComandos();
        }
        #endregion

        #region Funciones

        /// <summary>
        /// Asigna los comandos correspondientes a cada boton de seleccion
        /// </summary>
        private void EstablecerComandos()
        {
            ComandoBotonFichasServants = new Comando(() =>
            {
                //Creamos una variable tempoal para almacenar los view models para cada item
                List<ViewModelFichaPersonaje> fichasTemp = new List<ViewModelFichaPersonaje>(SistemaPrincipal.DatosRolSeleccionado.Servants.Count);

                //Creamos los view models
                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.Servants[i]));

                //Cambiamos las fichas actuales por la variable temporal que creamos
                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Cambiamos la pagina actual
                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });

            ComandoBotonFichasMasters = new Comando(() =>
            {
                List<ViewModelFichaPersonaje> fichasTemp = new List<ViewModelFichaPersonaje>(SistemaPrincipal.DatosRolSeleccionado.Masters.Count);

                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.Masters.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.Masters[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });

            ComandoBotonFichasInvocaciones = new Comando(() =>
            {
                List<ViewModelFichaPersonaje> fichasTemp = new List<ViewModelFichaPersonaje>(SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Count);

                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.Invocaciones[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });

            ComandoBotonFichasNPCs = new Comando(() =>
            {
                List<ViewModelFichaPersonaje> fichasTemp = new List<ViewModelFichaPersonaje>(SistemaPrincipal.DatosRolSeleccionado.NPCs.Count);

                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.NPCs.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.NPCs[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });
        } 
        #endregion
    }
}
