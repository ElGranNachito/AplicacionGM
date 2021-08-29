using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloConVariablesYTiradas"/> y un <see cref="ModeloVariableBase"/>
	/// </summary>
	public class TIVarible
	{
		[ForeignKey(nameof(ModeloContenedorVar))]
		public int IdModelo { get; set; }

		public virtual ModeloConVariablesYTiradas ModeloContenedorVar { get; set; }

		[ForeignKey(nameof(Variable))]
		public int IdVariable { get; set; }

		public virtual ModeloVariableBase Variable { get; set; }
	}
}
