namespace AppGM.Core
{
	/// <summary>
	/// Provee la logica para <see cref="ModeloFuenteDeDaño"/>
	/// </summary>
	public partial class ModeloFuenteDeDaño
	{
		public override string ToString()
		{
			//Mostramos el nombre de la fuente y los tipos de daño que abarca
			return $"{NombreFuente} ({TiposDeDaño.FlagsActivasEnumToString()})";
		}
	}
}
