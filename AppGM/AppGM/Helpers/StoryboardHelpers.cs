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

            Storyboard.SetTargetProperty(animacion, new PropertyPath("LayoutTransform.Angle"));

            sb.Children.Add(animacion);
        }

        public static void AñadirDesplazamientoALaDerecha(this Storyboard sb, TimeSpan duracionAnimacion, Thickness desplazamiento)
        {
            ThicknessAnimation animacion = new ThicknessAnimation
            {
                To = desplazamiento,
                Duration = duracionAnimacion,
                DecelerationRatio = 0.4f
            };

            Storyboard.SetTargetProperty(animacion, new PropertyPath("Margin"));

            sb.Children.Add(animacion);
        }
    }
}
