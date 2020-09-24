
using System.Windows;

namespace AppGM.Helpers
{
    public static class WindowHelpers
    {
        public static bool EstaMaximizada(this Window ventana)
        {
            return ventana.WindowState == WindowState.Maximized;
        }
    }
}
