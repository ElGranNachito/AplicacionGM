namespace AppGM.Core
{
    /// <summary>
    /// Contiene varios helpers para lidiar con strings y chars
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Permite averiguar si una variable de tipo <see cref="char"/> tiene un valor numerico
        /// </summary>
        /// <param name="caracter">Caracter a revisar</param>
        /// <returns>true si el caracter es un numero</returns>
        public static bool EsUnNumero(this char caracter)
        {
            return (caracter > 47 && caracter < 58);
        }

        /// <summary>
        /// Permite averiguar si un <see cref="char"/> es un punto (".")
        /// </summary>
        /// <param name="caracter">Caracter a revisar</param>
        /// <returns>true si el caracter es un numero</returns>
        public static bool EsPunto(this char caracter)
        {
            return caracter == '.';
        }

        /// <summary>
        /// Permite averiguar si un <see cref="string"/> esta vacio
        /// </summary>
        /// <param name="cadena">string a evaluar</param>
        /// <returns>true si el string esta vacio</returns>
        public static bool EstaVacio(this string cadena)
        {
            return cadena.Length == 0;
        }
    }
}
