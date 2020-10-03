namespace AppGM.Core
{
    /// <summary>
    /// Contiene varios helpers para lidiar con strings y chars
    /// </summary>
    public static class StringHelpers
    {
        public static bool EsUnNumero(this char caracter)
        {
            if ((int) caracter > 47 && (int) caracter < 58)
                return true;

            return false;
        }
    }
}
