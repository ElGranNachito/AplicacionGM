using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

using AppGM.Core;

using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Convierte de un <see cref="EDireccionItems"/> a su equivalente en <see cref="Orientation"/>
	/// </summary>
	[ValueConversion(sourceType: typeof(EDireccionItems), targetType: typeof(Orientation))]
	class DireccionToOrientacionConverter : BaseConverter<DireccionToOrientacionConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is EDireccionItems direccionItems)
			{
				switch (direccionItems)
				{
					case EDireccionItems.Horizontal:
						return Orientation.Horizontal;
					case EDireccionItems.Vertical:
						return Orientation.Vertical;
				}
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} no es de tipo {nameof(EDireccionItems)}", ESeveridad.Error);

			return Orientation.Vertical;
		}
	}
}