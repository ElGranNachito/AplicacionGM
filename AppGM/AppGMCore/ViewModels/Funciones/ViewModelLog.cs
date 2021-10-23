using System;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un log de GuraScratch
	/// </summary>
	public class ViewModelLog : ViewModel
	{
		public string Mensaje { get; set; }

		public ESeveridad Severidad { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_mensaje">Mensaje que contiene este log</param>
		/// <param name="_severidad">Severidad de este log</param>
		public ViewModelLog(string _mensaje, ESeveridad _severidad = ESeveridad.Info)
		{
			Mensaje   = $"[{DateTime.Now.ToString("T")}] {_mensaje}";
			Severidad = _severidad;
		}
	}
}
