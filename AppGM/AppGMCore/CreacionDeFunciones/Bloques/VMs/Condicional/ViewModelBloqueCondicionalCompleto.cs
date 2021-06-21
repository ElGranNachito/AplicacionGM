namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa una cadena de If y Elses
	/// </summary>
	public class ViewModelBloqueCondicionalCompleto : ViewModelBloqueCondicionalBase
	{
		private const string NombreIf = "If";
		private const string NombreElseIf = "Else If";
		private const string NombreElse = "Else";

		/// <summary>
		/// Nombre de la condicional (If, Else o Else if)
		/// </summary>
		public string NombreCondicional { get; set; } = NombreIf;

		public ViewModelBloqueCondicionalCompleto(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion)
		{
			
		}

		public override BloqueCondicional GenerarBloque_Impl()
		{
			return null;
		}
	}
}
