using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para el ingreso de una variable de tipo conocido
	/// </summary>
	public class ViewModelIngresoVariable : ViewModel
	{
		#region Campos & Propiedades
		/// <summary>
		/// Contiene el valor de <see cref="TipoVariable"/>
		/// </summary>
		private Type mTipoVariable;

		/// <summary>
		/// Contiene el valor de <see cref="TextoActual"/>
		/// </summary>
		private string mTextoActual;

		/// <summary>
		/// Contiene el valor de <see cref="EsLista"/>
		/// </summary>
		private bool mEsLista;

		/// <summary>
		/// Indica si se debe seleccionar un controlador para el valor de la variable
		/// </summary>
		public bool DebeSeleccionarControlador => (mTipoVariable == typeof(ControladorPersonaje) || mTipoVariable == typeof(ControladorUtilizable)) && !EsLista;

		/// <summary>
		/// Indica si esta variable es una lista
		/// </summary>
		public bool EsLista
		{
			get => mEsLista;
			set
			{
				if (value == mEsLista)
					return;

				mEsLista = value;

				DispararPropertyChanged(nameof(DebeSeleccionarControlador));
			}
		}

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		public Type TipoVariable
		{
			get => mTipoVariable;
			set
			{
				EliminarCaracteresNoValidos();
				DispararPropertyChanged(nameof(DebeSeleccionarControlador));
			}
		}

		/// <summary>
		/// Texto actual ingresado por el usuario
		/// </summary>
		public string TextoActual
		{
			get => mTextoActual;
			set
			{
				mTextoActual = value;

				EliminarCaracteresNoValidos();
			}
		} 

		#endregion

		private void EliminarCaracteresNoValidos()
		{
			if (TipoVariable == typeof(int))
			{
				mTextoActual = Regex.Replace(TextoActual, "[^0-9]", "");
			}
			else if (TipoVariable == typeof(float))
			{
				//No soy tan bueno con las expresiones regulares como para armarme esto en una sola expresion, sepan disculpar
				mTextoActual = Regex.Replace(mTextoActual, "[^0-9.]", "");

				if (mTextoActual.Count(c => c == '.') > 1)
				{
					int indicePrimerPunto = mTextoActual.IndexOf('.');

					mTextoActual = mTextoActual.Remove(mTextoActual.IndexOf('.', indicePrimerPunto + 1));
				}
			}

			DispararPropertyChanged(nameof(TextoActual));
		}
	}
}