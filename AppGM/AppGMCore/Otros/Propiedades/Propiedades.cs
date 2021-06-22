using System;

namespace AppGM.Core
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Enum)]
	class AccesibleEnGuraScratch : Attribute
	{
		public string nombreQueMostrar;

		/// <summary>
		/// Constructor default
		/// </summary>
		public AccesibleEnGuraScratch(){}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="nombreQueMostrar">Nombre que se mostrara para este elemento en gura scratch</param>
		public AccesibleEnGuraScratch(string _nombreQueMostrar)
		{
			nombreQueMostrar = _nombreQueMostrar;
		}
	}
}
