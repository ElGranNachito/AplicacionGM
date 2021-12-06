using System;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un elemento del codigo al que se puede acceder desde GuraScratch.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)]
	public class AccesibleEnGuraScratch : Attribute
	{
		/// <summary>
		/// Nombre que se mostrara para este elemento en gura scratch
		/// </summary>
		public string nombreQueMostrar;

		/// <summary>
		/// Constructor default
		/// </summary>
		public AccesibleEnGuraScratch(){}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="nombreQueMostrar">Valor que se asignara a <see cref="nombreQueMostrar"/></param>
		public AccesibleEnGuraScratch(string _nombreQueMostrar)
		{
			nombreQueMostrar = _nombreQueMostrar;
		}
	}

	/// <summary>
	/// Sirve para modificar el nombre con el que aparece el parametro de una funcion en GuraScratch
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	public class NombreParametroGuraScratch : Attribute
	{
		/// <summary>
		/// Nombre con el que mostrar al parametro
		/// </summary>
		public string nombre;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_nombre">Valor que se asignara a <see cref="nombre"/></param>
		public NombreParametroGuraScratch(string _nombre)
		{
			nombre = _nombre;
		}
	}
}