using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

namespace AppGM
{
	/// <summary>
	/// Accion que se puede llamar desde un evento
	/// </summary>
	public class TriggerActionBase : TriggerAction<DependencyObject>
	{
		//Propiedad cuyo valor cambiaremos
		protected PropertyInfo mPropiedad;

		//Inicializamos de mPropiedad perezosamente
		protected PropertyInfo Propiedad => mPropiedad ?? Destino.GetType().GetProperty(Nombre, BindingFlags.Public | BindingFlags.Instance);

		/// <summary>
		/// Nombre de la propiedad cuyo valor cambiaremos
		/// </summary>
		public static readonly DependencyProperty NombreProperty = 
			DependencyProperty.Register("Nombre", typeof(string), typeof(TriggerActionBase));

		public virtual string Nombre
		{
			get => GetValue(NombreProperty) as string;
			set => SetValue(NombreProperty, value);
		}

		/// <summary>
		/// Valor que le daremos a la propiedad
		/// </summary>
		public static readonly DependencyProperty ValorProperty =
			DependencyProperty.Register("Valor", typeof(object), typeof(TriggerActionBase));

		public virtual object Valor
		{
			get => GetValue(ValorProperty);
			set => SetValue(ValorProperty, value);
		}

		/// <summary>
		/// Instancia de la clase que alberga la propiedad
		/// </summary>
		public static readonly DependencyProperty DestinoProperty =
			DependencyProperty.Register("Destino", typeof(object), typeof(TriggerActionBase));

		public virtual object Destino
		{
			get => GetValue(DestinoProperty);
			set => SetValue(DestinoProperty, value);
		}

		/// <summary>
		/// Funcion que se llama cuando el evento se dispara
		/// </summary>
		/// <param name="parameter">Parametro del evento</param>
		protected override void Invoke(object parameter)
		{
			if (Propiedad != null)
				Propiedad.SetValue(Destino, Valor);
		}
	}
}