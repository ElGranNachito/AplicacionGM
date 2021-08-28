using System;
using CoolLogs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador base para <see cref="ModeloVariableFuncion{TipoVariable}"/>
	/// </summary>
	public abstract class ControladorVariableFuncionBase : Controlador<ModeloVariableFuncionBase>
	{
		/// <summary>
		/// Tipo de la variable.
		/// </summary>
		//Es un campo aparte porque el tipo de la variable guardada en el modelo es un string
		private Type mTipoVariable;

		public string NombreVariable
		{
			get => modelo.NombreVariable;
			set
			{
				if (value == NombreVariable)
					return;

				modelo.NombreVariable = value;
			}
		}

		public Type TipoVariable
		{
			get => mTipoVariable;
			set
			{
				if (value == mTipoVariable)
					return;

				//Actualizamos los datos del modelo
				modelo.TipoVariable = value.AssemblyQualifiedName;
				mTipoVariable = value;
			}
		}

		public int IDVariable
		{
			get => modelo.IDVariable;
			set => modelo.IDVariable = value;
		}

		public ControladorVariableFuncionBase(ModeloVariableFuncionBase _modelo)
			: base(_modelo)
		{
			//No queremos disparar la propiedad asi que establecemos el valor del campo directamente
			mTipoVariable = Type.GetType(modelo.TipoVariable);
		}

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

		/// <summary>
		/// Crea el <see cref="ControladorVariableFuncionBase"/> de tipo correspondiente para <paramref name="var"/>
		/// </summary>
		/// <param name="var"><see cref="ModeloVariableFuncionBase"/> para el que se creara el controlador</param>
		/// <returns>Controlador para <paramref name="var"/></returns>
		public static ControladorVariableFuncionBase CrearControladorCorrespondiente(ModeloVariableFuncionBase var)
		{
			switch (var)
			{
				case ModeloVariableFuncion_Int i:
					return new ControladorVariableFuncion_Int(i);
				case ModeloVariableFuncion_Float f:
					return new ControladorVariableFuncion_Float(f);
				case ModeloVariableFuncion_String s:
					return new ControladorVariableFuncion_String(s);
				default:
					SistemaPrincipal.LoggerGlobal.Log($"Tipo de {nameof(var)}({var.GetType()}) no soportado!", ESeveridad.Error);
					return null;
			}
		}

		/// <summary>
		/// Crea el <see cref="ModeloVariableFuncionBase"/> correspondiente para un <paramref name="tipo"/>
		/// </summary>
		/// <param name="tipo"><see cref="Type"/> de la variable</param>
		/// <param name="id">ID de la variable</param>
		/// <param name="nombre">Nombre de la variable</param>
		/// <returns>Modelo que representa la variable persistente creada</returns>
		public static ModeloVariableFuncionBase CrearModeloCorrespondiente(Type tipo, int id, string nombre)
		{
			ModeloVariableFuncionBase resultado = null;

			if (tipo == typeof(int))
				resultado = new ModeloVariableFuncion_Int();
			else if (tipo == typeof(float))
				resultado = new ModeloVariableFuncion_Float();
			else if (tipo == typeof(string))
				resultado = new ModeloVariableFuncion_String();

			resultado.TipoVariable   = tipo.AssemblyQualifiedName;
			resultado.IDVariable     = id;
			resultado.NombreVariable = nombre;

			return resultado;
		}
	}

	/// <summary>
	/// Controlador para un <see cref="ModeloVariableFuncion{TipoVariable}"/>
	/// </summary>
	/// <typeparam name="TipoVariable">Tipo de la variable representada por el modelo</typeparam>
	public class ControladorVariableFuncion<TipoVariable> : ControladorVariableFuncionBase
	{
		public ControladorVariableFuncion(ModeloVariableFuncionBase _modelo)
			: base(_modelo) { }

		public override object ObtenerValorVariable()
		{
			if (modelo is ModeloVariableFuncion<TipoVariable> m)
				return m.ValorVariable;

			SistemaPrincipal.LoggerGlobal.Log(
				$"No se pudo obtener el valor de la variable {this}. Error al intentar castearla a ModeloVariableFuncion de tipo {typeof(TipoVariable)}",
				ESeveridad.Error);

			return null;
		}

		public override void GuardarValorVariable(object nuevoValor)
		{
			if (nuevoValor is TipoVariable n)
			{
				((ModeloVariableFuncion<TipoVariable>) modelo).ValorVariable = n;

				return;
			}

			SistemaPrincipal.LoggerGlobal.Log($@"Se intento guardar valor de tipo {nuevoValor.GetType()} pero el tipo de esta variable es {typeof(TipoVariable)}.
				{Environment.NewLine}{this}");
		}
	}

	public class ControladorVariableFuncion_Int : ControladorVariableFuncion<int>
	{
		public ControladorVariableFuncion_Int(ModeloVariableFuncionBase _modelo)
			: base(_modelo) { }
	}

	public class ControladorVariableFuncion_Float : ControladorVariableFuncion<float>
	{
		public ControladorVariableFuncion_Float(ModeloVariableFuncionBase _modelo)
			: base(_modelo) { }
	}

	public class ControladorVariableFuncion_String : ControladorVariableFuncion<string>
	{
		public ControladorVariableFuncion_String(ModeloVariableFuncionBase _modelo)
			: base(_modelo) { }
	}
}
