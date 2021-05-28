using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloParticipante : ModeloBase
    {
        public ControladorParticipante controladorParticipante;

        public int TiradaIniciativa { get; set; }
        public bool EsSuTurno { get; set; }

        /// <summary>
        /// Personaje participante del combate
        /// </summary>
        public virtual TIParticipantePersonaje Personaje { get; set; }

        /// <summary>
        /// Acciones realizadas por el participante
        /// </summary>
        public virtual List<TIParticipanteAccion> AccionesRealizadas { get; set; } = new List<TIParticipanteAccion>();

        /// <summary>
        /// Combate en el que participa
        /// </summary>
        public virtual TIAdministradorDeCombateParticipante CombateActual { get; set; }
    }
}