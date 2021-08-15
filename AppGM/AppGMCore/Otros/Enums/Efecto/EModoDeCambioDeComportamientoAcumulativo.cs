using System;

namespace AppGM.Core
{
	/// <summary>
	/// Indica como realizar el cambio de <see cref="EComportamientoAcumulativo"/> en un <see cref="ControladorEfecto"/>
	/// o en un <see cref="ControladorEfectoSiendoAplicado"/>
	/// </summary>
	[Flags]
	[AccesibleEnGuraScratch(nameof(EModoDeCambioDeComportamientoAcumulativo))]
	public enum EModoDeCambioDeComportamientoAcumulativo
	{
		/// <summary>
		/// Indica que los <see cref="EComportamientoAcumulativo"/> de los <see cref="ControladorEfectoSiendoAplicado"/> activos
		/// tambien deben ser modificados.
		/// 
		/// Esta opcion solo tiene efecto al modificar un <see cref="ControladorEfecto"/>
		/// </summary>
		ModificarAplicacionesActivas = 1 << 0,

		/// <summary>
		/// Indica que los <see cref="EComportamientoAcumulativo"/> de los <see cref="ControladorEfectoSiendoAplicado"/> activos
		/// del tipo anterior al cambio de comportamiento deben ser actualizados
		/// 
		/// Esta opcion solo tiene efecto al modificar un <see cref="ControladorEfecto"/>
		/// </summary>
		ModificarAplicacionesActivasConElComportamientoAnterior = 1 << 1,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se deben sumar los turnos restantes de todos los efectos que se estaban solapando
		/// </summary>
		SumarTurnosRestantes = 1 << 2,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// los turnos restantes del efecto resultante deberan ser los de el efecto con mas turnos restantes
		/// </summary>
		TomarValorMasAltoDeTurnosRestantes = 1 << 3,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// los turnos restantes del efecto resultante deberan ser el promedio de turnos restantes de todas las aplicaciones
		/// </summary>
		TomarValorPromedioDeTurnosRestantes = 1 << 4,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se los turnos restantes del efecto resultante deberan ser los de el efecto con menos turnos restantes
		/// </summary>
		TomarValorMasBajoDeTurnosResntantes = 1 << 4,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara de <see cref="EComportamientoAcumulativo.SumarTurnos"/> a
		/// <see cref="EComportamientoAcumulativo.Solapar"/>, indica que los turnos restantes acumulados se juntaran en una sola aplicacion
		/// del efecto
		/// </summary>
		JuntarTurnosEnUnaAplicacion = 1 << 5,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara de <see cref="EComportamientoAcumulativo.SumarTurnos"/> a
		/// <see cref="EComportamientoAcumulativo.Solapar"/>, indica que los turnos restantes acumulados se repartiran para crear
		/// todas las aplicaciones del efecto posibles siempre intentando alcanzar su duracion maxima
		/// </summary>
		RepartirTurnos = 1 << 6,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara a <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se ignoraran las acumulaciones y los efectos creados tendran sus turnos restantes al maximo
		/// </summary>
		IgnorarAcumulacionesYMaxear = 1 << 7,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara a <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se ignoraran las turnos acumulaciones y los efectos creados tendran sus turnos restantes al minimo
		/// </summary>
		IgnorarAcumulacionesYDejarAlMinimo = 1 << 8,

		/// <summary>
		/// Se deshecharan por completo todas las aplicaciones del efecto con un <see cref="EComportamientoAcumulativo"/> distinto.
		/// Si se utiliza para modificar <see cref="ControladorEfectoSiendoAplicado"/> esto solo tiene efecto en instancias del efecto con el mismo <see cref="EComportamientoAcumulativo"/>.
		/// Por otro lado si se utiliza para modificar un <see cref="ControladorEfecto"/> esto tiene efecto para todas las aplicaciones del efecto
		/// </summary>
		EliminarAplicacionesConElComportamientoAnterior = 1 << 9,

		/// <summary>
		/// Se deshecharan por completo todas las aplicaciones del efecto con un <see cref="EComportamientoAcumulativo"/> distinto.
		/// Si se utiliza para modificar <see cref="ControladorEfectoSiendoAplicado"/> esto solo tiene efecto en instancias del efecto con el mismo <see cref="EComportamientoAcumulativo"/>.
		/// Por otro lado si se utiliza para modificar un <see cref="ControladorEfecto"/> esto tiene efecto para todas las aplicaciones del efecto
		/// </summary>
		EliminarAplicacionesConComportamientoDistinto = 1 << 10,

		/// <summary>
		/// Comportamiento por defecto, vaya a saber si llegue el dia en que se use otro
		/// </summary>
		PorDefecto = ModificarAplicacionesActivas | TomarValorPromedioDeTurnosRestantes | RepartirTurnos,

		/// <summary>
		/// Buena suerte con tu vida
		/// </summary>
		NINGUNO = 0

	}
}
