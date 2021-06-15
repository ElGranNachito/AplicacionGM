namespace AppGM.Core
{
	/// <summary>
	/// VM base para representar bloques durante la creacion de funciones
	/// </summary>
	public abstract class ViewModelBloqueFuncion<TipoBloque> : ViewModelBloqueFuncionBase
		where TipoBloque: BloqueBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> del que es parte este bloque</param>
		public ViewModelBloqueFuncion(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion){}

		/// <summary>
		/// Indica si este bloque esta bien construido
		/// </summary>
		public bool EsValido { get; set; } = true;

		/// <summary>
		/// Genera el <see cref="TipoBloque"/> que equivale a los datos ingresados en este VM.
		/// Esta version es la que implementaran los diferentes <see cref="ViewModelBloqueFuncion{TipoBloque}"/>.
		/// </summary>
		/// <returns><see cref="TipoBloque"/></returns>
		public abstract TipoBloque GenerarBloque_Impl();

		//Sobreescribimos el metodo GenerarBloque de la clase base para que llame a GenerarBloque_Impl
		public override BloqueBase GenerarBloque() => GenerarBloque_Impl();
	}
}
