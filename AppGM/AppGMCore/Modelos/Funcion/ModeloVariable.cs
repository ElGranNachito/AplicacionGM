using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa una variable persistente en una funcion
	/// </summary>
	public abstract class ModeloVariableBase : ModeloBase
	{
		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string NombreVariable { get; set; }

		/// <summary>
		/// Descripcion de la variable
		/// </summary>
		[MaxLength(500)]
		public string DescripcionVariable { get; set; }

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		public string TipoVariable { get; set; }

		/// <summary>
		/// Id de la variable
		/// </summary>
		public int IDVariable { get; set; }

		/// <summary>
		/// Modelo del personaje que contiene esta variables
		/// </summary>
		public virtual TIVariablePersonaje PersonajeContenedor { get; set; }

		/// <summary>
		/// Modelo de la habilidad que contiene esta variables
		/// </summary>
		public virtual TIVariableHabilidad HabilidadContenedora { get; set; }

		/// <summary>
		/// Modelo del utilizable que contiene esta variables
		/// </summary>
		public virtual TIVariableUtilizable UtilizableContenedor { get; set; }

		/// <summary>
		/// Modelo de la funcion que contiene esta variables
		/// </summary>
		public virtual TIVariableFuncion FuncionContenedora { get; set; }
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

	/// <summary>
	/// Variable persistente de tipo <see cref="string"/>
	/// </summary>
	public class ModeloVariableControlador : ModeloVariable<int>
	{
		public string TipoModeloControlador { get; set; }
	}

	public class ModeloVariableLista : ModeloVariable<string>
	{

	}
}