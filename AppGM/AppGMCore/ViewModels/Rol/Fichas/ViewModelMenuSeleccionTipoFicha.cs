using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelMenuSeleccionTipoFicha : BaseViewModel
    {
        #region Propiedades

        public Grosor AnchoMargenCartas => SistemaPrincipal.Aplicacion.VentanaPrincipal.EstaMaximizada() ? new Grosor(20) : new Grosor(5, 20);
        public ICommand ComandoBotonFichasServants { get; set; }
        public ICommand ComandoBotonFichasMasters { get; set; }
        public ICommand ComandoBotonFichasInvocaciones { get; set; }
        public ICommand ComandoBotonFichasNPCs { get; set; }

        public List<ModeloServant> Servants { get; set; }
        public List<ModeloMaster> Masters { get; set; }
        public List<ModeloInvocacion> Invocaciones { get; set; }
        public List<ModeloPersonaje> NPCs { get; set; }

        #endregion

        #region Constructores

        public ViewModelMenuSeleccionTipoFicha(
            List<ModeloServant> _servants,
            List<ModeloMaster> _masters,
            List<ModeloInvocacion> _invocaciones,
            List<ModeloPersonaje> _npcs)
        {
            Servants     = _servants;
            Masters      = _masters;
            Invocaciones = _invocaciones;
            NPCs         = _npcs;

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

            Servants = new List<ModeloServant>
            {
                new ModeloServant
                {
                    Nombre = "Stephen Hawking",
                    EClaseServant = EClaseServant.Caster,
                    Agi = 0,
                    Str = 0,
                    Intel = 20,
                    Lck = 12
                }
            };

            Masters = new List<ModeloMaster>
            {
                new ModeloMaster
                {
                    Nombre = "Lepibe",
                    EClaseDeSuServant = EClaseServant.Berserker,
                    Str = 10,
                    Agi = 11,
                    End = 10,
                    Intel = 13,
                    Lck = 16,
                    Chr = 15,

                    Caracteristicas = new TIPersonajeJugableCaracteristicas
                    {
                        Caracteristicas = new ModeloCaracteristicas
                        {
                            Contextura = "Fuera de forma",
                            EAlineamiento = EAlineamiento.NeutralGood,
                            Edad = 19,
                            EManoDominante = EManoDominante.Derecha,
                            ESexo = ESexo.Masculino,
                            Estatura = 170,
                            Nacionalidad = "Estadounidense"
                        }
                    }
                }
            };

            EstablecerComandos();
        }
        #endregion

        #region Funciones
        private void EstablecerComandos()
        {
            ComandoBotonFichasServants = new Comando(() =>
            {
                //Creamos una variable tempoal para almacenar los view models para cada item
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                //Creamos los view models
                for (int i = 0; i < Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Servants[i]));

                //Cambiamos las fichas actuales por la variable temporal que creamos
                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Cambiamos la pagina actual
                SistemaPrincipal.RolSeleccionado.EMenuActual =
                    EMenuActualRol.VistaFichas;
            });

            ComandoBotonFichasMasters = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                for (int i = 0; i < Masters.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Masters[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenuActual =
                    EMenuActualRol.VistaFichas;
            });

            ComandoBotonFichasInvocaciones = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                for (int i = 0; i < Invocaciones.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Invocaciones[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenuActual =
                    EMenuActualRol.VistaFichas;
            });

            ComandoBotonFichasNPCs = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                for (int i = 0; i < NPCs.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(NPCs[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenuActual =
                    EMenuActualRol.VistaFichas;
            });
        } 
        #endregion
    }
}
