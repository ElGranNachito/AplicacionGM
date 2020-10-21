namespace AppGM.Core
{
    public class Grosor
    {
        #region Propiedades
        public double Izquierdo { get; set; }
        public double Superior { get; set; }
        public double Derecho { get; set; }
        public double Inferior { get; set; } 
        #endregion

        #region Constructores
        public Grosor(double tamañoUniforme)
        {
            Izquierdo = tamañoUniforme;
            Superior = tamañoUniforme;
            Derecho = tamañoUniforme;
            Inferior = tamañoUniforme;
        }

        public Grosor(double tamañoIzquierdoDerecho, double tamañoSuperiorInferior)
        {
            Izquierdo = tamañoIzquierdoDerecho;
            Derecho = tamañoIzquierdoDerecho;

            Superior = tamañoSuperiorInferior;
            Inferior = tamañoSuperiorInferior;
        }

        public Grosor(double tamañoIzquierdo, double tamañoSuperior, double tamañoDerecho, double tamañoInferior)
        {
            Izquierdo = tamañoIzquierdo;
            Superior = tamañoSuperior;
            Derecho = tamañoDerecho;
            Inferior = tamañoInferior;
        } 
        #endregion
    }
}
