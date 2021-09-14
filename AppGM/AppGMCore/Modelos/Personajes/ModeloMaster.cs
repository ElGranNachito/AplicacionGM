using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloMaster : ModeloPersonajeJugable
    {
        /// <summary>
        /// Clase de servant que posee el master
        /// </summary>
        public EClaseServant EClaseDeSuServant { get; set; }
        /// <summary>
        /// Estado de bienestar del master
        /// </summary>
        public EBienestar EBienestar           { get; set; }

        /// <summary>
        /// Energia magica del personaje
        /// </summary>
        public int Od         { get; set; }
        public int OdActual   { get; set; }
        public int Mana       { get; set; }
        public int ManaActual { get; set; }

        /// <summary>
        /// Stat de carisma
        /// </summary>
        public ushort Chr        { get; set; }
        /// <summary>
        /// Ventaja en stat de carisma
        /// </summary>
        public ushort VentajaChr { get; set; }

        /// <summary>
        /// Command spells disponibles
        /// </summary>
        public ushort CommandSpells { get; set; }

        /// <summary>
        /// Lore del personaje
        /// </summary>
        [MaxLength(5000)]
        public string Lore     { get; set; }
        /// <summary>
        /// Condicion relacionada al lore del personaje
        /// </summary>
        [MaxLength(50)]
        public string Origen   { get; set; } 
        /// <summary>
        /// Conceptos dominados por el master
        /// </summary>
        [MaxLength(50)]
        public string Afinidad { get; set; }
    }
}