using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// Obtiene una <see cref="BitmapImage"/> que representa el contenido de un <see cref="ControladorSlot"/>
	/// </summary>
	[ValueConversion(sourceType: typeof(ControladorSlot), targetType: typeof(BitmapImage))]
	public class ContenidoSlotToImagenConverter : BaseConverter<ContenidoSlotToImagenConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//Nos aseguramos de que el valor que nos pasaron es un slot
			if (value is ControladorSlot slot)
			{
				if (slot.ContieneParteDelCuerpo)
				{
					return new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Botones/MenupRincipal/Flechita_Derecha_Select.png"));
				}
				else if (slot.ContieneItems)
				{
					return new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Botones/MenupRincipal/Flechita_Derecha_Select.png"));
				}
				else
				{
					return new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Botones/MenupRincipal/Flechita_Derecha_Select.png"));
				}
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} debe ser un {nameof(ControladorSlot)}");

			return null;
		}
	}
}
