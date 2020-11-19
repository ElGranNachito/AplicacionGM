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

        //Cantidad de dados
        public ushort Dados { get; set; }
        //Caras de los dados
        public ushort Caras { get; set; }
    }

    public class ModeloTiradaStat : ModeloTiradaBase
    {
        public ControladorTiradaStat controladorTiradaStat;

        public EStat StatDeLaQueDepende { get; set; }
    }

    public class ModeloTiradaDeDaño : ModeloTiradaVariable
    {
        public ControladorTiradaDaño controladorTiradaDaño;

        public ETipoDeDaño TipoDeDaño { get; set; }
    }
}