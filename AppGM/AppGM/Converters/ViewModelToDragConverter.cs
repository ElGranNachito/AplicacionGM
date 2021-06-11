﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// Convierte un <see cref="BaseViewModel"/> a un <see cref="UserControl"/>.
	/// Este convertidor toma como parametro la ventana en la que se realiza la conversion para determinar si es la ventana
	/// actualmente siendo utilizada. Esto sirve para no crear un drag en dos ventana a la vez.
	/// </summary>
	[ValueConversion(sourceType: typeof(BaseViewModel), targetType: typeof(UserControl), ParameterType = typeof(Window))]
	public class ViewModelToDragConverter : BaseConverter<ViewModelToDragConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//Revisamos que la ventana sea la actualmente seleccionada
			switch (value)
			{
				case ViewModelCarta vmCarta:
					return new UserControlCarta
					{
						DataContext = vmCarta,
						MaxWidth    = 100,
						MaxHeight   = 125,
						Width = 100,
						Height = 125
					};
				default:
					return null;
			}
		}
	}
}