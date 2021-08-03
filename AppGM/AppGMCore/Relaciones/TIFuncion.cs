using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Clase base que representa una relacion con un <see cref="ModeloFuncion"/>
	/// </summary>
	public class TIFuncion : ModeloBaseSK
	{
		[ForeignKey(nameof(Funcion))]
		public int IDFuncion { get; set; }

		public virtual ModeloFuncion Funcion { get; set; }
	}

	/// <summary>
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloVariableFuncionBase"/>
	/// </summary>
	public class TIFuncionVariable : TIFuncion
	{
		[ForeignKey(nameof(Variable))]
		public int IDVariable { get; set; }

		public virtual ModeloVariableFuncionBase Variable { get; set; }
	}

	/// <summary>
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloEfecto"/>
	/// </summary>
	public class TIFuncionEfecto : TIFuncion
	{
		[ForeignKey(nameof(Efecto))]
		public int IDEfecto { get; set; }

		public virtual ModeloEfecto Efecto { get; set; }
	}

	/// <summary>
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloHabilidad"/>
	/// </summary>
	public class TIFuncionHabilidad : TIFuncion
	{
		[ForeignKey(nameof(Habilidad))]
		public int IDHabilidad { get; set; }

		public virtual ModeloHabilidad Habilidad { get; set; }
	}
}