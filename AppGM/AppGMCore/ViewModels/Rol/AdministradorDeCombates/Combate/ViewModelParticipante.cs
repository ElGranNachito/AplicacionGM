using System;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un <see cref="ModeloParticipante"/>
    /// </summary>
    public class ViewModelParticipante : ViewModel
    {
        #region Miembros

        //----------------------------------CAMPOS-----------------------------------


        /// <summary>
        /// Funcion que se ejecuta cuando el turno actual del combate se actualiza
        /// </summary>
        private ControladorAdministradorDeCombate.dTurnoCambio handlerTurnoCambio;

        /// <summary>
        /// Controlador de este participante
        /// </summary>
        public ControladorParticipante controladorParticipante;

        /// <summary>
        /// VM del combate
        /// </summary>
        public ViewModelCombate combate;


        //-------------------------------------PROPIEDADES----------------------------------------------


        /// <summary>
        /// Comando que se ejecuta al presionar el boton 'Eliminar participante'.
        /// </summary>
        public ICommand ComandoEliminarParticipante { get; set; }

        /// <summary>
        /// Comando que se ejecuta al presionar el menu 'Accion'.
        /// </summary>
        public ICommand ComandoAñadirAccion { get; set; }

        /// <summary>
        /// Comando que se ejecuta al presionar el menu 'Ver personaje'.
        /// </summary>
        public ICommand ComandoCrearMensajeFichaPersonaje { get; set; }

        /// <summary>
        /// Nombre del participante
        /// </summary>
        public string NombreParticipante { get; private set; } = "Participante X";

        /// <summary>
        /// Tipo del personaje
        /// </summary>
        public string TipoPersonaje => Enum.GetName(controladorParticipante.ControladorPersonaje.modelo.TipoPersonaje);

        /// <summary>
        /// Ruta relativa a la imagen del participante
        /// </summary>
        public string PathImagen
        {
            get
            {
                switch (controladorParticipante.ControladorPersonaje.modelo.TipoPersonaje)
                {
                    case ETipoPersonaje.Master:
                        return string.Intern($"../../../../Media/Imagenes/Posiciones/Master_{EnumHelpers.ToStringClaseServant(((ModeloMaster) controladorParticipante.ControladorPersonaje.modelo).ClaseServant)}.png");
                    case ETipoPersonaje.Servant:
                        return string.Intern($"../../../../Media/Imagenes/Posiciones/{EnumHelpers.ToStringClaseServant(((ModeloServant) controladorParticipante.ControladorPersonaje.modelo).ClaseServant)}.png");
                    case ETipoPersonaje.Invocacion:
                        return string.Intern($"../../../../Media/Imagenes/Posiciones/Invocacion_{EnumHelpers.ToStringClaseServant(((ModeloPersonajeJugable)((ModeloInvocacion)controladorParticipante.ControladorPersonaje.modelo).Invocador).ClaseServant)}.png");
                    default:
                        return string.Intern("../../../../Media/Imagenes/Megumin.png");
                }
            }
        }

        /// <summary>
        /// Total de acciones realizadas desde que comenzo su turno.
        /// </summary>
        public int AccionesRealizadas => controladorParticipante.modelo.AccionesRealizadasEnTurno;

        /// <summary>
        /// Total de acciones restantes hasta alcanzar el limite por turno.
        /// </summary>
        public int AccionesRestantes => controladorParticipante.modelo.AccionesRestantes;

        /// <summary>
        /// Total de acciones posibles por turno.
        /// </summary>
        public int TotalAccionesPosibles => controladorParticipante.modelo.TotalAccionesPorTurno;

        /// <summary>
        /// Indica si actualmente es el turno del personaje
        /// </summary>
        public bool EsSuTurno => controladorParticipante.modelo.EsSuTurno;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_controladorParticipante">Controlador del participante</param>
        /// <param name="_combate">VM del combate</param>
        public ViewModelParticipante(ControladorParticipante _controladorParticipante, ViewModelCombate _combate)
        {
            controladorParticipante = _controladorParticipante;
            combate = _combate;

            NombreParticipante = controladorParticipante.modelo.Personaje.Nombre;
            
            handlerTurnoCambio = (ref int turnoAnterior, ref int turnoActual) =>
            {
                if (combate.ParticipanteTurnoActual.controladorParticipante.ControladorPersonaje.modelo.Id == this.controladorParticipante.ControladorPersonaje.modelo.Id)
                {
                    var participanteAnterior = combate.Participantes[turnoAnterior];
                    
                    participanteAnterior.controladorParticipante.modelo.AccionesRestantes         = 0;
                    participanteAnterior.controladorParticipante.modelo.AccionesRealizadasEnTurno = 0;

                    participanteAnterior.DispararPropertyChanged(nameof(participanteAnterior.AccionesRestantes));
                    participanteAnterior.DispararPropertyChanged(nameof(participanteAnterior.AccionesRealizadas));
                    participanteAnterior.DispararPropertyChanged(nameof(participanteAnterior.EsSuTurno));

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsSuTurno)));
                }
            };

            ComandoEliminarParticipante = new Comando(EliminarParticipante);
            ComandoAñadirAccion         = new Comando(AñadirAccion);

            combate.HandlerTurnoCambio += handlerTurnoCambio;
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Elimina este participante del combate y de la base de datos
        /// </summary>
        private void EliminarParticipante()
        {
            if (combate.ParticipanteTurnoActual == this)
                combate.AvanzarTurno();

            combate.Participantes.Remove(this);

            controladorParticipante.Eliminar();

            SistemaPrincipal.GuardarDatosAsync();
        }

        /// <summary>
        /// Funcion llamada para añadir una nueva accion al participante
        /// </summary>
        private async void AñadirAccion()
        {
            //VM para el contenido del popup
            ViewModelCrearAccionParticipante vm = new ViewModelCrearAccionParticipante(this);

            //Se crea el popup y se espera a que se cierre
            await SistemaPrincipal.MostrarMensajeAsync(vm, "Añadir accion", true, -1, -1);

            //Si el resultado es valido entonces añadimos la nueva accion
            if (vm.vmResultado is ViewModelAccion vmNuevaAccion)
            {
                controladorParticipante.ControladoresAcciones.Add(vmNuevaAccion.accion);

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AccionesRestantes)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AccionesRealizadas)));
            }
        }

        #endregion
    }
}