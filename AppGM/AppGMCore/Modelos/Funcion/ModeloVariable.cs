namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa una variable persistente en una funcion
	/// </summary>
	public class ModeloVariableBase : ModeloBase
	{
		public string NombreVariable { get; set; }

		public string TipoVariable { get; set; }

		public int IDVariable { get; set; }
	}

	/// <summary>
	/// Modelo para una variable persistente de tipo conocido
	/// </summary>
	/// <typeparam name="TipoVariable"></typeparam>
	public abstract class ModeloVariable<TipoVariable> : ModeloVariableBase
	{
		public TipoVariable ValorVariable { get; set; }
	}

	/// <summary>
	/// Variable persistente de tipo <see cref="int"/>
	/// </summary>
	public class ModeloVariableInt : ModeloVariable<int>{}

	/// <summary>
	/// Variable persistente de tipo <see cref="float"/>
	/// </summary>
	public class ModeloVariableFloat : ModeloVariable<float> { }

	/// <summary>
	/// Variable persistente de tipo <see cref="string"/>
	/// </summary>
	public class ModeloVariableString : ModeloVariable<string> { }
}