using System;
using System.Linq;

namespace AppGM.Core
{
    /// <summary>
    /// Contiene varios helpers para lidiar con strings y chars
    /// </summary>
    public static class StringHelpers
    {
        public static bool EsUnNumero(this char caracter)
        {
            return (caracter > 47 && caracter < 58);
        }

        public static bool EsPunto(this char caracter)
        {
            return caracter == '.';
        }
        public static bool EstaVacio(this string cadena)
        {
            return cadena.Length == 0;
        }
    }
}
