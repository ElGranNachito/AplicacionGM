using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloConVariablesYTiradas{TRelacionVariable}"/> y un <see cref="ModeloTiradaBase"/>
	/// </summary>
	public abstract class TITirada : ModeloBaseSK
	{
		[ForeignKey(nameof(Tirada))]
		public int IdTirada { get; set; }

		public virtual ModeloTiradaBase Tirada { get; set; } 
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloPersonaje"/> y un <see cref="ModeloTiradaBase"/>
	/// </summary>
	public class TITiradaPersonaje : TITirada
	{
		[ForeignKey(nameof(Personaje))]
		public int IdPersonaje { get; set; }

		public virtual ModeloPersonaje Personaje { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloHabilidad"/> y un <see cref="ModeloTiradaBase"/>
	/// </summary>
	public class TITiradaHabilidad : TITirada
	{
		[ForeignKey(nameof(Habilidad))]
		public int IdHabilidad { get; set; }

		public virtual ModeloHabilidad Habilidad { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloUtilizable"/> y un <see cref="ModeloTiradaBase"/>
	/// </summary>
	public class TITiradaUtilizable : TITirada
	{
		[ForeignKey(nameof(Utilizable))]
		public int IdUtilizable { get; set; }

		public virtual ModeloUtilizable Utilizable { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloFuncion"/> y un <see cref="ModeloTiradaBase"/>
	/// </summary>
	public class TITiradaFuncion : TITirada
	{
		[ForeignKey(nameof(Funcion))]
		public int IdFuncion { get; set; }

		public virtual ModeloFuncion Funcion { get; set; }
	}
}