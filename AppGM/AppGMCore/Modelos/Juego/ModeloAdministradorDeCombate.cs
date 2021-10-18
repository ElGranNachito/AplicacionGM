using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public virtual ModeloRol Rol { get; set; }

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
        [MaxLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Ambiente en el que se lleva a cabo el combate.
        /// </summary>
        public virtual ModeloAmbiente AmbienteDelCombate { get; set; }

        /// <summary>
        /// Participantes en el combate
        /// </summary>
        public virtual List<ModeloParticipante> Participantes { get; set; } = new List<ModeloParticipante>();
        /// <summary>
        /// Mapas en los que el combate se lleve a cabo
        /// </summary>
        public virtual List<ModeloMapa> Mapas { get; set; } = new List<ModeloMapa>();
    }
}