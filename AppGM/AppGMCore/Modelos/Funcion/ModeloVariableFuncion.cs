namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa una variable persistente en una funcion
	/// </summary>
	public class ModeloVariableFuncionBase : ModeloBase
	{
		public string NombreVariable { get; set; }

		public string TipoVariable { get; set; }

		public int IDVariable { get; set; }
	}

	/// <summary>
	/// Modelo para una variable persistente de tipo conocido
	/// </summary>
	/// <typeparam name="TipoVariable"></typeparam>
	public abstract class ModeloVariableFuncion<TipoVariable> : ModeloVariableFuncionBase
	{
		public TipoVariable ValorVariable { get; set; }
	}

	/// <summary>
	/// Variable persistente de tipo <see cref="int"/>
	/// </summary>
	public class ModeloVariableFuncion_Int : ModeloVariableFuncion<int>{}

	/// <summary>
	/// Variable persistente de tipo <see cref="float"/>
	/// </summary>
	public class ModeloVariableFuncion_Float : ModeloVariableFuncion<float> { }

	/// <summary>
	/// Variable persistente de tipo <see cref="string"/>
	/// </summary>
	public class ModeloVariableFuncion_String : ModeloVariableFuncion<string> { }
}