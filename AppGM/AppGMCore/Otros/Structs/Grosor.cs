namespace AppGM.Core
{
    /// <summary>
    /// Estrucutra equivalente a Thickness para uso interno
    /// </summary>
    public struct Grosor
    {
        #region Propiedades

        public double Izquierdo { get; set; }
        public double Superior { get; set; }
        public double Derecho { get; set; }
        public double Inferior { get; set; } 

        #endregion

        #region Constructores

        /// <summary>
        /// Crea una instancia de <see cref="Grosor"/> a partir de un solo valor que se le dara a todos los lados
        /// </summary>
        /// <param name="tamañoUniforme">Tamaño que se le dara a todos los lados</param>
        public Grosor(double tamañoUniforme)
        {
            Izquierdo = tamañoUniforme;
            Superior = tamañoUniforme;
            Derecho = tamañoUniforme;
            Inferior = tamañoUniforme;
        }

        /// <summary>
        /// Crea una instancia de <see cref="Grosor"/>
        /// </summary>
        /// <param name="tamañoIzquierdoDerecho">Tamaño que se le dara a los lados izquierdo y derecho</param>
        /// <param name="tamañoSuperiorInferior">Tamaño que se le dara a los lados superior e inferior</param>
        public Grosor(double tamañoIzquierdoDerecho, double tamañoSuperiorInferior)
        {
            Izquierdo = tamañoIzquierdoDerecho;
            Derecho = tamañoIzquierdoDerecho;

            Superior = tamañoSuperiorInferior;
            Inferior = tamañoSuperiorInferior;
        }

        /// <summary>
        /// Crea una instancia de <see cref="Grosor"/>
        /// </summary>
        /// <param name="tamañoIzquierdo">Tamaño del lado izquierdo</param>
        /// <param name="tamañoSuperior">Tamaño del lado superior</param>
        /// <param name="tamañoDerecho">Tamaño del lado derecho</param>
        /// <param name="tamañoInferior">Tamaño del lado inferior</param>
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