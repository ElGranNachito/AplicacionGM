using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloConVariablesYTiradas"/> y un <see cref="ModeloTiradaBase"/>
	/// </summary>
	public class TITirada
	{
		[ForeignKey(nameof(ModeloContenedorTirada))]
		public int IdModelo { get; set; }

		public virtual ModeloConVariablesYTiradas ModeloContenedorTirada { get; set; }

		[ForeignKey(nameof(Tirada))]
		public int IdTirada { get; set; }

		public virtual ModeloTiradaBase Tirada { get; set; } 
	}
}
