using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Property para <see cref="TextBox"/> cuyo proposito sea el ingreso de digitos
    /// </summary>
    public class CampoDeTextoNumericoProperty : BaseAttachedProperty<bool, CampoDeTextoNumericoProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			//Nos aseguramos que el objeto sea un TextBox
	        if (d is TextBox textBox)
	        {
				//Si esta vacio simplemente retornamos 0
		        if (textBox.Text == string.Empty)
			        textBox.Text = "0";

				//Cuando el texto sea modificado...
		        textBox.TextChanged += (o, eargs) =>
		        {
					//Si esta vacio no hacemos nada
			        if (textBox.Text.Length == 0)
				        return;

					//Si el ultimo caracter añadido no es un numero lo sacamos
			        if (!textBox.Text.Last().ToString().EsUnNumero())
				        textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
		        };

				//Cuando el usuario deje de editar el texto...
		        textBox.LostFocus += (o, eargs) =>
		        {
					//Si esta vacio añadimos un '0' al texto
			        if (textBox.Text.Length == 0)
				        textBox.Text = "0";
		        };

				//Cuando el usuario seleccione el control...
		        textBox.GotFocus += (o, eargs) =>
		        {
					//Si solo hay un caracter y ese caracter es un cero entonces eliminamos el texto
			        if (textBox.Text.Length == 1 && textBox.Text[0].Equals('0'))
				        textBox.Clear();
		        };
	        }
        }
    }
}
