using System;
using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
    /// <summary>
    /// Attached Property para encontrar elementos de determinado tipo dentro de un contenedor y añadir margen automaticamente
    /// Para que funcione se necesita que el elemento contenedor tambien defina una <see cref="ParametroThicknessProperty"/>
    /// </summary>
    public class AñadirMargenAElementosProperty : BaseAttachedProperty<Type, AñadirMargenAElementosProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Panel panel = (Panel) d;

            RoutedEventHandler loadedEventListener = null;

            loadedEventListener = (o, re) =>
            {
                //Desuscribimos la lambda
                panel.Loaded -= loadedEventListener;

                //Obtenemos el tipo de elemento que estamos buscando
                Type tipo = (Type)e.NewValue;

                for (int i = 0; i < panel.Children.Count; ++i)
                {
                    FrameworkElement doc = (FrameworkElement)panel.Children[i];
                    
                    //Si el tipo del child actual es igual al que estamos buscando establecemos su margen
                    if (doc != null && doc.GetType() == tipo)
                        doc.Margin = ParametroThicknessProperty.GetParametro(d);
                }
            };

            panel.Loaded += loadedEventListener;
        }
    }
}
