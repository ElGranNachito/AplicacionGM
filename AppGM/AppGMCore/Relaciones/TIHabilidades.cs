namespace AppGM.Core
{
    public abstract class TIHabilidad
    {
        public int IdHabilidad { get; set; }

        public ModeloHabilidad Habilidad { get; set; }
    }

    public class TIHabilidadLimitador : TIHabilidad
    {
        public int IdLimitador { get; set; }
        public ModeloLimitador ModeloLimitador { get; set; }
    }

    public class TIHabilidadCargasHabilidad : TIHabilidad
    {
        public int IdCargasHabilidad { get; set; }
        public ModeloCargasHabilidad ModeloCargasHabilidad { get; set; }
    }

    public class TIHabilidadTiradaBase : TIHabilidad
    {
        public int IdTirada { get; set; }
        public ModeloTiradaBase TiradaBase { get; set; }
    }

    public class TIHabilidadTiradaDeDaño : TIHabilidad
    {
        public int IdTirada { get; set; }
        public ModeloTiradaDeDaño TiradaDeDaño { get; set; }
    }

    public class TIHabilidadItem : TIHabilidad
    {
        public int IdItem { get; set; }
        public ModeloItem Item { get; set; }
    }

    public class TIHabilidadInvocacion : TIHabilidad
    {
        public int IdInvocacion { get; set; }

        public ModeloInvocacion Invocacion { get; set; }
    }

    public class TIHabilidadEfecto : TIHabilidad
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }
}
