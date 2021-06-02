using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	//En busca de un mejor nombre

	/// <summary>
	/// Modelo que representa un efecto siendo aplicado sobre un objetivo.
	/// </summary>
	public class ModeloEfectoSiendoAplicado : ModeloBase
	{
		/// <summary>
		/// Turnos que le restan al efecto
		/// </summary>
		public ushort TurnosRestantes { get; set; }

		/// <summary>
		/// Indica si esta siendo aplicado actualmente
		/// </summary>
		public bool EstaSiendoAplicado { get; set; }

		/// <summary>
		/// Modelo del efecto
		/// </summary>
		public TIEfectoSiendoAplicadoEfecto Efecto { get; set; }

		/// <summary>
		/// <see cref="ModeloPersonaje"/> que causo o instigo el efecto
		/// </summary>
		public TIEfectoSiendoAplicadoPersonaje Instigador { get; set; }

		/// <summary>
		/// <see cref="ModeloPersonaje"/> a los que se les esta aplicando el efecto
		/// </summary>
		public List<TIEfectoSiendoAplicadoPersonaje> Objetivos { get; set; }

		/// <summary>
		/// Atajo para obtener el <see cref="ModeloEfecto"/>
		/// </summary>
		[NotMapped]
		public ModeloEfecto ModeloEfecto => Efecto.Efecto;
	}
}