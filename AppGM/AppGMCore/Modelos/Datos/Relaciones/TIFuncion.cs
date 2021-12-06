using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase base que representa una relacion con un <see cref="ModeloFuncion"/>
	/// </summary>
	public class TIFuncion : ModeloBase
	{
		[ForeignKey(nameof(Funcion))]
		public int IDFuncion { get; set; }

		public virtual ModeloFuncion Funcion { get; set; }

		/// <summary>
		/// Proposito de la funcion contenida en este relacion
		/// </summary>
		public EPropositoFuncionRelacion PropositoFuncionRelacion { get; set; }

		[NotMapped]
		public override int Id { get; set; }

		[NotMapped]
		public override bool EsValido
		{
			get => Funcion.EsValido;
			set => SistemaPrincipal.LoggerGlobal.Log($"No se puede establecer la validez de un {this.GetType()}", ESeveridad.Error);
		}
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
	/// Representa la relacion de un <see cref="ModeloFuncion"/> con un <see cref="ModeloEfecto"/>
	/// </summary>
	public class TIFuncionEfecto : TIFuncion
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
	public class TIFuncionItem : TIFuncion
	{
		[ForeignKey(nameof(Item))]
		public int IDItem { get; set; }

		public virtual ModeloItem Item { get; set; }
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

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloBase"/> y un <see cref="ModeloFuncion_HandlerEvento"/>
	/// </summary>
	/// <typeparam name="TOtro">Tipo del modelo cuyo controlador contiene los eventos a los que la funcion esta subscrita</typeparam>
	public partial class TIFuncionHandlerEvento<TOtro> : ModeloBase

		where TOtro: ModeloBase
	{
		/// <summary>
		/// Clave foranea a la <see cref="Funcion"/>
		/// </summary>
		[ForeignKey(nameof(Funcion))]
		[NoCopiar]
		public int IdFuncion { get; set; }

		/// <summary>
		/// Funcion
		/// </summary>
		public virtual ModeloFuncion_HandlerEvento Funcion { get; set; }

		/// <summary>
		/// Clave foranea al otro modelo que forma parte de la relacion
		/// </summary>
		[ForeignKey(nameof(Otro))]
		[NoCopiar]
		public int IdOtro { get; set; }

		/// <summary>
		/// El otro modelo que forma parte de esta relacion
		/// </summary>
		public virtual TOtro Otro { get; set; }

		/// <summary>
		/// Nombre de los eventos a los que se debe subscribir el handler, separados por punto y coma ';'
		/// </summary>
		[StringLength(1000)]
		public string NombresEventosVinculados { get; set; }

		[NotMapped]
		public override int Id
		{
			get => mId;
			set => mId = value;
		}

		[NotMapped]
		public override bool EsValido
		{
			get => Funcion.EsValido;
			set
			{
				SistemaPrincipal.LoggerGlobal.Log($"No se puede establecer la validez de un {this.GetType()}", ESeveridad.Error);
			}
		}
	}
}