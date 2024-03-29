﻿using System.Text.RegularExpressions;
using System.Linq;
using System;

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
        public static bool EsUnNumero(this string cadena)
        {
	        return Regex.IsMatch(cadena, "[0-9]+$");
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
        public static bool IsNullOrWhiteSpace(this string cadena)
        {
	        return string.IsNullOrWhiteSpace(cadena);
        }

        public static int LookBehindFirstIndexOf(this string cadena, char [] caracteres, int indice)
		{
            for(int i = --indice; i >= 0; --i)
			{
                if (caracteres.Contains(cadena[i]))
                    return i;
			}

            return -1;
		}

        public static bool SeEncuentraDentroDe(this char caracter, ReadOnlySpan<char> cadena)
		{
            foreach(var c in cadena)
			{
                if (c == caracter)
                    return true;
			}

            return false;
		}

        public static string Remove(this string cadena, int indiceComienzo, int indiceFin) => cadena.Remove(indiceComienzo, indiceFin - indiceComienzo);
    }
}
