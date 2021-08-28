namespace AppGM.Core
{
    public static class MathHelpers
    {
        /// <summary>
        /// Redondea un <see cref="decimal"/>
        /// </summary>
        /// <param name="numero"><see cref="decimal"/> a redondear</param>
        /// <param name="decimales">cantidad de decimales a dejar</param>
        /// <returns><see cref="decimal"/> Redondeado</returns>
        public static decimal Round(this decimal numero, int decimales)
        {
            return System.Math.Round(numero, decimales);
        }

        /// <summary>
        /// Redondea un <see cref="float"/>
        /// </summary>
        /// <param name="numero"><see cref="float"/> a redondear</param>
        /// <param name="decimales">cantidad de decimales a dejar</param>
        /// <returns><see cref="float"/> Redondeado</returns>
        public static float Round(this float numero, int decimales)
        {
            return (float)System.Math.Round(numero, decimales);
        }

        /// <summary>
        /// Redondea un <see cref="double"/>
        /// </summary>
        /// <param name="numero"><see cref="double"/> a redondear</param>
        /// <param name="decimales">cantidad de decimales a dejar</param>
        /// <returns><see cref="double"/> Redondeado</returns>
        public static double Round(this double numero, int decimales)
        {
            return System.Math.Round(numero, decimales);
        }

        /// <summary>
        /// Permite averiguar la longitud de un <see cref="int"/>
        /// </summary>
        /// <param name="numero"><see cref="int"/> a evaluar</param>
        /// <returns>longitud del <see cref="int"/></returns>
        public static int Length(this int numero)
        {
            return numero.ToString().Length;
        }

        public static int ToInt(this decimal numero) => (int) numero;
        public static int ToInt(this float numero) => (int) numero;
        public static int ToInt(this double numero) => (int) numero;
    }
}