using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// Attached property que permite la seleccion de varios elementos en un control
	/// </summary>
	public class DragAndDrop_DrageableSeleccionMultiple : BaseAttachedProperty<bool, DragAndDrop_DrageableSeleccionMultiple>
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement fe)
			{
				RoutedEventHandler loadedEventHandler = null;

				loadedEventHandler = (sender, args) =>
				{
					if(fe.DataContext is ViewModelElementoArbolBase seleccionable)
						PrepararEventos(fe, seleccionable);
				};

				fe.Loaded += loadedEventHandler;
			}
		}

		/// <summary>
		/// Se subscribe a los eventos necesarios del <paramref name="fe"/> para implementar la seleccion multiple
		/// </summary>
		/// <param name="fe">Elemento que implementa la seleccion multiple</param>
		/// <param name="seleccionable"><see cref="ViewModelElementoArbolBase"/></param>
		private void PrepararEventos(FrameworkElement fe, ViewModelElementoArbolBase seleccionable)
		{
			fe.PreviewMouseDown += (sender, args) =>
			{
				var hostDragAndDrop = seleccionable.HostDragAndDrop;

				//Guardamos si el item esta previamente seleccionado
				var estabaPreviamenteSeleccionado = seleccionable.EstaSeleccionado;

				seleccionable.EstaSeleccionado = true;

				//Si cualquiera de las teclas ctrl esta presionada significa que estamos realizando seleccion multiple asi que nos pegamos la vuelta
				//
				//Si el item estaba previamente seleccionado entonces tambien nos pegamos la vuelta porque entonces o queremos arrastrar varios elementos
				//o hicimos un click sobre un elemento que ya se encontraba seleccionado por lo que no vale la pena ejecutar el siguiente codigo
				if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || estabaPreviamenteSeleccionado)
				{
					return;
				}

				//Deseleccionamos los elementos seleccionados que no sean este seleccionable
				foreach (var seleccionableActual in hostDragAndDrop.ElementosSeleccionados.Cast<ViewModelElementoArbolBase>().ToList())
				{
					if (seleccionableActual != seleccionable)
						seleccionableActual.EstaSeleccionado = false;
				}
			};

			fe.PreviewMouseUp += (sender, args) =>
			{
				//Si estamos haciendo seleccion multiple nos pegamos la vuelta
				if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || seleccionable.HostDragAndDrop.ElementosSeleccionados.Count <= 1)
					return;

				//Deseleccionamos todos los elementos seleccionados que no sean este seleccionable
				foreach (var seleccionableActual in seleccionable.HostDragAndDrop.ElementosSeleccionados.Cast<ViewModelElementoArbolBase>().ToList())
				{
					if (seleccionableActual != seleccionable)
						seleccionableActual.EstaSeleccionado = false;
				}
			};
		}
	}
}
