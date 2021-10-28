using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Representa una funcion escrita en GuraScratch
	/// </summary>
	public partial class ModeloFuncion : ModeloConVariablesYTiradas
	{
		/// <summary>
		/// Padre de este modelo. Se trata de la funcion en la que se basa
		/// </summary>
		public virtual TIFuncionPadreFuncion Padre { get; set; }

		/// <summary>
		/// Funciones que dependen de este modelo
		/// </summary>
		public virtual List<TIFuncionPadreFuncion> Hijos { get; set; }

		/// <summary>
		/// Modelo del efecto que contiene esta funcion
		/// </summary>
		public virtual TIFuncionEfecto EfectoContenedor { get; set; }

		/// <summary>
		/// Modelo de la habilidad que contiene esta funcion
		/// </summary>
		public virtual TIFuncionHabilidad HabilidadContenedora { get; set; }

		/// <summary>
		/// Modelo del item que contiene esta funcion
		/// </summary>
		public virtual TIFuncionItem ItemContenedor { get; set; }

		/// <summary>
		/// Nombre de la funcions
		/// </summary>
		[StringLength(100)]
		public string NombreFuncion { get; set; }

		/// <summary>
		/// Devuelve la id de la funcion utilizada para identificar al archivo XML
		/// </summary>
		[NotMapped]
		public int IDFuncion
		{
			get
			{
				if (Padre != null)
					return Padre.IDPadre;

				return Id;
			}
		}
	}
}