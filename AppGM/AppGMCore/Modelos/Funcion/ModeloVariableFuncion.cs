namespace AppGM.Core
{
	public abstract class ModeloVariableFuncionBase
	{

	}

	public abstract class ModeloVariableFuncion<TipoVariable> : ModeloVariableFuncionBase
	{
		public TipoVariable ValorVariable { get; set; }
	}

	public class ModeloVariableFuncion_Int : ModeloVariableFuncion<int>{}
	public class ModeloVariableFuncion_Float : ModeloVariableFuncion<float> { }
	public class ModeloVariableFuncion_String : ModeloVariableFuncion<string> { }
}
