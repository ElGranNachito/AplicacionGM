using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloTiradaBase
    {
        [Key]
        public int IdTirada { get; set; }
    }

    public class ModeloTiradaVariable : ModeloTiradaBase
    {
        //Cantidad de dados
        public ushort Dados { get; set; }
        //Caras de los dados
        public ushort Caras { get; set; }
    }

    public class ModeloTiradaStat : ModeloTiradaBase
    {
        public EStat StatDeLaQueDepende { get; set; }
    }

    public class ModeloTiradaDeDaño : ModeloTiradaVariable
    {
        public ETipoDeDaño TipoDeDaño { get; set; }
    }
}