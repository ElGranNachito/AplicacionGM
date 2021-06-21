using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Ninject.Infrastructure.Language;

namespace AppGM.Core
{
	public class ViewModelBloqueLlamarFuncion : ViewModelBloqueFuncion<BloqueFuncion>
	{
		private Type mTipoSeleccionado { get; set; }

		private MethodInfo mMetodoSeleccionado { get; set; }

		public bool MostrarListaMetodosDisponibles { get; set; } = true;

		public ViewModelBloqueArgumentosFuncion ArgumentosFuncion { get; set; }

		public List<ViewModelItemComboBoxBase<Type>> TiposDisponibles { get; set; } = new List<ViewModelItemComboBoxBase<Type>>
		{
			new ViewModelItemComboBoxBase<Type>{Texto = "Int", valor = typeof(int)},
			new ViewModelItemComboBoxBase<Type>{Texto = "Controlador Habilidad", valor = typeof(ControladorHabilidad)}
		};

		public List<ViewModelItemComboBoxBase<MethodInfo>> MetodosDisponibles { get; set; } = 
			new List<ViewModelItemComboBoxBase<MethodInfo>>();

		public ViewModelItemComboBoxBase<Type> TipoSeleccionado
		{
			get => new ViewModelItemComboBoxBase<Type>{Texto = mTipoSeleccionado?.ToString(), valor = mTipoSeleccionado};
			set
			{
				if (value.valor != mTipoSeleccionado)
				{
					mTipoSeleccionado = value.valor;

					ActualizarListaFunciones();
				}
			}
		}

		public ViewModelItemComboBoxBase<MethodInfo> MetodoSeleccionado
		{
			get => new ViewModelItemComboBoxBase<MethodInfo> { Texto = mMetodoSeleccionado?.Name, valor = mMetodoSeleccionado };
			set
			{
				if (value.valor != mMetodoSeleccionado)
				{
					mMetodoSeleccionado = value.valor;

					ArgumentosFuncion.ActualizarFuncion(value.valor);
				}
			}
		}

		public ViewModelBloqueLlamarFuncion(ViewModelCreacionDeFuncionBase _vmCreacionFuncion)
			:base(_vmCreacionFuncion)
		{
			ArgumentosFuncion = new ViewModelBloqueArgumentosFuncion(mVMCreacionDeFuncion, null);
		}

		private void ActualizarListaFunciones()
		{
			MetodosDisponibles.Clear();

			MetodosDisponibles = MetodosDisponibles.Concat(
				from metodo in mTipoSeleccionado.GetMethods()
				where metodo.HasAttribute(typeof(AccesibleEnGuraScratch))
				select new ViewModelItemComboBoxBase<MethodInfo>{Texto = metodo.Name, valor = metodo}).ToList();

			DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MetodosDisponibles)));
		}

		public override BloqueFuncion GenerarBloque_Impl()
		{
			throw new System.NotImplementedException();
		}

		public override bool VerificarValidez() => ArgumentosFuncion.VerificarValidez();
	}
}