using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AppGM.Helpers
{
    public static class AnimationHelpers
    {
        public static async Task AñadirRotacionYMovimientoALaDerecha(this FrameworkElement elemento, TimeSpan duracionAnimacion, double rotacionObjetivo)
        {
            if (!(elemento.LayoutTransform is RotateTransform))
                return;

            Storyboard sb = new Storyboard();

            sb.AñadirRotacion(duracionAnimacion, rotacionObjetivo);

            sb.AñadirDesplazamientoALaDerecha(duracionAnimacion, new Thickness(120, 0, 0, 0));

            sb.Begin(elemento);

            await Task.Delay(duracionAnimacion);
        }

        public static async Task AñadirRotacionYMovimientoALaIzquierda(this FrameworkElement elemento, TimeSpan duracionAnimacion, double rotacionObjetivo)
        {
            if (!(elemento.LayoutTransform is RotateTransform))
                return;

            Storyboard sb = new Storyboard();

            sb.AñadirRotacion(duracionAnimacion, rotacionObjetivo);

            sb.AñadirDesplazamientoALaDerecha(duracionAnimacion, new Thickness(30, 0, 0, 0));

            sb.Begin(elemento);

            await Task.Delay(duracionAnimacion);
        }
    }
}
