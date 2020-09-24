namespace AppGM.Core
{
    public class DesignModelPrueba : ViewModelPrueba
    {
        public static DesignModelPrueba Instancia { get; set; } = new DesignModelPrueba();

        public DesignModelPrueba()
        {
            Instancia.Nombre = "Nachito";
        }
    }
}
