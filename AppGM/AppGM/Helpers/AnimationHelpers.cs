using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AppGM.Helpers
{
    /// <summary>
    /// Varios helpers para facilitar la adicion de animaciones a un <see cref="FrameworkElement"/>
    /// </summary>
    public static class AnimationHelpers
    {
        /// <summary>
        /// Añade animaciones de rotacion y desplazamiento a un <see cref="FrameworkElement"/>
        /// </summary>
        /// <param name="elemento">Elemento sobre el que se realizaran las animaciones</param>
        /// <param name="duracionAnimacion">Intervalo de tiempo que durara la animacion</param>
        /// <param name="rotacionObjetivo">Rotacion que pasara a tener el <paramref name="elemento"/></param>
        /// <param name="desplazamiento">Margen que pasara a tener el <paramref name="elemento"/></param>
        /// <returns></returns>
        public static async Task AñadirRotacionYDesplazamiento(
            this FrameworkElement elemento,
            TimeSpan duracionAnimacion,
            double rotacionObjetivo,
            Thickness desplazamiento)
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

        /// <summary>
        /// Añade una animacion de cambio de opacidad a un <see cref="FrameworkElement"/>
        /// </summary>
        /// <param name="elemento">Elemento sobre el que se realizara la animacion</param>
        /// <param name="duracionAnimacion">Intervalo de tiempo que durara la animacion</param>
        /// <param name="opacidadObjetivo">Opacidad que pasara a tener el <paramref name="elemento"/></param>
        /// <returns></returns>
        public static async Task AñadirAnimacionOpacidad(
            this FrameworkElement elemento,
            TimeSpan duracionAnimacion,
            double opacidadObjetivo)
        {
            Storyboard sb = new Storyboard();

            sb.AñadirCambioOpacidad(duracionAnimacion, opacidadObjetivo);

            sb.Begin(elemento);

            await Task.Delay(duracionAnimacion);
        }
    }
}
