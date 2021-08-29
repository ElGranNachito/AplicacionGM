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
	/// Clase que representa una relacion de un <see cref="ModeloFuncion"/> con otro <see cref="ModeloFuncion"/> que actua como su padre
	/// </summary>
	public class TIFuncionPadreFuncion : TIFuncion
	{
		public int IDPadre { get; set; }

		public virtual ModeloFuncion Padre { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloBase"/> y un <see cref="ModeloFuncion"/> contenido en este
	/// </summary>
	public class TIFuncionContenedor : TIFuncion
	{
		[ForeignKey(nameof(Contenedor))]
		public int IdContenedor { get; set; }

		public virtual ModeloBase Contenedor { get; set; }
	}

	/// <summary>
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloVariableBase"/>
	/// </summary>
	public class TIFuncionVariable : TIFuncion
	{
		[ForeignKey(nameof(Variable))]
		public int IDVariable { get; set; }

		public virtual ModeloVariableBase Variable { get; set; }
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