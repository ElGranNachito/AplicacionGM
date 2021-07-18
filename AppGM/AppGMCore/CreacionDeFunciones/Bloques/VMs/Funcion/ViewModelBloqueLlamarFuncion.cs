using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AppGM.Core
{
	public class ViewModelBloqueLlamarFuncion : ViewModelBloqueFuncion<BloqueFuncion>
	{
		private static List<ViewModelItemComboBoxBase<object>> TiposDisponibles = new List<ViewModelItemComboBoxBase<object>>
		{
			new ViewModelItemComboBoxBase<object>{Texto = "Int", valor = typeof(int)},
			new ViewModelItemComboBoxBase<object>{Texto = "Double", valor = typeof(double)}
		};

		private object mValorSeleccionado { get; set; }

		private MethodInfo mMetodoSeleccionado { get; set; }

		public bool MostrarListaMetodosDisponibles { get; set; } = true;

		public ViewModelBloqueArgumentosFuncion ArgumentosFuncion { get; set; }

		public ViewModelListaDeElementos<ViewModelItemComboBoxBase<object>> ValoresDisponibles { get; set; } = new ViewModelListaDeElementos<ViewModelItemComboBoxBase<object>>();

		public List<ViewModelItemComboBoxBase<MethodInfo>> MetodosDisponibles { get; set; } = 
			new List<ViewModelItemComboBoxBase<MethodInfo>>();

		public ViewModelItemComboBoxBase<object> ValorSeleccionado
		{
			get => new ViewModelItemComboBoxBase<object>{Texto = mValorSeleccionado?.ToString(), valor = mValorSeleccionado};
			set
			{
				if (value?.valor != mValorSeleccionado)
				{
					mValorSeleccionado = value?.valor;

					if(mValorSeleccionado != null)
						ActualizarListaFunciones();
				}
			}
		}

		public ViewModelItemComboBoxBase<MethodInfo> MetodoSeleccionado
		{
			get => new ViewModelItemComboBoxBase<MethodInfo> { Texto = mMetodoSeleccionado?.Name, valor = mMetodoSeleccionado };
			set
			{
				if (value?.valor != mMetodoSeleccionado)
				{
					mMetodoSeleccionado = value?.valor;

					if(mMetodoSeleccionado != null)
						ArgumentosFuncion.ActualizarFuncion(value.valor);
				}
			}
		}

		public ViewModelBloqueLlamarFuncion(ViewModelCreacionDeFuncionBase _vmCreacionFuncion)
			:base(_vmCreacionFuncion)
		{
			ArgumentosFuncion = new ViewModelBloqueArgumentosFuncion(mVMCreacionDeFuncion, this, null);

			mVMCreacionDeFuncion.OnBloqueAñadido  += AñadirVariableAPosibilidades;
			mVMCreacionDeFuncion.OnBloqueRemovido += QuitarVariableDePosibilidades;

			OnPadreModificado += delegate(IContenedorDeBloques anterior, IContenedorDeBloques actual)
			{
				ActualizarValoresDisponibles();
			};
		}

		public override BloqueFuncion GenerarBloque_Impl()
		{
			if (mValorSeleccionado is BloqueVariable var)
				return new BloqueFuncionVariable(MetodoSeleccionado.valor, ArgumentosFuncion.ObtenerArgumentosFuncion(), var);

			return new BloqueFuncionTipo(MetodoSeleccionado.valor, ArgumentosFuncion.ObtenerArgumentosFuncion(),(Type)mValorSeleccionado);
		}

		private void ActualizarListaFunciones()
		{
			MetodosDisponibles.Clear();

			Type tipoSeleccionado;

			if (mValorSeleccionado is BloqueVariable var)
				tipoSeleccionado = var.tipo;
			else
				tipoSeleccionado = (Type) mValorSeleccionado;

			MetodosDisponibles = MetodosDisponibles.Concat(
				from metodo in tipoSeleccionado.GetMethods()
				where metodo.EsAccesibleEnGuraScratch()
				select new ViewModelItemComboBoxBase<MethodInfo>{Texto = metodo.Name, valor = metodo}).ToList();

			DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MetodosDisponibles)));
		}

		private void ActualizarValoresDisponibles()
		{
			ValoresDisponibles.Elementos.Clear();

			IEnumerable<ViewModelItemComboBoxBase<object>> variablesDisponibles = new List<ViewModelItemComboBoxBase<object>>();

			//TODO: Quitar este chequeo una vez cambiemos la forma en la que se diferencian los bloques de muestra de los colocados
			if (mPadre != null)
			{
				variablesDisponibles = mPadre.ObtenerVariables(this)
					.Select(var =>
					{
						return new ViewModelItemComboBoxBase<object>
						{
							Texto = var.nombre,
							valor = var
						};
					});
			}

			ValoresDisponibles.AddRange(variablesDisponibles.Concat(TiposDisponibles));
		}

		private void AñadirVariableAPosibilidades(ViewModelBloqueFuncionBase bloque, IContenedorDeBloques padre)
		{
			if (bloque is ViewModelBloqueDeclaracionVariable vmVar)
			{
				var var = vmVar.GenerarBloque_Impl();

				ValoresDisponibles.Add(new ViewModelItemComboBoxBase<object> {Texto = var.nombre, valor = var});
			}
		}

		private void QuitarVariableDePosibilidades(ViewModelBloqueFuncionBase bloque, IContenedorDeBloques padre)
		{
			if (bloque is ViewModelBloqueDeclaracionVariable vmVar)
			{
				var var = vmVar.GenerarBloque_Impl();

				ValoresDisponibles.RemoveFirst(elemento => elemento.valor == var);
			}
		}

		public override bool VerificarValidez() => ArgumentosFuncion.VerificarValidez();

		public override void OnBloqueRemovido()
		{
			mVMCreacionDeFuncion.OnBloqueAñadido  -= AñadirVariableAPosibilidades;
			mVMCreacionDeFuncion.OnBloqueRemovido -= QuitarVariableDePosibilidades;
		}
	}
}