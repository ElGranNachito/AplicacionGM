using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Helpers;

namespace AppGM
{ 
    /// <summary>
    /// Realiza una animacion de opacidad sobre un< <see cref="FrameworkElement"/>
    /// </summary>
    public class AnimarOpacidadProperty : BaseAnimarProperty<AnimarOpacidadProperty>
    {
        protected override async Task RealizarAnimacion(FrameworkElement fe, bool valor)
        {
            //Si el valor es verdadero llevamos la opacidad a 0.5
            if (valor)
                await fe.AñadirAnimacionOpacidad(TimeSpan.FromSeconds(3), 0.5);
            //Si es falso la llevamos a 0
            else
                await fe.AñadirAnimacionOpacidad(TimeSpan.FromSeconds(3), 0);
        }
    }
}
