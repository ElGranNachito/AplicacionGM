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
        #region Propiedades

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

        //TODO: Seguramente remover todas estas listas ya que podemos acceder a ellas desde el sistema principal----------------

        /// <summary>
        /// Lista de los <see cref="ModeloServant"/> disponibles
        /// </summary>
        public List<ModeloServant> Servants { get; set; }

        /// <summary>
        /// Lista de los <see cref="ModeloMaster"/> disponibles
        /// </summary>
        public List<ModeloMaster> Masters { get; set; }

        /// <summary>
        /// Lista de los <see cref="ModeloInvocacion"/> disponibles
        /// </summary>
        public List<ModeloInvocacion> Invocaciones { get; set; }

        /// <summary>
        /// Lista de los <see cref="ModeloPersonaje"/> disponibles
        /// </summary>
        public List<ModeloPersonaje> NPCs { get; set; }

        //TODO----------------------------------------------------------------------------------------------------------------------

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
                    ClaseServant = EClaseServant.Saber,
                    Str = ERango.A.AValorNumerico(),
                    End = ERango.A.AValorNumerico(),
                    Agi = ERango.C.AValorNumerico(),
                    Int = ERango.D.AValorNumerico(),
                    Lck = ERango.B.AValorNumerico()
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

                    Caracteristicas = new ModeloCaracteristicas
                    {
                       Fisico = "1.90, Mamadisimo",
                       Arquetipo = EArquetipo.Mago,
                       Edad = 32,
                       ManoDominante = EManoDominante.Derecha,
                       Sexo = ESexo.Masculino,
                       Nacionalidad = "Britanico"
                    }
                }
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
