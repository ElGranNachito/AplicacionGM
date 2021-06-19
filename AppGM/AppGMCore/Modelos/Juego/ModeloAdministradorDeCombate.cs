using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el administrador de combates
    /// </summary>
    public class ModeloAdministradorDeCombate : ModeloBase
    {
        /// <summary>
        /// Relacion rol
        /// </summary>
        public TIRolCombate RolCombate { get; set; }

        public ControladorAdministradorDeCombate controladorAdministradorDeCombate;

        /// <summary>
        /// IndiceZ para el turno actual entre personajes
        /// </summary>
        public int    IndicePersonajeTurnoActual { get; set; }
        /// <summary>
        /// Turno actual en el combate
        /// </summary>
        public uint   TurnoActual { get; set; }
        /// <summary>
        /// Nombre del combate
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Participantes en el combate
        /// </summary>
        public List<TIAdministradorDeCombateParticipante> Participantes { get; set; } = new List<TIAdministradorDeCombateParticipante>();
        /// <summary>
        /// Mapas en los que el combate se lleve a cabo
        /// </summary>
        public List<TIAdministradorDeCombateMapa> Mapas { get; set; }                 = new List<TIAdministradorDeCombateMapa>();
    }
}