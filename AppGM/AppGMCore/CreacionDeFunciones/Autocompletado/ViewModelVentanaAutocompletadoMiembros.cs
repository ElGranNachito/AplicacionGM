using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Enum que representa tipos de <see cref="MemberInfo"/> de una clase
	/// </summary>
	[Flags]
	public enum EMiembrosBuscados
	{
		Campos      = 1<<0,
		Propiedades = 1<<1,
		Funciones   = 1<<2
	}

	/*
	/// <summary>
	/// Ventana de autocompletado para valores de tipo <see cref="MemberInfo"/>
	/// </summary>
	public class ViewModelVentanaAutocompletadoMiembros : ViewModelVentanaAutocompletado
	{
		/// <summary>
		/// <see cref="MemberInfo"/> que estamos buscando
		/// </summary>
		public EMiembrosBuscados miembrosBuscados;

		protected override List<ViewModelItemAutocompletado<MemberInfo>> ActualizarPosibilidades()
		{
			//TODO:Implementar
			return new List<ViewModelItemAutocompletado<MemberInfo>>
			{
				new ViewModelItemAutocompletadoMiembro(
					typeof(ModeloPersonajeJugable).GetMember(nameof(ModeloPersonajeJugable.Hp))[0])
			};
		}
	}*/
}