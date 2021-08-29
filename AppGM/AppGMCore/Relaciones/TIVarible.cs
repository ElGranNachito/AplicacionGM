using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una relacion con un <see cref="ModeloVariableBase"/>
	/// </summary>
	public abstract class TIVarible
	{
		[ForeignKey(nameof(Variable))]
		public int IdVariable { get; set; }
		
		public virtual ModeloVariableBase Variable { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloVariableBase"/> y un <see cref="ModeloPersonaje"/>
	/// </summary>
	public class TIVariablePersonaje : TIVarible
	{
		[ForeignKey(nameof(Personaje))]
		public int IdPersonaje { get; set; }

		public virtual ModeloPersonaje Personaje { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloVariableBase"/> y un <see cref="ModeloHabilidad"/>
	/// </summary>
	public class TIVariableHabilidad : TIVarible
	{
		[ForeignKey(nameof(Habilidad))]
		public int IdHabilidad { get; set; }

		public virtual ModeloHabilidad Habilidad { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloVariableBase"/> y un <see cref="ModeloUtilizable"/>
	/// </summary>
	public class TIVariableUtilizable : TIVarible
	{
		[ForeignKey(nameof(Utilizable))]
		public int IdUtilizable { get; set; }

		public virtual ModeloUtilizable Utilizable { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloVariableBase"/> y un <see cref="ModeloFuncion"/>
	/// </summary>
	public class TIVariableFuncion : TIVarible
	{
		[ForeignKey(nameof(Funcion))]
		public int IdFuncion { get; set; }

		public virtual ModeloFuncion Funcion { get; set; }
	}
}
