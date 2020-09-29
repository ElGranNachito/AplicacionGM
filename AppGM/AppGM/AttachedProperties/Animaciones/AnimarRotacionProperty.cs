using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Helpers;

namespace AppGM
{
    public class AnimarRotacionProperty : BaseAnimarProperty<AnimarRotacionProperty>
    {
        public override async Task RealizarAnimacion(FrameworkElement fe, bool valor)
        {
            if (valor)
                await fe.AñadirRotacionYMovimientoALaDerecha(TimeSpan.FromSeconds(0.5f), 40f);
            else
                await fe.AñadirRotacionYMovimientoALaIzquierda(TimeSpan.FromSeconds(0.5f), 25f);
        }
    }
}
