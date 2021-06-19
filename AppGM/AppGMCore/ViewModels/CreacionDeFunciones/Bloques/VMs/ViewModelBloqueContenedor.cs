using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	public abstract class ViewModelBloqueContenedor : ViewModelBloqueFuncionBase
	{
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> Bloques { get; set; }

		public List<ViewModelBloqueDeclaracionVariable> VariablesCreadas = new List<ViewModelBloqueDeclaracionVariable>();

		public Grosor MargenContenido { get; set; }

		public ViewModelBloqueContenedor(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion)
		{
			
		}

		public void EstablecerPadre(ViewModelBloqueContenedor nuevoPadre)
		{
			if (nuevoPadre == mPadre)
				return;

			ViewModelBloqueContenedor contenedorActual = mPadre;

			float margenActual = 0; 

			while (contenedorActual != null)
			{
				margenActual += 20;

				contenedorActual = contenedorActual.mPadre;
			}

			MargenContenido = new Grosor(margenActual, 0, 0, 0);
		}

		public override List<BloqueVariable> ObtenerVariables()
		{
			if (mPadre != null)
			{
				var variablesPadre = mPadre.ObtenerVariables();
				
				variablesPadre.AddRange(
					VariablesCreadas.FindAll(elemento => elemento.EsValido)
						.Select(elemento => elemento.GenerarBloque_Impl()));
				
				return variablesPadre;
			}

			return mVMCreacionDeFuncion.ObtenerVariables(this);
		}
	}
}