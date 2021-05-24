using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Clase derivada de <see cref="TriggerActionBase"/> que alterna entre dos valores de una propiedad
	/// </summary>
	class TriggerActionFlipFlop : TriggerActionBase
	{
		private PropertyInfo mPropiedadVM;

		private PropertyInfo PropiedadVM
		{
			get
			{
				if (mPropiedadVM != null)
					return mPropiedadVM;

				if (Destino is FrameworkElement elemento)
					mPropiedadVM = elemento.DataContext.GetType().GetProperty(NombrePropiedadVM);

				return mPropiedadVM;
			}
		}

		public static readonly DependencyProperty NombrePropiedadVMProperty = 
			DependencyProperty.Register("NombrePropiedadVM", typeof(string), typeof(TriggerActionFlipFlop));

		public string NombrePropiedadVM
		{
			get => GetValue(NombrePropiedadVMProperty) as string;
			set => SetValue(NombrePropiedadVMProperty, value);
		}

		protected override void Invoke(object obj)
		{
			//Guardamos el valor actual de la propiedad
			var valorActualPropiedad = Propiedad.GetValue(Destino);

			//Cambiamos el valor de la propiedad
			Propiedad.SetValue(Destino, Valor);
			
			//Guardamos en la propiedad Valor el valor anterior
			Valor = valorActualPropiedad;

			//Si no nos pasaron nada sobre la propiedad del view model retornamos
			if (string.IsNullOrWhiteSpace(NombrePropiedadVM))
				return;

			//Hacemos un try porque estamos haciendo operacion bastantes peligrosas
			try
			{
				if(Destino is FrameworkElement elemento)
					PropiedadVM.SetValue(elemento.DataContext, !(bool)PropiedadVM.GetValue(elemento.DataContext));
			}
			//Logueamos la aplicacion y continuamos la ejecucion
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log(ex.Message, ESeveridad.Error);
				Debugger.Break();
			}
		}
	}
}
