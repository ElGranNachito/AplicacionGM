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
	public class ReceptorDeDragProperty : BaseAttachedProperty<bool, ReceptorDeDragProperty>
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement fe)
			{
				MouseEventHandler mouseEnterHandler = null;
				MouseEventHandler mouseLeaveHandler = null;
				DDrag finDragHandler = null;

				mouseEnterHandler = (sender, args) =>
				{
					if (SistemaPrincipal.Drag.HayUnDragActivo 
					    && sender is FrameworkElement fe
					    && fe.DataContext is IReceptorDeDrag vm)
					{
						fe.MouseLeave += mouseLeaveHandler;
						SistemaPrincipal.Drag.OnFinDrag += finDragHandler;

						vm.OnDragEnter(SistemaPrincipal.Drag.ViewModelContenido);
					}
				};

				mouseLeaveHandler = (sender, args) =>
				{
					if (sender is FrameworkElement fe 
					    && fe.DataContext is IReceptorDeDrag vm)
					{
						fe.MouseLeave -= mouseLeaveHandler;
						SistemaPrincipal.Drag.OnFinDrag -= finDragHandler;

						vm.OnDragLeave(SistemaPrincipal.Drag.ViewModelContenido);
					}
				};

				finDragHandler = contenido =>
				{
					if (fe.DataContext is IReceptorDeDrag vm)
						vm.OnDrop(contenido);
				};

				fe.MouseEnter += mouseEnterHandler;
			}
		}
	}
}
