using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa una cadena de If y Elses
	/// </summary>
	public class ViewModelBloqueCondicionalCompleto : ViewModelBloqueContenedor<BloqueCondicionalCompleto>
	{
		/// <summary>
		/// Comando que se ejecuta al presionar el boton para añadir un nuevo <see cref="ViewModelBloqueCondicional"/> a
		/// <see cref="CondicionesConsecuentes"/>
		/// </summary>
		public ICommand ComandoAñadirBloque { get; set; }

		public ViewModelBloqueCondicionalCompleto(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
			:base(_vmCreacionDeFuncion)
		{
			ComandoAñadirBloque = new Comando(()=> AñadirBloque(new ViewModelBloqueCondicional(mVMCreacionDeFuncion, ETipoBloqueCondicional.Else)));

			AñadirBloque(new ViewModelBloqueCondicional(mVMCreacionDeFuncion, ETipoBloqueCondicional.If));
		}

		public override BloqueCondicionalCompleto GenerarBloque_Impl()
		{
			IEnumerable<ViewModelBloqueCondicional> vmsCondiciones = Bloques.Elementos.Cast<ViewModelBloqueCondicional>();

			List<BloqueCondicional> condiciones = new List<BloqueCondicional>(vmsCondiciones.Select(condicion => condicion.GenerarBloque_Impl()));

			List<List<BloqueBase>> acciones = new List<List<BloqueBase>>();

			foreach (var condicion in vmsCondiciones)
			{
				acciones.Add(condicion.Bloques.Elementos.Select(bloque => bloque.GenerarBloque()).ToList());
			}

			var resultado = new BloqueCondicionalCompleto(condiciones, acciones);

			return resultado;
		}

		public override void OnDragSalio_Impl(IDrageable vm)
		{
			IEnumerable<ViewModelBloqueCondicional> vmsCondiciones = Bloques.Elementos.Cast<ViewModelBloqueCondicional>();

			foreach (var condicion in vmsCondiciones)
			{
				if (condicion.ReceptorAñadirBloque.EsVisible)
					return;
			}

			MostrarEspacioDrop = false;
		}

		public override bool OnDrop_Impl(IDrageable vm)
		{
			foreach (var bloque in Bloques)
			{
				if (bloque is ViewModelBloqueCondicional condicion)
					condicion.ReceptorAñadirBloque.EsVisible = false;
			}

			return base.OnDrop_Impl(vm);
		}
	}
}