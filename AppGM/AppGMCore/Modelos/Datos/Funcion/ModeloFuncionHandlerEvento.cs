using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa una funcion destinada a lidiar con eventos de controladores
	/// </summary>
	public class ModeloFuncion_HandlerEvento : ModeloFuncion
	{
		/// <summary>
		/// Contiene el valor de <see cref="TipoEventoQueManejaString"/>
		/// </summary>
		[NotMapped]
		private string mTipoHandlerString;

		/// <summary>
		/// Tipo del evento que maneja esta funcion
		/// </summary>
		[NotMapped]
		public Type TipoHandler { get; private set; }

		/// <summary>
		/// Tipo del evento que maneja esta funcion, representado como un string
		/// </summary>
		[StringLength(255)]
		public string TipoHandlerString
		{
			get => mTipoHandlerString;
			set
			{
				if (value == mTipoHandlerString)
					return;

				mTipoHandlerString = value;

				if (mTipoHandlerString.IsNullOrWhiteSpace())
					return;

				try
				{
					TipoHandler = Type.GetType(value);
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.LogCrash($"No se pudo parsear {value} a un {nameof(Type)}{Environment.NewLine}{ex.Message}");
				}

				if (!typeof(MulticastDelegate).IsAssignableFrom(TipoHandler))
					SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(TipoHandler)}({TipoHandler}) debe ser un delegado");
			}
		}

		/// <summary>
		/// Eventos de <see cref="ControladorPersonaje"/> a los que esta subscrito
		/// </summary>
		public virtual List<TIFuncionHandlerEvento<ModeloPersonaje>> EventosEnPersonaje { get; set; } =
			new List<TIFuncionHandlerEvento<ModeloPersonaje>>();

		/// <summary>
		/// Eventos de <see cref="ControladorHabilidad"/> a los que esta subscrito
		/// </summary>
		public virtual List<TIFuncionHandlerEvento<ModeloHabilidad>> EventosEnHabilidad { get; set; } =
			new List<TIFuncionHandlerEvento<ModeloHabilidad>>();

		/// <summary>
		/// Eventos de <see cref="ControladorUtilizable"/> a los que esta subscrito
		/// </summary>
		public virtual List<TIFuncionHandlerEvento<ModeloUtilizable>> EventosEnUtilizable { get; set; } =
			new List<TIFuncionHandlerEvento<ModeloUtilizable>>();

		/// <summary>
		/// Eventos de <see cref="ControladorEfecto"/> a los que esta subscrito
		/// </summary>
		public virtual List<TIFuncionHandlerEvento<ModeloEfecto>> EventosEnEfecto { get; set; } =
			new List<TIFuncionHandlerEvento<ModeloEfecto>>();
	}
}
