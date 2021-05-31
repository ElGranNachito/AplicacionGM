using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AppGM
{
    /// <summary>
    /// Varias funciones para facilitar la adicion de animaciones a un <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Añade una animacion de rotacion a un <see cref="Storyboard"/>
        /// </summary>
        /// <param name="sb">Storyboard al que añadir la animacion</param>
        /// <param name="duracionAnimacion">Duracion de la animacion</param>
        /// <param name="rotacionObjetivo">Rotacion que tendra el <see cref="FrameworkElement"/> al finalizar la animacion</param>
        public static void AñadirRotacion(this Storyboard sb, TimeSpan duracionAnimacion, double rotacionObjetivo)
        {
            DoubleAnimation animacion = new DoubleAnimation
            {
                To = rotacionObjetivo,
                Duration = duracionAnimacion,
                DecelerationRatio = 0.4f
            };

            //Establece como propiedad objetivo "Angulo" que esta dentro de un RotateTransform
            Storyboard.SetTargetProperty(animacion, new PropertyPath("LayoutTransform.Angle"));

            sb.Children.Add(animacion);
        }

        /// <summary>
        /// Añade una animacion de desplazamiento a un <see cref="Storyboard"/>
        /// </summary>
        /// <param name="sb">Storyboard al que añadir la animacion</param>
        /// <param name="duracionAnimacion">Duracion de la animacion</param>
        /// <param name="desplazamiento">Margen que tendra el <see cref="FrameworkElement"/> al finalizar la animacion</param>
        public static void AñadirDesplazamiento(this Storyboard sb, TimeSpan duracionAnimacion, Thickness desplazamiento)
        {
            ThicknessAnimation animacion = new ThicknessAnimation
            {
                To = desplazamiento,
                Duration = duracionAnimacion,
                DecelerationRatio = 0.4f
            };

            //Establece como propiedad objetivo al margen del elemento
            Storyboard.SetTargetProperty(animacion, new PropertyPath("Margin"));

            sb.Children.Add(animacion);
        }

        /// <summary>
        /// Añade una animacion de opacidad a un <see cref="Storyboard"/>
        /// </summary>
        /// <param name="sb">Storyboard al que añadir la animacion</param>
        /// <param name="duracionAnimacion">Duracion de la animacion</param>
        /// <param name="opacidadObjetivo">Opacidad que tendra el <see cref="FrameworkElement"/> al finalizar la animacion</param>
        public static void AñadirCambioOpacidad(this Storyboard sb, TimeSpan duracionAnimacion, double opacidadObjetivo)
        {
            DoubleAnimation da = new DoubleAnimation
            {
                Duration = duracionAnimacion,
                To = opacidadObjetivo
            };

            //Establecemos como propiedad objetivo la opacidad del elemento
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

            sb.Children.Add(da);
        }
    }
}
