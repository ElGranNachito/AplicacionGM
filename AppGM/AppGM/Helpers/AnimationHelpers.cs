using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AppGM.Helpers
{
    public static class AnimationHelpers
    {
        public static async Task AñadirRotacionYDesplazamiento(this FrameworkElement elemento, TimeSpan duracionAnimacion, double rotacionObjetivo, Thickness desplazamiento)
        {
            //Primero debemos revisar que tenga un rotate transform
            if (!(elemento.LayoutTransform is RotateTransform))
                return;

            Storyboard sb = new Storyboard();

            //Añadimos la animacion de rotacion
            sb.AñadirRotacion(duracionAnimacion, rotacionObjetivo);

            //Añadimos la animacion de desplazamiento
            sb.AñadirDesplazamiento(duracionAnimacion, desplazamiento);

            //Comenzamos el storyboard sobre el elemento especificado
            sb.Begin(elemento);

            //Esperamos a que finalice la animacion
            await Task.Delay(duracionAnimacion);
        }
    }
}
