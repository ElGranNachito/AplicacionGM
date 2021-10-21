using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Logica para el <see cref="ModeloTirada"/>
	/// </summary>
	public partial class ModeloTiradaBase
	{
		/// <summary>
		/// Obtiene
		/// </summary>
		/// <returns></returns>
		public ModeloConVariablesYTiradas ObtenerModeloContenedor()
		{
			if (PersonajeContenedor != null)
				return PersonajeContenedor;

			if (HabilidadContenedora != null)
				return HabilidadContenedora;

			if (UtilizableContenedor != null)
				return UtilizableContenedor;

			if (FuncionContenedora != null)
				return FuncionContenedora;

			return null;
		}
	}
}
