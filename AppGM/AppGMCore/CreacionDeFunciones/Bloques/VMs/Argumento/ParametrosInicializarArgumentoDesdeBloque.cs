using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Contiene los parametros requeridos para incializar un <see cref="ViewModelArgumento"/> desde un <see cref="BloqueArgumento"/>
	/// </summary>
	public class ParametrosInicializarArgumentoDesdeBloque
	{
		public int                          idBloque;
		public string                       nombre;
		public bool                         detectarTipoAutomaticamente;
		public bool                         puedeQuedarVacio;
		public string                       textoActual;
		public Type                         tipoArgumento;
		public ViewModelBloqueFuncionBase   contenedor;
		public List<BloqueFuncion>          metodos;
	}
}