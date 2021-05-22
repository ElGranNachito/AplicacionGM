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
                    Nombre = "King Pellinore",
                    EClaseServant = EClaseServant.Saber,
                    Str = 15,
                    End = 13,
                    Agi = 15,
                    Int = 14,
                    Lck = 10
                }
            };

            Masters = new List<ModeloMaster>
            {
                new ModeloMaster
                {
                    Nombre = "Charles",
                    EClaseDeSuServant = EClaseServant.Saber,
                    Origen = "Traicion",
                    Afinidad = "Escudo",
                    Str = 14,
                    Agi = 13,
                    End = 14,
                    Int = 15,
                    Lck = 14,
                    Chr = 8,

                    Caracteristicas = new TIPersonajeJugableCaracteristicas
                    {
                        Caracteristicas = new ModeloCaracteristicas
                        {
                            Fisico = "1.90, Mamadisimo",
                            EArquetipo = EArquetipo.Mago,
                            Edad = 32,
                            EManoDominante = EManoDominante.Derecha,
                            ESexo = ESexo.Masculino,
                            Nacionalidad = "Britanico"
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
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>(Servants.Count);

                //Creamos los view models
                for (int i = 0; i < Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Servants[i]));

                //Cambiamos las fichas actuales por la variable temporal que creamos
                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Cambiamos la pagina actual
                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });

            ComandoBotonFichasMasters = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>(Masters.Count);

                for (int i = 0; i < Masters.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Masters[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });

            ComandoBotonFichasInvocaciones = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>(Invocaciones.Count);

                for (int i = 0; i < Invocaciones.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Invocaciones[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });

            ComandoBotonFichasNPCs = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>(NPCs.Count);

                for (int i = 0; i < NPCs.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(NPCs[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                SistemaPrincipal.RolSeleccionado.EMenu =
                    EMenuRol.VistaFichas;
            });
        } 
        #endregion
    }
}
