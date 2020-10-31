using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Helpers;

namespace AppGM
{ 
    public class AnimarOpacidadProperty : BaseAnimarProperty<AnimarOpacidadProperty>
    {
        protected override async Task RealizarAnimacion(FrameworkElement fe, bool valor)
        {
            if (valor)
                await fe.AñadirAnimacionOpacidad(TimeSpan.FromSeconds(3), 0.5);
            else
                await fe.AñadirAnimacionOpacidad(TimeSpan.FromSeconds(3), 0);
        }
    }
}
