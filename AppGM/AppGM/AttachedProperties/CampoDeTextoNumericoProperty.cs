using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    public class CampoDeTextoNumericoProperty : BaseAttachedProperty<bool, CampoDeTextoNumericoProperty>
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)d;
            textBox.Text = "0";

            textBox.TextChanged += (o, eargs) =>
            {
                if (textBox.Text.Length == 0)
                    return;

                if (!textBox.Text.Last().EsUnNumero())
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            };

            textBox.LostFocus += (o, eargs) =>
            {
                if (textBox.Text.Length == 0)
                    textBox.Text = "0";
            };

            textBox.GotFocus += (o, eargs) =>
            {
                if (textBox.Text.Length == 1 && textBox.Text[0].Equals('0'))
                    textBox.Clear();
            };
        }
    }
}
