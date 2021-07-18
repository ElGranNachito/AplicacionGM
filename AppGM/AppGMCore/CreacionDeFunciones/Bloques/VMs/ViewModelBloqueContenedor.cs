using System;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	public abstract class ViewModelBloqueContenedor<TipoBloque> : ViewModelBloqueFuncion<TipoBloque>, IContenedorDeBloques
		where TipoBloque : BloqueBase
	{
		#region Propiedades

		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> Bloques { get; set; } =
			new ViewModelListaDeElementos<ViewModelBloqueFuncionBase>();

		/// <summary>
		/// <see cref="ViewModelBloqueDeclaracionVariable"/> creados en este contenedor
		/// </summary>
		public List<ViewModelBloqueDeclaracionVariable> VariablesCreadas =
			new List<ViewModelBloqueDeclaracionVariable>();

		IContenedorDeBloques IContenedorDeBloques.Padre
		{
			get => Padre;
			set => Padre = value;
		}

		#endregion

		#region Constructor

		public ViewModelBloqueContenedor(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			: base(_vmCreacionDeFuncion) { } 

		#endregion

		#region Metodos

		public void AñadirBloque(ViewModelBloqueFuncionBase bloque, int indice = -1)
		{
			bloque.Padre = this;

			if (bloque is ViewModelBloqueDeclaracionVariable var)
				VariablesCreadas.Add(var);

			if (indice != -1)
			{
				Base<IContenedorDeBloques>()?.InsertarBloque(bloque, indice);

				mVMCreacionDeFuncion.DispararBloqueAñadido(bloque, this);

				return;
			}

			bloque.IndiceBloque = Bloques.Count;
			bloque.IndiceZ = IndiceZ + 1;

			Bloques.Add(bloque);

			mVMCreacionDeFuncion.DispararBloqueAñadido(bloque, this);
		}

		public void QuitarBloque(ViewModelBloqueFuncionBase bloque)
		{
			Bloques.Remove(bloque);

			mVMCreacionDeFuncion.DispararBloqueRemovido(bloque, this);

			Base<IContenedorDeBloques>()?.ActualizarIndicesBloques(bloque.IndiceBloque);
		}

		public List<BloqueVariable> ObtenerVariables(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables)
		{
			//Obtenemos las variables del padre a las que podemos acceder
			var variablesPadre = mPadre.ObtenerVariables(this);

			//Añadimos a esas variables las declaradas en este mismo bloque
			variablesPadre.AddRange(
				VariablesCreadas.FindAll(elemento => elemento.EsValido)
					.Select(elemento => elemento.GenerarBloque_Impl()));

			return variablesPadre;
		} 

		#endregion
	}
}
