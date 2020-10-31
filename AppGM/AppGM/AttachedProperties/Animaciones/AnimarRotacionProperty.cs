using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Helpers;

namespace AppGM
{
    public class AnimarRotacionProperty : BaseAnimarProperty<AnimarRotacionProperty>
    {
        protected override async Task RealizarAnimacion(FrameworkElement fe, bool valor)
        {
            if (valor)
                await fe.AñadirRotacionYDesplazamiento(TimeSpan.FromSeconds(0.5f), 40f, new Thickness(120, 0,0, 0));
            else
                await fe.AñadirRotacionYDesplazamiento(TimeSpan.FromSeconds(0.5f), 25f, new Thickness(30, 0, 0, 0));
        }
    }
}
