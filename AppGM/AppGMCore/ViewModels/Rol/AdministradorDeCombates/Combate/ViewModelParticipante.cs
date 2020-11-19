using System;
using System.ComponentModel;

namespace AppGM.Core
{
    public class ViewModelParticipante : BaseViewModel
    {
        #region Miembros

        private ControladorAdministradorDeCombate.dTurnoCambio handlerTurnoCambio = delegate { };

        public ControladorParticipante controladorParticipante;

        public ViewModelCombate combate;

        #endregion

        #region Propiedades

        public string NombreParticipante { get; private set; } = "Mr Sin Nombre";
        public string PathImagen { get; private set; } = "../../../../Media/Imagenes/Megumin.png";
        public string TipoPersonaje { get; private set; } = Enum.GetName(typeof(ETipoPersonaje), ETipoPersonaje.Master);
        public bool   EsSuTurno { get; private set; } = false;

        #endregion

        #region Constructor

        public ViewModelParticipante(ControladorParticipante _controladorParticipante, ViewModelCombate _combate)
        {
            controladorParticipante = _controladorParticipante;
            combate = _combate;

            NombreParticipante = controladorParticipante.modelo.Personaje.Personaje.Nombre;
            TipoPersonaje      = Enum.GetName(typeof(ETipoPersonaje), controladorParticipante.modelo.Personaje.Personaje.TipoPersonaje);
            PathImagen         = String.IsNullOrEmpty(controladorParticipante.ControladorPersonaje.modelo.PathImagen) ? PathImagen : controladorParticipante.ControladorPersonaje.ObtenerPathAImagen(4);

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

        #region Destructor

        ~ViewModelParticipante()
        {
            combate.HandlerTurnoCambio -= handlerTurnoCambio;
        } 

        #endregion
    }
}
