using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{

	/// <summary>
	/// <see cref="ViewModel"/> para un control que represente una condicion
	/// </summary>
	public class ViewModelBloqueCondicionalBase : ViewModelBloqueContenedor<BloqueCondicional>
	{
		/// <summary>
		/// Nombre de este bloque
		/// </summary>
		public string NombreCondicional { get; set; }

		/// <summary>
		/// Contiene todos los <see cref="ViewModelArgumento"/> y <see cref="EOperacionLogica"/>
		/// que realizar con esos argumentos
		/// </summary>
		public ViewModelSeccionesCondicion ArgumentosCondicion { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> que contiene este bloque</param>
		public ViewModelBloqueCondicionalBase(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion)
		{
			ArgumentosCondicion = new ViewModelSeccionesCondicion(_vmCreacionDeFuncion, this);
		}

		public override BloqueCondicional GenerarBloque_Impl()
		{
			IEnumerable<BloqueArgumento> argumentos = ArgumentosCondicion.argumentos.Select(arg => arg.GenerarBloque_Impl());

			return new BloqueCondicional(argumentos.ToList(), ArgumentosCondicion.operaciones);
		}
	}
}