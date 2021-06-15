namespace AppGM.Core
{
	public abstract class ViewModelBloqueFuncionBase : ViewModel, IReceptorDeDrag
	{
		/// <summary>
		/// VM del control de creacion de funciones
		/// </summary>
		protected ViewModelCreacionDeFuncionBase mVMCreacionDeFuncion;

		/// <summary>
		/// Indice en la lista de <see cref="ViewModelBloqueFuncionBase"/> de <see cref="ViewModelCreacionDeFuncionBase"/>
		/// </summary>
		public int indiceBloque = -1;

		/// <summary>
		/// Indica si mostrar el espacio donde se posicionaria un bloque en caso de ser dropeado sobre este elemento
		/// </summary>
		public bool MostrarEspacioDrop { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> al que pertenece este bloque</param>
		public ViewModelBloqueFuncionBase(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
		{
			mVMCreacionDeFuncion = _vmCreacionDeFuncion;
		}

		/// <summary>
		/// Crea una copiar superficial de este bloque
		/// </summary>
		/// <returns>Copia superficial de est <see cref="ViewModelBloqueFuncionBase"/></returns>
		public ViewModelBloqueFuncionBase Copiar()
		{
			return (ViewModelBloqueFuncionBase)MemberwiseClone();
		}

		public virtual BloqueBase GenerarBloque() => null;

		/// <summary>
		/// Verifica que este bloque sea valido
		/// </summary>
		/// <returns><see cref="bool"/> indicando si este bloque es valido</returns>
		public virtual bool VerificarValidez() => true;

		public void OnDragEnter(ViewModel vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque)
			{
				MostrarEspacioDrop = true;

				vmBloque.indiceBloque = indiceBloque;
			}
		}

		public void OnDragLeave(ViewModel vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque)
			{
				MostrarEspacioDrop = false;

				vmBloque.indiceBloque = -1;
			}
		}

		public void OnDrop(ViewModel vm)
		{
			MostrarEspacioDrop = false;
		}
	}
}