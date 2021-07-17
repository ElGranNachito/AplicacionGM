using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AppGM.Core
{
	/// <summary>
	/// Bloque que representa la declaracion de una variable
	/// </summary>
	public class ViewModelBloqueDeclaracionVariable : ViewModelBloqueFuncion<BloqueVariable>
	{
		#region Campos & Propiedades

		//---------------------------------------CAMPOS--------------------------------------


		/// <summary>
		/// <see cref="BloqueVariable"/> resultante
		/// </summary>
		private BloqueVariable mResultado = null;

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		private Type mTipo;


		//-------------------------------------PROPIEDADES------------------------------------


		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string Nombre { get; set; }

		/// <summary>
		/// Indica si mostrar el menu inferior del bloque
		/// </summary>
		public bool MostrarMenuInferior { get; set; }

		/// <summary>
		/// Indica si el valor de la variable debe guardarse
		/// </summary>
		public bool EsPersistente { get; set; }

		/// <summary>
		/// Indica si esta variable sera un parametro en la funcion final
		/// </summary>
		public bool EsParametro { get; set; }

		/// <summary>
		/// Indica si se debe mostrar le combo box para seleccionar el tipo de la variable
		/// </summary>
		public bool DebeSeleccionarTipoVariable => MostrarMenuInferior && !ValorPorDefecto.DeteccionAutomaticaDeTipo;

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		public Type Tipo
		{
			get => mTipo;
			set
			{
				mTipo = value;

				ValorPorDefecto.TipoArgumento = mTipo;
			}
		}

		/// <summary>
		/// Tipos que puede asumir esta variable
		/// </summary>
		public List<Type> TiposDisponibles { get; set; } = new List<Type>
		{
			typeof(EArquetipo), typeof(EBienestar), typeof(EClaseServant),
			typeof(int), typeof(string), typeof(float), typeof(double), typeof(object)
		};

		/// <summary>
		/// VM que contiene el valor por defecto de la variable
		/// </summary>
		public ViewModelArgumento ValorPorDefecto { get; set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionFuncionBase"><see cref="ViewModelCreacionDeFuncionBase"/> del que es parte esta variable</param>
		public ViewModelBloqueDeclaracionVariable(ViewModelCreacionDeFuncionBase _vmCreacionFuncionBase)
			: base(_vmCreacionFuncionBase)
		{
			ValorPorDefecto = new ViewModelArgumento(mVMCreacionDeFuncion, this, typeof(object), "Valor por defecto");
			Tipo = typeof(object);

			ValorPorDefecto.OnEsValidoCambio += esValido =>
			{
				if (!esValido)
					EsValido = false;
			};

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(MostrarMenuInferior)))
					DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarTipoVariable)));
			};

			ValorPorDefecto.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(ValorPorDefecto.DeteccionAutomaticaDeTipo)))
					DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarTipoVariable)));
			};
		}

		#endregion

		#region Metodos

		public override BloqueVariable GenerarBloque_Impl()
		{
			mResultado ??= new BloqueVariable(Nombre, Tipo, ObtenerTipoVariable(), ValorPorDefecto.GenerarBloque_Impl());

			mResultado.nombre = Nombre;
			mResultado.tipo   = Tipo;

			return mResultado;
		}

		private ETipoVariable ObtenerTipoVariable()
		{
			if (EsParametro)
				return ETipoVariable.Parametro;

			if (EsPersistente)
				return ETipoVariable.Persistente;

			return ETipoVariable.Normal;
		}

		#endregion
	}
}