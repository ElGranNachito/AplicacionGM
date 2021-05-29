using System;
using System.ComponentModel;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un <see cref="ModeloParticipante"/>
    /// </summary>
    public class ViewModelParticipante : BaseViewModel
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
        /// Nombre del participante
        /// </summary>
        public string NombreParticipante { get; private set; } = "Mr Sin Nombre";

        /// <summary>
        /// Ruta relativa a la imagen del participante
        /// </summary>
        public string PathImagen { get; private set; } = "../../../../Media/Imagenes/Megumin.png";

        /// <summary>
        /// Tipo del personaje
        /// </summary>
        public string TipoPersonaje { get; private set; } = Enum.GetName(typeof(ETipoPersonaje), ETipoPersonaje.Master);

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

            NombreParticipante = controladorParticipante.modelo.Personaje.Personaje.Nombre;
            TipoPersonaje      = Enum.GetName(typeof(ETipoPersonaje), controladorParticipante.modelo.Personaje.Personaje.TipoPersonaje);
            PathImagen         = controladorParticipante.ControladorPersonaje.modelo.PathImagen.IsNullOrWhiteSpace() ? PathImagen : controladorParticipante.ControladorPersonaje.ObtenerPathAImagen(4);

            handlerTurnoCambio = (ref int turnoActual) =>
            {
                if (combate.ParticipanteTurnoActual == this)
                {
                    EsSuTurno = true;

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsSuTurno)));
                }
            };

            combate.HandlerTurnoCambio += handlerTurnoCambio;
        }

        #endregion
    }
}