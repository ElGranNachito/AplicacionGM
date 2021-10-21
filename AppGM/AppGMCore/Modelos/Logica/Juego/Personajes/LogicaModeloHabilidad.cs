using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Contiene la logica del <see cref="ModeloHabilidad"/>
	/// </summary>
	public partial class ModeloHabilidad
	{
		public override ModeloPersonaje ObtenerPersonajeContenedor() => Dueño;
	}
}
