using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public abstract class TIHabilidad
    {
        [ForeignKey(nameof(Habilidad))]
        public int IdHabilidad { get; set; }

        public ModeloHabilidad Habilidad { get; set; }
    }

    public class TIHabilidadLimitador : TIHabilidad
    {
        [ForeignKey(nameof(ModeloLimitador))]
        public int IdLimitador { get; set; }
        public ModeloLimitador ModeloLimitador { get; set; }
    }

    public class TIHabilidadCargasHabilidad : TIHabilidad
    {
        [ForeignKey(nameof(ModeloCargasHabilidad))]
        public int IdCargasHabilidad { get; set; }
        public ModeloCargasHabilidad ModeloCargasHabilidad { get; set; }
    }

    public class TIHabilidadTiradaBase : TIHabilidad
    {
        [ForeignKey(nameof(TiradaBase))]
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }

    public class TIHabilidadTiradaDeDaño : TIHabilidad
    {
        [ForeignKey(nameof(TiradaDeDaño))]
        public int IdTirada { get; set; }
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
    }

    public class TIHabilidadItem : TIHabilidad
    {
        [ForeignKey(nameof(Item))]
        public int IdItem { get; set; }
        public ModeloItem Item { get; set; }
    }

    public class TIHabilidadInvocacion : TIHabilidad
    {
        [ForeignKey(nameof(Invocacion))]
        public int IdInvocacion { get; set; }

        public ModeloInvocacion Invocacion { get; set; }
    }

    public class TIHabilidadEfecto : TIHabilidad
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }
}
