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

		/// <summary>
		/// Almacena el valor de <see cref="Nombre"/>
		/// </summary>
		private string mNombre;

		/// <summary>
		/// Tipo de esta variable
		/// </summary>
		private ETipoVariable mTipoDeVariable;


		//-------------------------------------PROPIEDADES------------------------------------


		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string Nombre
		{
			get => mNombre;
			set
			{
				if (value == mNombre)
					return;

				mNombre = value;

				ActualizarValidez();
			}
		}

		/// <summary>
		/// Indica si esta variable sera un parametro en la funcion final
		/// </summary>
		public bool EsParametro
		{
			get => mTipoDeVariable == ETipoVariable.ParametroCreadoPorElUsuario;
			set
			{
				if (value && mTipoDeVariable != ETipoVariable.ParametroCreadoPorElUsuario)
					mTipoDeVariable = ETipoVariable.ParametroCreadoPorElUsuario;
				else if (!value && mTipoDeVariable == ETipoVariable.ParametroCreadoPorElUsuario)
					mTipoDeVariable = ETipoVariable.Normal;
				else
					return;
				
				//Le avisamos a la interfaz que se actualizo el valor de PuedeSerPersistente
				DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeSerPersistente)));
			}
		}

		/// <summary>
		/// Indica si el valor de la variable debe guardarse
		/// </summary>
		public bool EsPersistente
		{
			get => mTipoDeVariable == ETipoVariable.Persistente;
			set
			{
				if (value && mTipoDeVariable != ETipoVariable.Persistente)
					mTipoDeVariable = ETipoVariable.Persistente;
				else if (!value && mTipoDeVariable == ETipoVariable.Persistente)
					mTipoDeVariable = ETipoVariable.Normal;
			}
		}

		/// <summary>
		/// Indica si mostrar el menu inferior del bloque
		/// </summary>
		public bool MostrarMenuInferior { get; set; }

		/// <summary>
		/// Indica si se debe mostrar le combo box para seleccionar el tipo de la variable
		/// </summary>
		public bool DebeSeleccionarTipoVariable => MostrarMenuInferior && !ValorPorDefecto.DeteccionAutomaticaDeTipo;

		/// <summary>
		/// Indica si esta variable puede ser persistente
		/// </summary>
		public bool PuedeSerPersistente => mTipoDeVariable != ETipoVariable.ParametroCreadoPorElUsuario;

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
			ValorPorDefecto = new ViewModelArgumento( this, typeof(object), "Valor por defecto");
			Tipo = typeof(object);

			ValorPorDefecto.OnEsValidoCambio += esValido => ActualizarValidez();
		}

		public ViewModelBloqueDeclaracionVariable(
			int _idBloque,
			ViewModelCreacionDeFuncionBase _vmCreacionFuncionBase,
			Type _tipo,
			ETipoVariable _tipoVariable,
			string _nombre, 
			ParametrosInicializarArgumentoDesdeBloque _parametrosVMArgumento)

			:base(_vmCreacionFuncionBase, _idBloque)
		{
			Tipo = _tipo;

			EsPersistente = _tipoVariable == ETipoVariable.Persistente;
			EsParametro   = _tipoVariable == ETipoVariable.ParametroCreadoPorElUsuario;

			Nombre = _nombre;

			_parametrosVMArgumento.contenedor = this;

			ValorPorDefecto = new ViewModelArgumento(_parametrosVMArgumento);
		}

		#endregion

		#region Metodos

		public override BloqueVariable GenerarBloque_Impl()
		{
			mResultado ??= new BloqueVariable(IDBloque, Nombre, Tipo, mTipoDeVariable, ValorPorDefecto.GenerarBloque_Impl());

			return mResultado;
		}

		public override void Inicializar()
		{
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

		public override bool VerificarValidez()
		{
			ActualizarValidez();

			return EsValido;
		}

		private void ActualizarValidez() => EsValido = ValorPorDefecto.EsValido && Nombre.Length != 0;

		#endregion
	}
}