using System;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador base para <see cref="ModeloVariable{TipoVariable}"/>
	/// </summary>
	public abstract class ControladorVariableBase : Controlador<ModeloVariableBase>
	{
		#region Campos & Propiedades

		//---------------------------------------CAMPOS-------------------------------------------

		/// <summary>
		/// Tipo de la variable.
		/// </summary>
		//Es un campo aparte porque el tipo de la variable guardada en el modelo es un string
		private Type mTipoVariable;

		//------------------------------------PROPIEDADES-------------------------------------

		/// <summary>
		/// Atajo para obtener/establecer el valor del nombre de la variable en el modelo
		/// </summary>
		public string NombreVariable
		{
			get => modelo.NombreVariable;
			set => modelo.NombreVariable = value;
		}

		/// <summary>
		/// Atajo para obtener/establecer el valor de la descripcion de la variable en el modelo
		/// </summary>
		public string DescripcionVariable
		{
			get => modelo.DescripcionVariable;
			set => modelo.DescripcionVariable = value;
		}

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		public Type TipoVariable
		{
			get => mTipoVariable;
			set
			{
				if (value == mTipoVariable)
					return;

				//Actualizamos los datos del modelo
				modelo.TipoVariableString = value.AssemblyQualifiedName;
				mTipoVariable = value;
			}
		}

		/// <summary>
		/// Atajo para obtener/establecer el valor del id de la variable en el modelo
		/// </summary>
		public int IDVariable
		{
			get => modelo.IDVariable != 0 ? modelo.IDVariable : modelo.Id;
			set => modelo.IDVariable = value;
		}

		#endregion

		#region Constructor

		public ControladorVariableBase(ModeloVariableBase _modelo)
			: base(_modelo)
		{
			//No queremos disparar la propiedad asi que establecemos el valor del campo directamente
			mTipoVariable = Type.GetType(modelo.TipoVariableString);
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Obtiene el valor de la variable almacenado
		/// </summary>
		/// <returns>Valor de la varaible almacenado en el modelo</returns>
		public abstract object ObtenerValorVariable();

		/// <summary>
		/// Guarda el valor de la variable en el modelo
		/// </summary>
		/// <param name="nuevoValor">Valor que asignarle a la variable</param>
		public abstract void GuardarValorVariable(object nuevoValor);

		public override string ToString() =>
			$"ID: {modelo.Id} - Nombre: {modelo.NombreVariable} - IDVariable: {modelo.IDVariable}";

		public override ViewModelItemListaGenerico<ViewModelVariableItem> CrearViewModelItem() =>
			new ViewModelVariableItem(this);

		#endregion

		#region Metodos Estaticos

		/// <summary>
		/// Crea el <see cref="ControladorVariableBase"/> de tipo correspondiente para <paramref name="var"/>
		/// </summary>
		/// <param name="var"><see cref="ModeloVariableBase"/> para el que se creara el controlador</param>
		/// <returns>Controlador para <paramref name="var"/></returns>
		public static ControladorVariableBase CrearControladorCorrespondiente(ModeloVariableBase var)
		{
			switch (var)
			{
				case ModeloVariableInt i:
					return new ControladorVariableInt(i);
				case ModeloVariableFloat f:
					return new ControladorVariableFloat(f);
				case ModeloVariableString s:
					return new ControladorVariableString(s);
				case ModeloVariableControlador c:
					return new ControladorVariableControlador(var);
				default:
					SistemaPrincipal.LoggerGlobal.Log($"Tipo de {nameof(var)}({var.GetType()}) no soportado!", ESeveridad.Error);
					return null;
			}
		}

		/// <summary>
		/// Crea el <see cref="ModeloVariableBase"/> correspondiente para un <paramref name="tipo"/>
		/// </summary>
		/// <param name="tipo"><see cref="Type"/> de la variable</param>
		/// <param name="id">ID de la variable</param>
		/// <param name="nombre">Nombre de la variable</param>
		/// <returns>Modelo que representa la variable persistente creada</returns>
		public static ModeloVariableBase CrearModeloCorrespondiente(Type tipo, int id, string nombre)
		{
			ModeloVariableBase resultado = null;

			if (tipo == typeof(int))
				resultado = new ModeloVariableInt();
			else if (tipo == typeof(float))
				resultado = new ModeloVariableFloat();
			else if (tipo == typeof(string))
				resultado = new ModeloVariableString();
			else if (tipo.IsSubclassOf(typeof(ControladorBase)))
			{
				var tipoModelo = tipo.GetField("modelo").FieldType;

				resultado = new ModeloVariableControlador
				{
					TipoModeloControlador = tipoModelo.AssemblyQualifiedName
				};
			}

			resultado.TipoVariableString = tipo.AssemblyQualifiedName;
			resultado.IDVariable = id;
			resultado.NombreVariable = nombre;

			return resultado;
		} 

		#endregion
	}

	/// <summary>
	/// Controlador para un <see cref="ModeloVariable{TipoVariable}"/>
	/// </summary>
	/// <typeparam name="TVariable">Tipo de la variable representada por el modelo</typeparam>
	public class ControladorVariable<TVariable> : ControladorVariableBase
	{
		#region Constructor

		public ControladorVariable(ModeloVariableBase _modelo)
			: base(_modelo) { } 

		#endregion

		#region Metodos

		public override object ObtenerValorVariable()
		{
			if (modelo is ModeloVariable<TVariable> m)
				return m.ValorVariable;

			SistemaPrincipal.LoggerGlobal.Log(
				$"No se pudo obtener el valor de la variable {this}. Error al intentar castearla a ModeloVariable de tipo {typeof(TVariable)}",
				ESeveridad.Error);

			return null;
		}

		public override void GuardarValorVariable(object nuevoValor)
		{
			if (nuevoValor is TVariable n)
			{
				((ModeloVariable<TVariable>)modelo).ValorVariable = n;

				return;
			}

			SistemaPrincipal.LoggerGlobal.Log($@"Se intento guardar valor de tipo {nuevoValor.GetType()} pero el tipo de esta variable es {typeof(TVariable)}.
				{Environment.NewLine}{this}");
		}

		#endregion
	}

	#region Implementaciones especificas

	/// <summary>
	/// Controlador de un <see cref="ModeloVariable{TipoVariable}"/> que guarda un <see cref="int"/>
	/// </summary>
	public class ControladorVariableInt : ControladorVariable<int>
	{
		public ControladorVariableInt(ModeloVariableBase _modelo)
			: base(_modelo) { }
	}

	/// <summary>
	/// Controlador de un <see cref="ModeloVariable{TipoVariable}"/> que guarda un <see cref="float"/>
	/// </summary>
	public class ControladorVariableFloat : ControladorVariable<float>
	{
		public ControladorVariableFloat(ModeloVariableBase _modelo)
			: base(_modelo) { }
	}

	/// <summary>
	/// Controlador de un <see cref="ModeloVariable{TipoVariable}"/> que guarda un <see cref="string"/>
	/// </summary>
	public class ControladorVariableString : ControladorVariable<string>
	{
		public ControladorVariableString(ModeloVariableBase _modelo)
			: base(_modelo) { }
	}

	/// <summary>
	/// Controlador de un <see cref="ModeloVariable{TipoVariable}"/> que guarda un <see cref="Controlador{TipoModelo}"/>
	/// </summary>
	public sealed class ControladorVariableControlador : ControladorVariable<int>
	{
		#region Campos & Propiedades

		/// <summary>
		/// Contiene el valor de <see cref="ControladorGuardado"/>
		/// </summary>
		private ControladorBase mControlador;

		/// <summary>
		/// Referencia al modelo con el tipo correcto, para no tener que castearlo cada vez que queramos acceder a sus valores
		/// </summary>
		private ModeloVariableControlador mModeloVariableControlador;

		/// <summary>
		/// Obtiene o establece el controlador guardado por esta variable
		/// </summary>
		public ControladorBase ControladorGuardado
		{
			get
			{
				if (mControlador == null)
					ObtenerValorVariable();

				return mControlador;
			}
			private set
			{
				GuardarValorVariable(value);
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelo"></param>
		public ControladorVariableControlador(ModeloVariableBase _modelo)
			: base(_modelo)
		{
			if (modelo is ModeloVariableControlador m)
			{
				//Verificamos que el tipo del modelo guardado sea valido 

				try
				{
					Type.GetType(m.TipoModeloControlador);
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log($"Nombre de tipo guardado ({m.TipoModeloControlador}) no es valido!", ESeveridad.Error);

					return;
				}

				mModeloVariableControlador = m;
			}
			else
			{
				SistemaPrincipal.LoggerGlobal.Log($"{modelo} no es de tipo {nameof(ModeloVariableControlador)}!", ESeveridad.Error);
			}
		}

		#endregion

		#region Metodos

		public override object ObtenerValorVariable()
		{
			//Si ya tenemos el controlador cacheado entonces sencillamente lo devolvemos
			if (mControlador != null)
				return mControlador;

			//Intentamos obtenerlo desde el sistema principal
			mControlador = SistemaPrincipal.ObtenerControlador(SistemaPrincipal.ObtenerModelo(Type.GetType(mModeloVariableControlador.TipoModeloControlador), mModeloVariableControlador.ValorVariable));

			return mControlador;
		}

		public override void GuardarValorVariable(object nuevoValor)
		{
			//Si el valor pasado es un controlador actualizamos los valores guardados en el modelo
			if (nuevoValor is ControladorBase c)
			{
				modelo.TipoVariableString = nuevoValor.GetType().AssemblyQualifiedName;

				mModeloVariableControlador.ValorVariable = c.Modelo.Id;
				mModeloVariableControlador.TipoModeloControlador = c.Modelo.GetType().AssemblyQualifiedName;

				mControlador = c;

				return;
			}

			SistemaPrincipal.LoggerGlobal.Log($"{nameof(nuevoValor)} no es un {nameof(ControladorBase)}!", ESeveridad.Error);
		}

		#endregion
	} 

	#endregion
}