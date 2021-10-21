    using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo que representa un <see cref="ModeloPersonaje"/> que participa en un combate
    /// </summary>
    public class ModeloParticipante : ModeloBase
    {
        /// <summary>
        /// Controlador
        /// </summary>
        public ControladorParticipante controladorParticipante;

        /// <summary>
        /// Resultado de la tirada de iniciativa
        /// </summary>
        public int TiradaIniciativa { get; set; }

        /// <summary>
        /// Indica si es su turno de actuar en el combate
        /// </summary>
        public bool EsSuTurno { get; set; }

        /// <summary>
        /// Personaje participante del combate
        /// </summary>
        public virtual ModeloPersonaje Personaje { get; set; }

        /// <summary>
        /// Acciones realizadas por el participante
        /// </summary>
        public virtual List<ModeloAccion> AccionesRealizadas { get; set; } = new List<ModeloAccion>();

        /// <summary>
        /// Combate en el que participa
        /// </summary>
        public virtual ModeloAdministradorDeCombate CombateActual { get; set; }
    }
}