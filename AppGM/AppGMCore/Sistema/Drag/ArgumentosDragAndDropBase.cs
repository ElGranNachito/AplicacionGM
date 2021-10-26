using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Argumentos de un evento de drag
	/// </summary>
	public abstract record ArgumentosDragAndDropBase
	{
		/// <summary>
		/// Argumentos pasados al realizar el drag
		/// </summary>
		public Dictionary<int, object> args = new Dictionary<int, object>();

		/// <summary>
		/// Constructor base
		/// </summary>
		/// <param name="_args">Diccionario con los argumentos del drag</param>
		public ArgumentosDragAndDropBase(Dictionary<int, object> _args)
		{
			foreach (var arg in args)
			{
				args.Add(arg.Key, arg.Value);
			}
		}
	}
}
