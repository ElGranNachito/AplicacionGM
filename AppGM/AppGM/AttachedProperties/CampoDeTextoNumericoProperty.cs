using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
    /// <summary>
    /// Property para <see cref="TextBox"/> cuyo proposito sea el ingreso de digitos
    /// </summary>
    public class CampoDeTextoNumericoProperty : BaseAttachedProperty<bool, CampoDeTextoNumericoProperty>
    {
		#region TipoProperty

		/// <summary>
		/// Propiedad que contiene el <see cref="Type"/> del valor numerico aceptado por el campo de texto
		/// </summary>
		public static readonly DependencyProperty TipoProperty = DependencyProperty.RegisterAttached("Tipo", typeof(Type), typeof(CampoDeTextoNumericoProperty), new PropertyMetadata(typeof(int), OnTipoChanged));

		/// <summary>
		/// Establece el valor de <see cref="TipoProperty"/> para <paramref name="o"/>
		/// </summary>
		/// <param name="o">Objeto en el que se guardara <paramref name="valor"/></param>
		/// <param name="valor">Valor que se guardara</param>
		public static void SetTipo(DependencyObject o, Type valor) => o.SetValue(TipoProperty, valor);

		/// <summary>
		/// Establece el valor de <see cref="TipoProperty"/> guardado en <paramref name="o"/>
		/// </summary>
		/// <param name="o">Objeto que contiene el valor de <see cref="TipoProperty"/></param>
		/// <returns>Valor de <see cref="TipoProperty"/> guardado en <paramref name="o"/></returns>
		public static Type GetTipo(DependencyObject o) => o.GetValue(TipoProperty) as Type;

		#endregion

		#region Campos

		/// <summary>
		/// Lambda que lidia con los eventos de texto modificado de los <see cref="TextBox"/> que
		/// utilicen esta attached property
		/// </summary>
		private static TextChangedEventHandler mTextChangedHandler = (o, eargs) =>
		{
			if (o is TextBox textBox)
			{
				//Si esta vacio no hacemos nada
				if (textBox.Text.Length == 0)
					return;

				Type tipoActual = GetTipo(textBox);

				//Nos desubscribimos del evento para que no se dispara cuando cambiemos el texto mas abajo
				textBox.TextChanged -= mTextChangedHandler;

				QuitarCaracteresNoPermitidos(textBox, tipoActual);

				//Nos volvemos a subscribir al evento de text changed
				textBox.TextChanged += mTextChangedHandler;
			}
		};

		/// <summary>
		/// Lambda que lidia con los eventos de GotFocus de los <see cref="TextBox"/> que
		/// utilicen esta attached property
		/// </summary>
		private static RoutedEventHandler mGotFocusHandler = (o, eargs) =>
		{
			if (o is TextBox textBox)
			{
				//Si solo hay un caracter y ese caracter es un cero entonces eliminamos el texto
				if (textBox.Text.Length == 1 && textBox.Text[0].Equals('0'))
					textBox.Clear();
			}
		};

		/// <summary>
		/// Lambda que lidia con los eventos de LostFocus de los <see cref="TextBox"/> que
		/// utilicen esta attached property
		/// </summary>
		private static RoutedEventHandler mLostFocusHandler = (o, eargs) =>
		{
			if (o is TextBox textBox)
			{
				//Si esta vacio añadimos un '0' al texto
				if (textBox.Text.Length == 0)
					textBox.Text = "0";
			}
		};

		#endregion

		#region Metodos

		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			//Nos aseguramos que el objeto sea un TextBox
			if (d is TextBox textBox)
			{
				//Si esta vacio simplemente retornamos 0
				if (textBox.Text == string.Empty)
					textBox.Text = "0";

				Type tipo = GetTipo(textBox);

				if (tipo != typeof(int) && tipo != typeof(float) && tipo != typeof(double))
				{
					SistemaPrincipal.LoggerGlobal.Log($"{tipo} no es un tipo aceptado", ESeveridad.Error);

					return;
				}

				if (e.NewValue is bool b)
				{
					//Si el campo de texto numerico esta habilitado
					if (b)
					{
						//Cuando el texto sea modificado...
						textBox.TextChanged += mTextChangedHandler;

						//Cuando el usuario seleccione el control...
						textBox.GotFocus += mGotFocusHandler;

						//Cuando el usuario deje de editar el texto...
						textBox.LostFocus += mLostFocusHandler;
					}
					//Si no lo esta
					else
					{
						textBox.TextChanged -= mTextChangedHandler;
						textBox.GotFocus -= mGotFocusHandler;
						textBox.LostFocus -= mLostFocusHandler;
					}
				}
			}
		}

		/// <summary>
		/// Metodo que lidia con el cambio de valor de <see cref="TipoProperty"/>
		/// </summary>
		/// <param name="d">Objeto que contiene la propiedad</param>
		/// <param name="args">Argumentos del evento</param>
		private static void OnTipoChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			//Si el campo de texto numerico esta deshabilitado para esto objeto nos pegamos media vuelta
			if (!GetValue(d))
				return;

			Type tipoActual = (Type)args.NewValue;

			if (tipoActual != typeof(int) || tipoActual != typeof(float) || tipoActual != typeof(double))
			{
				if (d is TextBox t)
				{
					QuitarCaracteresNoPermitidos(t, tipoActual);
				}
				else
				{
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(d)} no es un {nameof(TextBox)}", ESeveridad.Error);
				}
			}
			else
			{
				SistemaPrincipal.LoggerGlobal.Log($"{tipoActual} no es un tipo aceptado", ESeveridad.Advertencia);
			}
		}

		/// <summary>
		/// Quita los caracteres no permitidos del texto de <paramref name="textBox"/>
		/// </summary>
		/// <param name="textBox">Textbox cuyo texto purgar</param>
		/// <param name="tipoActual">Tipo numerico actual de <paramref name="textBox"/></param>
		private static void QuitarCaracteresNoPermitidos(TextBox textBox, Type tipoActual)
		{
			int posicionCursor = textBox.CaretIndex;

			//Si el tipo no es un entero entonces debe ser un tipo con decimales
			if (tipoActual != typeof(int))
			{
				//Reemplazamos todos los puntos por comas
				string cadenaFinal = textBox.Text.Replace('.', ',');

				//Quitamos todos los caracteres que no sean un numero o una coma
				cadenaFinal = Regex.Replace(textBox.Text, "[^0-9,]", "");

				//Si hay mas de un punto
				if (cadenaFinal.Count(c => c == '.') > 1)
				{
					int indicePrimerPunto = cadenaFinal.IndexOf('.');

					int indiceSegundoPunto = cadenaFinal.IndexOf('.', indicePrimerPunto + 1);

					//Quitamos todos los caracteres del segundo punto en adelante
					cadenaFinal = cadenaFinal.Remove(indiceSegundoPunto);
				}

				textBox.Text = cadenaFinal;
			}
			else
			{
				textBox.Text = Regex.Replace(textBox.Text, "\\D", "");
			}

			textBox.CaretIndex = Math.Min(textBox.Text.Length, posicionCursor);
		} 

		#endregion
	}
}