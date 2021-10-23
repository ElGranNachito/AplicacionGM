using System.ComponentModel.DataAnnotations;
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

	public abstract class TIFuncionContenedor : TIFuncion
	{
		/// <summary>
		/// Nombre del evento que maneja la funcion
		/// </summary>
		[StringLength(100)]
		public string NombreEvento { get; set; }
	}

	/// <summary>
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloEfecto"/>
	/// </summary>
	public class TIFuncionEfecto : TIFuncionContenedor
	{
		[ForeignKey(nameof(Efecto))]
		public int IDEfecto { get; set; }

		public virtual ModeloEfecto Efecto { get; set; }

		/// <summary>
		/// Tipo de la funcion
		/// </summary>
		public ETipoFuncionEfecto TipoFuncion { get; set; }
	}

	/// <summary>
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloHabilidad"/>
	/// </summary>
	public class TIFuncionHabilidad : TIFuncionContenedor
	{
		[ForeignKey(nameof(Habilidad))]
		public int IDHabilidad { get; set; }

		public virtual ModeloHabilidad Habilidad { get; set; }
	}
}