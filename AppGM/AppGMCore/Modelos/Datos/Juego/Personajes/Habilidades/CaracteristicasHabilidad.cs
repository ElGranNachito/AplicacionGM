namespace AppGM.Core
{
    /// <summary>
    /// Modelo que representa un limitador de usos de una <see cref="ModeloHabilidad"/> o <see cref="ModeloItem"/>
    /// </summary>
    public class ModeloLimitador : ModeloBase
    {
        /// <summary>
        /// Controlador
        /// </summary>
        public ControladorLimitador controladorLimitador;

        /// <summary>
        /// Limite de usos
        /// </summary>
        public int LimiteDeUsos  { get; set; }
        
        /// <summary>
        /// Usos restantes
        /// </summary>
        public int UsosRestantes { get; set; }
        
        /// <summary>
        /// Dias de enfriamiento
        /// De no poder reestablecerse los usos este valor sera -1
        /// </summary>
        public int DiasDeEnfriamiento { get; set; }
        
        /// <summary>
        /// Dias de enfriamiento restantes
        /// </summary>
        public int DiasRestantes { get; set; }
    }

    /// <summary>
    /// Modelo que representa un modelo de cargas de usos de una <see cref="ModeloHabilidad"/> o <see cref="ModeloItem"/>
    /// </summary>
    public class ModeloCargas : ModeloBase
    {
        /// <summary>
        /// Controlador de cargas
        /// </summary>
        public ControladorCargasHabilidad controladorCargasHabilidad;

        /// <summary>
        /// Cargas maximas
        /// </summary>
        public int CargasMaximas  { get; set; }
        
        /// <summary>
        /// Cargas actuales
        /// </summary>
        public int CargasActuales { get; set; }
    }
}