using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	public class BoolToImagenGloboCroto : BaseConverter<BoolToImagenGloboCroto>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b)
			{
				return new BitmapImage(new Uri(
					b
						? "pack://application:,,,/Media/Imagenes/Tiradas/GloboTipoTirada.png" 
						: "pack://application:,,,/Media/Imagenes/Tiradas/GloboError.png"));
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} debe ser un booleano", ESeveridad.Error);

			return new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/Tiradas/GloboTipoTirada.png"));
		}
	}
}
