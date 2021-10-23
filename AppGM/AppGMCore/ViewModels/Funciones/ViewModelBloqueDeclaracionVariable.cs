using System;
using System.Collections.Generic;
using System.ComponentModel;
using AppGM.Core.Delegados;

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
		private ETipoVariable mTipoDeVariable = ETipoVariable.Normal;


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
		/// <param name="_padre"><see cref="IContenedorDeBloques"/> que contiene a este bloque. Si se deja en null se asignara por defecto
		/// el <see cref="ViewModelCreacionDeFuncionBase"/> actualmente activo</param>
		/// <param name="_idBloque">id que sera asignada a este bloque. Si se deja en -1, la id se asignara automaticamente</param>
		public ViewModelBloqueDeclaracionVariable(IContenedorDeBloques _padre = null, int _idBloque = -1)
			: base(_padre, _idBloque)
		{
			ValorPorDefecto = new ViewModelArgumento( this, typeof(object), "Valor por defecto");
			Tipo = typeof(object);
		}

		/// <summary>
		/// Constructor utilizado para inicializar esta instancia a partir de datos existentes en un <see cref="BloqueVariable"/>
		/// </summary>
		/// <param name="_idBloque">id que se le asignara al bloque</param>
		/// <param name="_tipo"><see cref="Type"/> de la variable</param>
		/// <param name="_tipoVariable"><see cref="ETipoVariable"/> de la variable</param>
		/// <param name="_nombre">Nombre de la variable</param>
		/// <param name="_parametrosVMValorPorDefecto">Parametros que se utilizaran para incializar el <see cref="ValorPorDefecto"/></param>
		/// <param name="_padre"><see cref="IContenedorDeBloques"/> que contiene a este bloque. Si se deja en null se asignara por defecto
		/// el <see cref="ViewModelCreacionDeFuncionBase"/> actualmente activo</param>
		public ViewModelBloqueDeclaracionVariable(
			int _idBloque,
			Type _tipo,
			ETipoVariable _tipoVariable,
			string _nombre, 
			ParametrosInicializarArgumentoDesdeBloque _parametrosVMValorPorDefecto,
			IContenedorDeBloques _padre = null)

			:base(_padre, _idBloque)
		{
			_parametrosVMValorPorDefecto.contenedor = this;

			ValorPorDefecto = new ViewModelArgumento(_parametrosVMValorPorDefecto);

			Tipo = _tipo;

			EsPersistente = _tipoVariable == ETipoVariable.Persistente;
			EsParametro = _tipoVariable == ETipoVariable.ParametroCreadoPorElUsuario;

			Nombre = _nombre;
		}

		#endregion

		#region Metodos

		public override BloqueVariable GenerarBloque_Impl()
		{
			if (mResultado == null)
			{
				mResultado ??= new BloqueVariable(
					IDBloque,
					Nombre, 
					Tipo,
					mTipoDeVariable,
					ValorPorDefecto.GenerarBloque_Impl());
			}
			else
			{
				mResultado.Actualizar(
					IDBloque,
					Nombre, 
					Tipo,
					mTipoDeVariable,
					ValorPorDefecto.GenerarBloque_Impl());
			}

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

			ValorPorDefecto.OnEsValidoCambio += esValido => ActualizarValidez();
		}

		public override bool VerificarValidez()
		{
			ActualizarValidez();

			return EsValido;
		}

		public void ActualizarValidez()
		{
			ValorPorDefecto.ActualizarValidez();

			EsValido = ValorPorDefecto.EsValido && Nombre.Length != 0;
		}

		#endregion
	}
}