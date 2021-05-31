namespace AppGM.Core
{
    /// <summary>
    /// Modelo de una tirada que no especifica su tipo
    /// </summary>
    public class ModeloTiradaBase : ModeloBase
    {
        /// <summary>
        /// Controlador
        /// </summary>
        public IControladorTiradaBase controladorTiradaBase;
    }

    /// <summary>
    /// Tirada con un numero de caras y dados personalizado
    /// </summary>
    public class ModeloTiradaVariable : ModeloTiradaBase
    {
	    /// <summary>
        /// Cantidad de dados
        /// </summary>
        public ushort Dados { get; set; }
        /// <summary>
        /// Caras de los dados
        /// </summary>
        public ushort Caras { get; set; }
    }

    /// <summary>
    /// Tirada que depende de una stat
    /// </summary>
    public class ModeloTiradaStat : ModeloTiradaBase
    {
	    /// <summary>
        /// Stat a tener en cuenta durante la tirada
        /// </summary>
        public EStat StatDeLaQueDepende { get; set; }
    }

    /// <summary>
    /// Tirada de daño
    /// </summary>
    public class ModeloTiradaDeDaño : ModeloTiradaVariable
    {
	    /// <summary>
        /// Tipo de daño que aplica la tirada
        /// </summary>
        public ETipoDeDaño TipoDeDaño { get; set; }
    }
}