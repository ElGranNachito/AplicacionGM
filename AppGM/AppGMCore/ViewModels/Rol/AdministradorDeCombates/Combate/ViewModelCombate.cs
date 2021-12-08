using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Castle.Core.Internal;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un <see cref="ModeloAdministradorDeCombate"/>
    /// </summary>
    public class ViewModelCombate : ViewModel
    {
        #region Campos & Propiedades

        //-----------------------------------CAMPOS-------------------------------------------


        private int indiceMapaActual = 0;

        public ControladorAdministradorDeCombate administradorDeCombate;

        public ControladorAdministradorDeCombate.dTurnoCambio HandlerTurnoCambio = delegate{};


        //--------------------------------PROPIEDADES------------------------------------------

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton de avanzar turno
        /// </summary>
        public ICommand ComandoAvanzarTurno { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton de retroceder turno
        /// </summary>
        public ICommand ComandoRetrocederTurno { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton 'Salir'
        /// </summary>
        public ICommand ComandoSalir { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton 'Tirada'
        /// </summary>
        public ICommand ComandoTirada { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton 'Agregar Participante'
        /// </summary>
        public ICommand ComandoAgregarParticipante { get; set; }

        /// <summary>
        /// Turno actual del combate
        /// </summary>
        public uint TurnoActual => administradorDeCombate.modelo.TurnoActual;

        /// <summary>
        /// Participantes
        /// </summary>
        public ObservableCollection<ViewModelParticipante> Participantes { get; set; } = new ObservableCollection<ViewModelParticipante>();

        /// <summary>
        /// Mapas del combate
        /// </summary>
        public ObservableCollection<ViewModelMapa> Mapas { get; set; } = new ObservableCollection<ViewModelMapa>();

        /// <summary>
        /// Participante de quien es el turno actual
        /// </summary>
        public ViewModelParticipante ParticipanteTurnoActual { get; set; }

        /// <summary>
        /// Mapa actualmente siendo mostrado
        /// </summary>
        public ViewModelMapa MapaActual => Mapas.IsNullOrEmpty() ? SistemaPrincipal.MapaPrincipal : Mapas[indiceMapaActual];

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewModelCombate()
        {
            ComandoSalir               = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.AdministrarCombates);
            ComandoAgregarParticipante = new Comando(AgregarParticipante);
            ComandoAvanzarTurno        = new Comando(AvanzarTurno);
            ComandoRetrocederTurno     = new Comando(RetrocederTurno);

            HandlerTurnoCambio = (ref int turnoAnterior, ref int turnoActual) =>
            {
                ParticipanteTurnoActual = Participantes[turnoActual];

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TurnoActual)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ParticipanteTurnoActual)));

            };
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Actualiza el combate actual borrando los datos del anterior
        /// </summary>
        /// <param name="_administradorDeCombate"></param>
        public void ActualizarCombateActual(ControladorAdministradorDeCombate _administradorDeCombate)
        {
            //Desubscribimos los delegado del controlador anterior
            if(administradorDeCombate != null)
				administradorDeCombate.OnTurnoCambio -= HandlerTurnoCambio;
            
            administradorDeCombate = _administradorDeCombate;

            if (Participantes.IsNullOrEmpty() && !_administradorDeCombate.ControladoresParticipantes.IsNullOrEmpty())
            {
                for (int i = 0; i < administradorDeCombate.ControladoresParticipantes.Count; ++i)
                    Participantes.Add(new ViewModelParticipante(administradorDeCombate.ControladoresParticipantes[i],this));

                ParticipanteTurnoActual = Participantes[0];

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ParticipanteTurnoActual)));
            }

            if(Mapas.IsNullOrEmpty() && !_administradorDeCombate.ControladoresMapas.IsNullOrEmpty())
                for(int i = 0; i < administradorDeCombate.ControladoresMapas.Count; ++i)
                    Mapas.Add(new ViewModelMapa(administradorDeCombate.ControladoresMapas[i], SistemaPrincipal.DatosRolSeleccionado.Climas[0]));

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MapaActual)));

            administradorDeCombate.OnTurnoCambio += HandlerTurnoCambio;
        }

        /// <summary>
        /// Funcion llamada para avanzar de turno en el combate
        /// </summary>
        public void AvanzarTurno()
        {
            administradorDeCombate.AvanzarTurno();
        }

        /// <summary>
        /// Funcion llamada para retroceder de turno en el combate
        /// </summary>
        public void RetrocederTurno()
        {
            administradorDeCombate.RetrocederTurno();
        }

        /// <summary>
        /// Funcion llamada para agregar un nuevo participante al combate
        /// </summary>
        private async void AgregarParticipante()
        {
            //VM para el contenido del popup
            ViewModelCrearParticipanteCombate vm = new ViewModelCrearParticipanteCombate(this);

            //Se crea el popup y se espera a que se cierre
            await SistemaPrincipal.MostrarMensajeAsync(vm, "Agregar Personaje", true, 250, 800);

            administradorDeCombate.AgregarParticipante(vm.vmResultado.controladorParticipante.modelo);

            if (Participantes.IsNullOrEmpty())
            {
                Participantes.Add(vm.vmResultado);
                administradorDeCombate.AvanzarTurno();
            }
            else
                Participantes.Add(vm.vmResultado);
            
            ActualizarCombateActual(administradorDeCombate);

            await SistemaPrincipal.GuardarDatosAsync();
        }

        #endregion
    }
}
