using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AppGM
{
    public static class StoryboardHelpers
    {
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

        public static void AñadirCambioOpacidad(this Storyboard sb, TimeSpan duracionAnimacion, double opacidadObjetivo)
        {
            DoubleAnimation da = new DoubleAnimation
            {
                Duration = duracionAnimacion,
                To = opacidadObjetivo
            };

            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

            sb.Children.Add(da);
        }
    }
}
