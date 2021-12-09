using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte un valor de <see cref="ETipoTirada"/> a su respectiva imagen
	/// </summary>
	[ValueConversion(typeof(ETipoTirada), typeof(ImageBrush))]
	public class TipoTiradaToImagenConverter : BaseConverter<TipoTiradaToImagenConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ETipoTirada tipoTirada)
			{
				var imagenResultado = new Image();

				imagenResultado.BeginInit();

				switch (tipoTirada)
				{
					case ETipoTirada.Daño:
						imagenResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Tiradas/Iconos/Stat.png"));
						break;
					case ETipoTirada.Personalizada:
						imagenResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Tiradas/Iconos/TiradaPersonalizada.png"));
						break;
					case ETipoTirada.Stat:
						imagenResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Tiradas/Iconos/TiradaStat.png"));
						break;
				}

				imagenResultado.EndInit();

				if(imagenResultado.Source.CanFreeze)
					imagenResultado.Source.Freeze();

				return imagenResultado;
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} debe ser de tipo {nameof(ETipoTirada)}", ESeveridad.Error);

			return null;
		}
	}
}
