using System;

namespace AppGM.Core
{
	/// <summary>
	/// Indica la manera en la que se acumularan los <see cref="ControladorEfectoSiendoAplicado"/> en un <see cref="ControladorPersonaje"/>
	/// </summary>
	[AccesibleEnGuraScratch(nameof(EComportamientoAcumulativo))]
	public enum EComportamientoAcumulativo
	{
		/// <summary>
		/// Se crea un nuevo <see cref="ControladorEfectoSiendoAplicado"/> por cada acumulacion
		/// que se aplica paralelamente a los que ya se estan aplicando
		/// </summary>
		Solapar         = 1,

		/// <summary>
		/// Se crea un solo <see cref="ControladorEfectoSiendoAplicado"/> y en acumulaciones posteriores
		/// simplemente se suma a un contador
		/// </summary>
		Contar          = 2,

		/// <summary>
		/// Solo un <see cref="ControladorEfectoSiendoAplicado"/> puede estar activo a la vez y los demas tendran que esperar
		/// </summary>
		Esperar         = 3,

		/// <summary>
		/// Por cada acumulacion del efecto se suman mas turnos a la duracion
		/// </summary>
		SumarTurnos     = 4,

		/// <summary>
		/// El comportamiento se selecciona a la hora de utlizar la Habilidad/Objeto/Algo que aplique el efecto
		/// </summary>
		SeleccionManual = 5,

		NINGUNO = 0
	}

	[Flags]
	public enum EComportamientoAcumulativoFlags
	{
		/// <summary>
		/// Se crea un nuevo <see cref="ControladorEfectoSiendoAplicado"/> por cada acumulacion
		/// que se aplica paralelamente a los que ya se estan aplicando
		/// </summary>
		Solapar = 1<<0,

		/// <summary>
		/// Se crea un solo <see cref="ControladorEfectoSiendoAplicado"/> y en acumulaciones posteriores
		/// simplemente se suma a un contador
		/// </summary>
		Contar = 1<<1,

		/// <summary>
		/// Solo un <see cref="ControladorEfectoSiendoAplicado"/> puede estar activo a la vez y los demas tendran que esperar
		/// </summary>
		Esperar = 1<<2,

		/// <summary>
		/// Por cada acumulacion del efecto se suman mas turnos a la duracion
		/// </summary>
		SumarTurnos = 1<<3,

		/// <summary>
		/// El comportamiento se selecciona a la hora de utlizar la Habilidad/Objeto/Algo que aplique el efecto
		/// </summary>
		SeleccionManual = 1<<4,

		TODOS = Solapar | Contar | Esperar | SumarTurnos | SeleccionManual,
		NINGUNO = 0
	}
}