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
		/// <para>Cambios en la funcionalidad:</para>	
		///		<list type="bullet">
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfectoSiendoAplicado"/></term>
		///				<description>Solo tiene efecto en instancias del efecto con el mismo objetivo.</description>
		///			</item>
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfecto"/></term>
		///				<description>Tiene efecto para todas las aplicaciones del efecto independientemente del objetivo de cada una.</description>
		///			</item>
		///		</list>
		/// </summary>
		ModificarAplicacionesActivas = 1 << 0,

		/// <summary>
		/// Indica que los <see cref="EComportamientoAcumulativo"/> de los <see cref="ControladorEfectoSiendoAplicado"/> activos
		/// cuyos valores sean iguales al comportamiento anterior deben ser actualizados
		///
		/// <para>Cambios en la funcionalidad:</para>	
		///		<list type="bullet">
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfectoSiendoAplicado"/></term>
		///				<description>Solo tiene efecto en instancias del efecto con el mismo objetivo.</description>
		///			</item>
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfecto"/></term>
		///				<description>Tiene efecto para todas las aplicaciones del efecto independientemente del objetivo de cada una.</description>
		///			</item>
		///		</list>
		/// </summary>
		ModificarAplicacionesActivasConElComportamientoAnterior = 1 << 1,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se deben sumar los turnos restantes de todos los efectos que se estaban solapando
		/// </summary>
		SumarTurnosRestantes = 1 << 3,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// los turnos restantes del efecto resultante deberan ser los de el efecto con mas turnos restantes
		/// </summary>
		TomarValorMasAltoDeTurnosRestantes = 1 << 4,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// los turnos restantes del efecto resultante deberan ser el promedio de turnos restantes de todas las aplicaciones
		/// </summary>
		TomarValorPromedioDeTurnosRestantes = 1 << 5,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> era <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se los turnos restantes del efecto resultante deberan ser los de el efecto con menos turnos restantes
		/// </summary>
		TomarValorMasBajoDeTurnosRestantes = 1 << 6,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara de <see cref="EComportamientoAcumulativo.SumarTurnos"/> a
		/// <see cref="EComportamientoAcumulativo.Solapar"/>, indica que los turnos restantes acumulados se juntaran en una sola aplicacion
		/// del efecto
		/// </summary>
		JuntarTurnosEnUnaAplicacion = 1 << 7,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara de <see cref="EComportamientoAcumulativo.SumarTurnos"/> a
		/// <see cref="EComportamientoAcumulativo.Solapar"/>, indica que los turnos restantes acumulados se repartiran para crear
		/// todas las aplicaciones del efecto posibles siempre intentando alcanzar su duracion maxima
		/// </summary>
		RepartirTurnos = 1 << 8,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara de <see cref="EComportamientoAcumulativo.SumarTurnos"/> a
		/// <see cref="EComportamientoAcumulativo.Solapar"/>, indica que los turnos restantes acumulados se repartiran para crear
		/// todas las aplicaciones del efecto posibles dejando a todas con el minimo de turnos posible
		/// </summary>
		RepartirTurnosAvariciosamente = 1 << 9,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara de <see cref="EComportamientoAcumulativo.SumarTurnos"/> a
		/// cualquier otro comportamiento menos <see cref="EComportamientoAcumulativo.Solapar"/>, indica que los turnos restantes acumulados
		/// se mantendran intactos
		/// </summary>
		MantenerTurnosAcumulados = 1 << 11,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara a <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se ignoraran los turnos restantes y los efectos creados los tendran al maximo
		/// </summary>
		IgnorarTurnosRestantesYMaxear = 1 << 12,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara a <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se ignoraran los turnos restantes y los efectos creados los tendran al minimo
		/// </summary>
		IgnorarTurnosRestantesYDejarAlMinimo = 1 << 13,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara a <see cref="EComportamientoAcumulativo.Solapar"/>, indica
		/// que se creara una aplicacion del efecto por cada acumulacion
		/// </summary>
		CrearUnaAplicacionPorAcumulacion = 1 << 14,

		/// <summary>
		/// Cuando el <see cref="EComportamientoAcumulativo"/> pasara a <see cref="EComportamientoAcumulativo.Solapar"/>, indica que
		/// se ignoraran las acumulaciones y se creara tan solo una aplicacion
		/// </summary>
		CrearUnaAplicacion = 1 << 15,

		MultiplicarAcumulacionesPorTurnosRestantes = 1<<16,

		/// <summary>
		/// Se quitaran todas las aplicaciones del efecto con el <see cref="EComportamientoAcumulativo"/> anterior.
		///
		/// <para>Cambios en la funcionalidad:</para>	
		///		<list type="bullet">
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfectoSiendoAplicado"/></term>
		///				<description>Solo tiene efecto en instancias del efecto con el mismo objetivo.</description>
		///			</item>
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfecto"/></term>
		///				<description>Tiene efecto para todas las aplicaciones del efecto independientemente del objetivo de cada una.</description>
		///			</item>
		///		</list>
		/// </summary>
		EliminarAplicacionesConElComportamientoAnterior = 1 << 17,

		/// <summary>
		/// Se quitaran todas las aplicaciones del efecto con un <see cref="EComportamientoAcumulativo"/> distinto.
		///
		/// <para>Cambios en la funcionalidad:</para>	
		///		<list type="bullet">
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfectoSiendoAplicado"/></term>
		///				<description>Solo tiene efecto en instancias del efecto con el mismo objetivo.</description>
		///			</item>
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfecto"/></term>
		///				<description>Tiene efecto para todas las aplicaciones del efecto independientemente del objetivo de cada una.</description>
		///			</item>
		///		</list>
		/// </summary>
		EliminarAplicacionesConComportamientoDistinto = 1 << 18,

		/// <summary>
		/// Se deshecharan por completo todas las aplicaciones activas.
		///
		///	<para>Cambios en la funcionalidad:</para>	
		///		<list type="bullet">
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfectoSiendoAplicado"/></term>
		///				<description>Solo tiene efecto en instancias del efecto con el mismo objetivo.</description>
		///			</item>
		/// 
		///			<item>
		///				<term>Utilizado en un <see cref="ControladorEfecto"/></term>
		///				<description>Tiene efecto para todas las aplicaciones del efecto independientemente del objetivo de cada una.</description>
		///			</item>
		///		</list>
		/// </summary>
		EliminarAplicaciones = 1 << 19,

		/// <summary>
		/// Comportamiento por defecto, vaya a saber si llegue el dia en que se use otro.
		///
		/// <para>Valores:</para>
		///		<list type="bullet">
		///
		///			<item>
		///				<see cref="EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivas"/>
		///			</item>
		///
		///			<item>
		///				<see cref="EModoDeCambioDeComportamientoAcumulativo.TomarValorPromedioDeTurnosRestantes"/>
		///			</item>
		///
		///			<item>
		///				<see cref="EModoDeCambioDeComportamientoAcumulativo.RepartirTurnos"/>
		///			</item>
		///
		///			<item>
		///				<see cref="EModoDeCambioDeComportamientoAcumulativo.CrearUnaAplicacionPorAcumulacion"/>
		///			</item>
		///		
		///		</list>
		/// </summary>
		PorDefecto = ModificarAplicacionesActivas | TomarValorPromedioDeTurnosRestantes | RepartirTurnos | CrearUnaAplicacionPorAcumulacion,

		/// <summary>
		/// Contiene los valores de <see cref="EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivas"/> y
		/// <see cref="EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivasConElComportamientoAnterior"/>
		/// </summary>
		AmbasModificaciones = ModificarAplicacionesActivas | ModificarAplicacionesActivasConElComportamientoAnterior,

		/// <summary>
		/// Buena suerte con tu vida
		/// </summary>
		NINGUNO = 0,

	}
}
