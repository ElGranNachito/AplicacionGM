namespace AppGM.Core
{
    /// <summary>
    /// Modelo de una tirada que no especifica su tipo
    /// </summary>
    public abstract class ModeloTiradaBase : ModeloBase
    {
        /// <summary>
        /// Nombre de la tirada
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion de la tirada
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Indica si esta tirada es para alguna accion en la que se tenga especialidad
        /// </summary>
        public int MultiplicadorDeEspecialidad { get; set; }

        /// <summary>
        /// Tipo de esta tirada
        /// </summary>
        public ETipoTirada TipoTirada { get; set; }

	    /// <summary>
        /// Modelo del personaje que contiene la tirada
        /// </summary>
        public virtual TITiradaPersonaje PersonajeContenedor { get; set; }

        /// <summary>
        /// Modelo de la habilidad que contiene la tirada
        /// </summary>
        public virtual TITiradaHabilidad HabilidadContenedora { get; set; }

        /// <summary>
        /// Modelo del utilizable que contiene la tirada
        /// </summary>
        public virtual TITiradaUtilizable UtilizableContenedor { get; set; }

        /// <summary>
        /// Modelo del la funcion que contiene la tirada
        /// </summary>
        public virtual TITiradaFuncion FuncionContenedora { get; set; }
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