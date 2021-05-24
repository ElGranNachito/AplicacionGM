using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloTiradaBase : ModeloBase
    {
        public ControladorTirada<ModeloTiradaBase> controladorTiradaBase;
    }

    public class ModeloTiradaVariable : ModeloTiradaBase
    {
        public ControladorTiradaVariable controladorTiradaVariable;

        /// <summary>
        /// Cantidad de dados
        /// </summary>
        public ushort Dados { get; set; }
        /// <summary>
        /// Caras de los dados
        /// </summary>
        public ushort Caras { get; set; }
    }

    public class ModeloTiradaStat : ModeloTiradaBase
    {
        public ControladorTiradaStat controladorTiradaStat;

        /// <summary>
        /// Stat a tener en cuenta durante la tirada
        /// </summary>
        public EStat StatDeLaQueDepende { get; set; }
    }

    public class ModeloTiradaDeDaño : ModeloTiradaVariable
    {
        public ControladorTiradaDaño controladorTiradaDaño;

        /// <summary>
        /// Tipo de daño que aplica la tirada
        /// </summary>
        public ETipoDeDaño TipoDeDaño { get; set; }
    }
}