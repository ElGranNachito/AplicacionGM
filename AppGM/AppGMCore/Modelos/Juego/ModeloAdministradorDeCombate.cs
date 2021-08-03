using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el administrador de combates
    /// </summary>
    public class ModeloAdministradorDeCombate : ModeloBase
    {
        public ControladorAdministradorDeCombate controladorAdministradorDeCombate;

        /// <summary>
        /// Relacion rol
        /// </summary>
        public virtual TIRolCombate RolCombate { get; set; }

        /// <summary>
        /// IndiceZ para el turno actual entre personajes
        /// </summary>
        public int IndicePersonajeTurnoActual { get; set; }
        /// <summary>
        /// Turno actual en el combate
        /// </summary>
        public uint TurnoActual { get; set; }

        /// <summary>
        /// Si es verdadero el combate sigue activo.
        /// De ser falso se toma como concluido o en receso. 
        /// </summary>
        public bool EstaActivo { get; set; }

        /// <summary>
        /// Nombre del combate
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Ambiente en el que se lleva a cabo el combate.
        /// </summary>
        public virtual TIAdministradorDeCombateAmbiente AmbienteDelCombate { get; set; }

        /// <summary>
        /// Participantes en el combate
        /// </summary>
        public virtual List<TIAdministradorDeCombateParticipante> Participantes { get; set; } = new List<TIAdministradorDeCombateParticipante>();
        /// <summary>
        /// Mapas en los que el combate se lleve a cabo
        /// </summary>
        public virtual List<TIAdministradorDeCombateMapa> Mapas { get; set; } = new List<TIAdministradorDeCombateMapa>();
    }
}