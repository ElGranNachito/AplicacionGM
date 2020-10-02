using System;
using AppGM.Core;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
    public class BotonMenuRolConViewModelProperty : BaseAttachedProperty<BaseViewModel, BotonMenuRolConViewModelProperty>
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseViewModel vm = ((IViewModelConBotonSeleccionado)(BaseViewModel)e.NewValue).ViewModelConBotonSeleccionado;
            IBotonSeleccionado<BaseViewModel> botonSeleccionado = (IBotonSeleccionado<BaseViewModel>) vm;

            if (d is Button b)
            {
                b.Click += (o, ea) =>
                {
                    if (botonSeleccionado.BotonSeleccionado == b.DataContext)
                        return;

                    b.BorderThickness = new Thickness(0, 0, 2, 0);

                    botonSeleccionado.BotonSeleccionado = (BaseViewModel) b.DataContext;

                    PropertyChangedEventHandler propertyChangedEventListener = null;

                    propertyChangedEventListener = (o2, ea2) =>
                    {
                        if (ea2.PropertyName != nameof(botonSeleccionado.BotonSeleccionado))
                            return;

                        b.BorderThickness = new Thickness(0);
                        vm.PropertyChanged -= propertyChangedEventListener;
                    };

                    vm.PropertyChanged += propertyChangedEventListener;
                };
            }
        }
    }
}
