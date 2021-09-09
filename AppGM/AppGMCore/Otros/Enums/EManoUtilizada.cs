using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Indica que mano utiliza un personaje para realizar una accion
	/// </summary>
	public enum EManoUtilizada
	{
		Dominante   = 0,
		NoDominante = 1,
		AmbasManos  = 2,
		Ninguna     = 3
	}
}
