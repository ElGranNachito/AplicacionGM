using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Helpers;

namespace AppGM
{
    /// <summary>
    /// Realiza una animacion de rotacion sobre un <see cref="FrameworkElement"/>
    /// </summary>
    public class AnimarRotacionProperty : BaseAnimarProperty<AnimarRotacionProperty>
    {
        protected override async Task RealizarAnimacion(FrameworkElement fe, bool valor)
        {
            //Si el valor es verdadero la rotacion objetivo sera 40 grados. El margen de la izquierda pasara a ser de 120
            if (valor)
                await fe.AñadirRotacionYDesplazamiento(TimeSpan.FromSeconds(0.5f), 40f, new Thickness(120, 0,0, 0));
            //Si el valor es falso la rotacion objetivo sera 25 grados. El margen de la izquierda pasara a ser de 30
            else
                await fe.AñadirRotacionYDesplazamiento(TimeSpan.FromSeconds(0.5f), 25f, new Thickness(30, 0, 0, 0));
        }
    }
}
