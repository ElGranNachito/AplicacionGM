using CoolLogs;

namespace AppGM.Core
{
	public abstract class ControladorVariableFuncionBase : Controlador<ModeloVariableFuncionBase>
	{
		public ControladorVariableFuncionBase(ModeloVariableFuncionBase _modelo)
			:base(_modelo){}

		public abstract object ObtenerValorVariable();

		public override string ToString() =>
			$"ID: {modelo.Id} - Nombre: {modelo.NombreVariable} - IDVariable: {modelo.IDVariable}";
	}

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
