using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo que representa una invocacion sin dar datos de su tipo
    /// </summary>
    public class ModeloDatosInvocacionBase : ModeloBase
    {
        /// <summary>
        /// Clave foranea que referencia a la <see cref="Invocacion"/>
        /// </summary>
        [ForeignKey(nameof(Invocacion))]
        public int IdInvocacion { get; set; }

        /// <summary>
        /// Invocacion cuyos datos contiene
        /// </summary>
        public virtual ModeloInvocacion Invocacion { get; set; }
    }

    /// <summary>
    /// Modelo de invocacion espiritual
    /// </summary>
    public class ModeloDatosInvocacion_Espiritual : ModeloDatosInvocacionBase
    {
        /// <summary>
        /// Mana que consume por turno a su invocador
        /// </summary>
        public int ConsumoDeManaPorTurno { get; set; }
    }

    /// <summary>
    /// Modelo de invocacion semi-espiritual
    /// </summary>
    public class ModeloDatosInvocacion_SemiEspiritual : ModeloDatosInvocacionBase
    {
        /// <summary>
        /// Mana que consume por cada dia de vida
        /// </summary>
        public int ConsumoDeManaDiario { get; set; }
        
        /// <summary>
        /// Prana maximo de la invocacion
        /// </summary>
        public int Prana { get; set; }

        /// <summary>
        /// Prana actual de la invocacion
        /// </summary>
        public int PranaActual { get; set; }
    }

    /// <summary>
    /// Modelo de invocacion fisica
    /// </summary>
    public class ModeloDatosInvocacion_Fisica : ModeloDatosInvocacionBase
    {
        /// <summary>
        /// Od total de la invocacion
        /// </summary>
        public int Od         { get; set; }

        /// <summary>
        /// Od actual de la invocacion
        /// </summary>
        public int OdActual   { get; set; }

        /// <summary>
        /// Mana maximo de la invocacion
        /// </summary>
        public int Mana       { get; set; }

        /// <summary>
        /// Mana actual de la invocacion
        /// </summary>
        public int ManaActual { get; set; }
    }
}
