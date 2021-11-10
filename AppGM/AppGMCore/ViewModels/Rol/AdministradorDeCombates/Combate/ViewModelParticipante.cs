using System;
using System.ComponentModel;
using System.Dynamic;
using System.Text;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un <see cref="ModeloParticipante"/>
    /// </summary>
    public class ViewModelParticipante : ViewModel
    {
        #region Campos & Propiedades

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
        /// Comando que se ejecuta al presionar el boton de eliminar participante.
        /// </summary>
        public ICommand ComandoEliminarParticipante { get; set; }

        /// <summary>
        /// Nombre del participante
        /// </summary>
        public string NombreParticipante { get; private set; } = "Participante X";

        /// <summary>
        /// Tipo del personaje
        /// </summary>
        public string TipoPersonaje { get; private set; }

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
        /// Indica si actualmente es el turno del personaje
        /// </summary>
        public bool   EsSuTurno { get; private set; } = false;

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
            TipoPersonaje = EnumHelpers.ToStringTipoPersonaje(controladorParticipante.ControladorPersonaje.modelo.TipoPersonaje);
            
            handlerTurnoCambio = (ref int turnoActual) =>
            {
                if (combate.ParticipanteTurnoActual == this)
                {
                    EsSuTurno = true;

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsSuTurno)));
                }
            };

            ComandoEliminarParticipante = new Comando(EliminarParticipante);

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
            {
                //Avanzar turno.
            }

            combate.Participantes.Remove(this);

            controladorParticipante.Eliminar();

            SistemaPrincipal.GuardarDatosAsync();
        }

        #endregion
    }
}