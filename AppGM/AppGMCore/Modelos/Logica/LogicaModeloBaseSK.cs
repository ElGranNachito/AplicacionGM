using System;
using System.Threading.Tasks;

namespace AppGM.Core
{
    /// <summary>
    /// Clase que contiene la logica del <see cref="ModeloBaseSK"/>
    /// </summary>
	public abstract partial class ModeloBaseSK
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando este modelo es guardado
		/// </summary>
		public event Action<ModeloBase> OnModeloGuardado = delegate { };

		/// <summary>
		/// Evento que se dispara cuando este modelo es eliminado
		/// </summary>
		public event Action<ModeloBase> OnModeloEliminado = delegate { };

		#endregion

		#region Metodos

		/// <summary>
		/// Guarda el modelo a la base de datos
		/// </summary>
		public virtual void Guardar()
		{
			SistemaPrincipal.GuardarModelo(this);

			OnModeloGuardado((ModeloBase)this);
		}

		/// <summary>
		/// Guarda el modelo a la base de datos
		/// </summary>
		public virtual async Task GuardarAsync()
		{
			await SistemaPrincipal.GuardarModeloAsync(this);

			OnModeloGuardado((ModeloBase)this);
		}

		/// <summary>
		/// Elimina el modelo de la base de datos
		/// </summary>
		public virtual void Eliminar()
		{
			if (this is ModeloBase mb)
			{
				if (mb.Id != 0)
				{
					SistemaPrincipal.EliminarModelo(this);					
				}
			}
			else
			{
				SistemaPrincipal.EliminarModelo(this);
			}

			OnModeloEliminado((ModeloBase)this);
		}

        /// <summary>
        /// Crea una copia superficial de este modelo
        /// </summary>
        /// <returns></returns>
        public ModeloBase Clonar() => (ModeloBase)MemberwiseClone();

		#endregion
	}
}