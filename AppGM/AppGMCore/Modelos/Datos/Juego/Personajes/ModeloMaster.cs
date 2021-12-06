using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloMaster : ModeloPersonajeJugable
    {
        /// <summary>
        /// Estado de bienestar del master
        /// </summary>
        public EBienestar EBienestar { get; set; }

        /// <summary>
        /// Energia magica total del master
        /// </summary>
        public int OdTotal         { get; set; }

        /// <summary>
        /// Energia magica actual del master
        /// </summary>
        public int OdActual   { get; set; }

        /// <summary>
        /// Mana total del master
        /// </summary>
        public int ManaTotal       { get; set; }

        /// <summary>
        /// Mana actual del master
        /// </summary>
        public int ManaActual { get; set; }

        /// <summary>
        /// Stat de carisma
        /// </summary>
        public int Chr { get; set; }
        /// <summary>
        /// Ventaja en stat de carisma
        /// </summary>
        public int VentajaChr { get; set; }

        /// <summary>
        /// Command spells disponibles
        /// </summary>
        public ushort CommandSpells { get; set; }

        /// <summary>
        /// Lore del personaje
        /// </summary>
        [MaxLength(5000)]
        public string Lore { get; set; }
    }
}