namespace AppGM.Core
{
	public class ModeloVariableFuncionBase : ModeloBase
	{
		public string NombreVariable { get; set; }

		public string TipoVariable { get; set; }

		public int IDVariable { get; set; }
	}

	public abstract class ModeloVariableFuncion<TipoVariable> : ModeloVariableFuncionBase
	{
		public TipoVariable ValorVariable { get; set; }
	}

	public class ModeloVariableFuncion_Int : ModeloVariableFuncion<int>{}
	public class ModeloVariableFuncion_Float : ModeloVariableFuncion<float> { }
	public class ModeloVariableFuncion_String : ModeloVariableFuncion<string> { }
}
