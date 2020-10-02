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
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StackPanel sp = (StackPanel) d;

            RoutedEventHandler loadedEventListener = null;

            loadedEventListener = (o, re) =>
            {
                //Desuscribimos la lambda
                sp.Loaded -= loadedEventListener;

                //Obtenemos el tipo de elemento que estamos buscando
                Type tipo = (Type)e.NewValue;

                for (int i = 0; i < sp.Children.Count; ++i)
                {
                    FrameworkElement doc = (FrameworkElement) sp.Children[i];

                    //Si el tipo del child actual es igual al que estamos buscando establecemos su margen
                    if (doc.GetType() == tipo)
                        doc.Margin = ParametroThicknessProperty.GetParametro(d);
                }
            };

            sp.Loaded += loadedEventListener;
        }
    }
}
