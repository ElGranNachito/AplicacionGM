﻿using System;
using System.Windows;
using System.Windows.Input;

using AppGM.Core;
using AppGM.Core.Delegados;

namespace AppGM
{
	/// <summary>
	/// Propiedad para la que implementen <see cref="FrameworkElement"/> que deseen lidiar con eventos de <see cref="Drag"/>.
	/// Su <see cref="ViewModel"/> debe implementar <see cref="IReceptorDeDrag"/>
	/// </summary>
	public class DragAndDrop_ReceptorDrag : BaseAttachedProperty<bool, DragAndDrop_ReceptorDrag>
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement fe)
			{
				RoutedEventHandler elementoCargadoHandler = null;

				elementoCargadoHandler = (sender, args) =>
				{
					MouseEventHandler mouseEnterHandler = null;
					MouseEventHandler mouseLeaveHandler = null;

					DDrag dragComenzadoHandler = null;
					DDrag dragFinalizadoHandler = null;

					Action<object> comenzarDrag = control =>
					{
						if (SistemaPrincipal.Drag.HayUnDragActivo
						    && control is FrameworkElement fe
						    && fe.DataContext is IReceptorDeDrag vm)
						{
							fe.MouseLeave += mouseLeaveHandler;

							SistemaPrincipal.Drag.OnFinDrag += dragFinalizadoHandler;

							SistemaPrincipal.Drag.AñadirReceptorDrag(vm);
						}
					};

					dragComenzadoHandler = contenido =>
					{
						if (fe.IsMouseOver)
							comenzarDrag(fe);
					};

					mouseEnterHandler = (sender, args) =>
					{
						comenzarDrag(sender);
					};

					mouseLeaveHandler = (sender, args) =>
					{
						if (SistemaPrincipal.Drag.HayUnDragActivo &&
						    sender is FrameworkElement fe &&
						    fe.DataContext is IReceptorDeDrag vm)
						{
							fe.MouseLeave -= mouseLeaveHandler;

							SistemaPrincipal.Drag.OnFinDrag -= dragFinalizadoHandler;

							SistemaPrincipal.Drag.QuitarReceptorDrag(vm);
						}
					};

					dragFinalizadoHandler = contenido =>
					{
						fe.MouseLeave -= mouseLeaveHandler;
						SistemaPrincipal.Drag.OnFinDrag -= dragFinalizadoHandler;
					};

					SistemaPrincipal.Drag.OnComienzoDrag += dragComenzadoHandler;

					fe.MouseEnter += mouseEnterHandler;

					fe.Loaded -= elementoCargadoHandler;
				};

				fe.Loaded += elementoCargadoHandler;
			}
		}
	}
}