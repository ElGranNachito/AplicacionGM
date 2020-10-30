namespace AppGM.Core
{
    public static class MathHelpers
    {
        public static decimal Round(this decimal numero, int decimales)
        {
            return System.Math.Round(numero, decimales);
        }
        public static double Round(this float numero, int decimales)
        {
            return System.Math.Round(numero, decimales);
        }
        public static double Round(this double numero, int decimales)
        {
            return System.Math.Round(numero, decimales);
        }
        public static int Length(this int numero)
        {
            return numero.ToString().Length;
        }
    }
}
