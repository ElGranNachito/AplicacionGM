using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una relacion con un <see cref="ModeloEspecialidad"/>
	/// </summary>
	public abstract class TIEspecialidad : ModeloBaseSK
	{
		[ForeignKey(nameof(Especialidad))]
		public int IDEspecialidad { get; set; }

		public virtual ModeloEspecialidad Especialidad { get; set; }
	}

	/// <summary>
	/// Representa una relacion entre un <see cref="ModeloPersonaje"/> y un <see cref="ModeloEspecialidad"/>
	/// </summary>
	public class TIEspecialidadPersonaje : TIEspecialidad
	{
		[ForeignKey(nameof(Personaje))]
		public int IDPersonaje { get; set; }

		public virtual ModeloPersonaje Personaje { get; set; }
	}
}