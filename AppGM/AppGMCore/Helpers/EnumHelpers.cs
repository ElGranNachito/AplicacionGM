using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CoolLogs;

namespace AppGM.Core
{
    /// <summary>
    /// Funciones y Propiedades estaticas para facilitar operaciones con varios tipos de enums
    /// </summary>
    public static class EnumHelpers
    {

		#region Obtencion de los valores de enums

		//Todas estas propiedades simplemente devuelven los valores de un enum como una lista para su uso en ComboBoxes
		public static List<ERango> RangosDisponibles => Enum.GetValues(typeof(ERango)).Cast<ERango>().ToList();
		public static List<ETemporada> TemporadaDelAño => Enum.GetValues(typeof(ETemporada)).Cast<ETemporada>().ToList();
		public static List<ECondicionClimatica> CondicionesClimaticas => Enum.GetValues(typeof(ECondicionClimatica)).Cast<ECondicionClimatica>().ToList();
		public static List<ECaracteristicasAmbiente> CaracteristicasAmbiente => Enum.GetValues(typeof(ECaracteristicasAmbiente)).Cast<ECaracteristicasAmbiente>().ToList();
		public static List<EArquetipo> ArquetiposDisponibles => Enum.GetValues(typeof(EArquetipo)).Cast<EArquetipo>().ToList();
		public static List<EBienestar> BienestarPersonaje => Enum.GetValues(typeof(EBienestar)).Cast<EBienestar>().ToList();
		public static List<EClaseServant> ClasesDisponibles => Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();
		public static List<ETipoPersonaje> TiposDePersonajesDisponibles => Enum.GetValues(typeof(ETipoPersonaje)).Cast<ETipoPersonaje>().ToList();
		public static List<ETipoAccion> TiposDeAccionesDisponibles => Enum.GetValues(typeof(ETipoAccion)).Cast<ETipoAccion>().ToList();
		public static List<EManoDominante> TiposDeManoDominanteDisponibles => Enum.GetValues(typeof(EManoDominante)).Cast<EManoDominante>().ToList();
		public static List<EParteDelCuerpo> PartesDelCuerpoDisponibles => Enum.GetValues(typeof(EParteDelCuerpo)).Cast<EParteDelCuerpo>().ToList();
		public static List<ESexo> SexosDisponibles => Enum.GetValues(typeof(ESexo)).Cast<ESexo>().ToList();
		public static List<EStat> StatsDisponibles => Enum.GetValues(typeof(EStat)).Cast<EStat>().ToList();
		public static List<ETipoDeDaño> TiposDeDañoDisponibles => Enum.GetValues(typeof(ETipoDeDaño)).Cast<ETipoDeDaño>().ToList();
		public static List<EUsoDeHabilidad> UsosDeHabilidadDisponibles => Enum.GetValues(typeof(EUsoDeHabilidad)).Cast<EUsoDeHabilidad>().ToList();
		public static List<ETipoHabilidad> TiposDeHabilidadDisponibles => Enum.GetValues(typeof(ETipoHabilidad)).Cast<ETipoHabilidad>().ToList();
		public static List<ETipoTirada> TiposDeTiradasDisponibles => Enum.GetValues(typeof(ETipoTirada)).Cast<ETipoTirada>().ToList();
		public static List<ETipoEfecto> TiposDeEfectoDisponibles => Enum.GetValues(typeof(ETipoEfecto)).Cast<ETipoEfecto>().ToList();
		public static List<EComportamientoAcumulativo> ComportamientosAcumulativosDisponibles => Enum.GetValues(typeof(EComportamientoAcumulativo)).Cast<EComportamientoAcumulativo>().ToList();
		public static List<EEstadoPortacion> EstadosDePortacionDisponibles => Enum.GetValues(typeof(EEstadoPortacion)).Cast<EEstadoPortacion>().ToList();
		public static List<ETipoItem> TiposItemDisponibles => Enum.GetValues(typeof(ETipoItem)).Cast<ETipoItem>().ToList();
		public static List<EEstrategiaDeDeteccionDeDaño> TiposDeDeteccionDeDañoDisponibles => Enum.GetValues<EEstrategiaDeDeteccionDeDaño>().ToList();
		public static List<EMetodoDeReduccionDeDaño> MetodosDeReduccionDeDañoDisponibles => Enum.GetValues<EMetodoDeReduccionDeDaño>().ToList();
		public static List<ENivelMagia> NivelesDeMagiaDisponibles => Enum.GetValues<ENivelMagia>().ToList();


        /// <summary>
        /// Obtiene todos los valores disponibles de un <typeparamref name="TEnum"/>
        /// </summary>
        /// <typeparam name="TEnum">Tipo del <see cref="Enum"/> cuyos valores obtener</typeparam>
        /// <param name="valoresQueExcluir">Valores del <typeparamref name="TEnum"/> que excluir del resultado</param>
        /// <returns><see cref="List{T}"/> con todos los valores disponibles del <typeparamref name="TEnum"/></returns>
		public static List<TEnum> ObtenerValoresEnum<TEnum>(IEnumerable<TEnum> valoresQueExcluir = null)

			where TEnum: struct, Enum
		{
			var valoresEnum = Enum.GetValues<TEnum>().ToList();

			if (valoresQueExcluir is not null)
			{
				valoresQueExcluir = valoresQueExcluir.ToList();

				foreach (var valor in valoresQueExcluir)
				{
					valoresEnum.Remove(valor);
				}
            }

			return valoresEnum;
		}

		#endregion

		/// <summary>
		/// Transforma el valor del <see cref="EFormatoImagen"/> a una cadena
		/// </summary>
		/// <param name="formato">Froamto que convertir a cadena</param>
		/// <returns>Valor del <paramref name="formato"/> como cadena</returns>
		public static string Valor(this EFormatoImagen formato) => "." + Enum.GetName(typeof(EFormatoImagen), formato);

		/// <summary>
		/// Obtiene el modificador correspondiente para un <see cref="ERango"/>
		/// </summary>
		/// <param name="rango">Rango cuyo modificador obtener</param>
		/// <returns>Valor del modificador</returns>
		public static int ObtenerModificador(this ERango rango)
		{
			return Convert.ToInt32(Math.Floor(((int)rango - 10) / 2.0f));
		}

		/// <summary>
        /// Obtiene una lista con las <see cref="EOperacionLogica"/> que se pueden realizar con el <paramref name="tipo"/>
        /// </summary>
        /// <param name="tipo"><see cref="Type"/> Con el que se quiere realizar la <see cref="EOperacionLogica"/></param>
        /// <returns><see cref="List{T}"/> con las <see cref="EOperacionLogica"/> posibles</returns>
        public static List<EOperacionLogica> ObtenerOperacionesLogicasDisponibles(this Type tipo)
        {
	        var operacionesLogicasDisponibles = Enum.GetValues(typeof(EOperacionLogica)).Cast<EOperacionLogica>();

	        switch (tipo)
	        {
                case Type t when t == typeof(bool):

	                return operacionesLogicasDisponibles.RemoverRango(new[]
	                {
		                EOperacionLogica.Igual, EOperacionLogica.NoIgual,
		                EOperacionLogica.Mayor, EOperacionLogica.Menor,
		                EOperacionLogica.MayorIgual, EOperacionLogica.MenorIgual
	                }).ToList();

                case Type t when t.IsValueType && (
                    t == typeof(int)   || t == typeof(uint)   ||
                    t == typeof(short) || t == typeof(ushort) ||
                    t == typeof(byte)  || t == typeof(sbyte)  ||
                    t == typeof(long)  || t == typeof(ulong)  ||
                    t == typeof(float) || t == typeof(double)):

	                return operacionesLogicasDisponibles.RemoverRango(new[] {EOperacionLogica.O, EOperacionLogica.Y, EOperacionLogica.No}).ToList();

                case Type t when t.IsByRef:

	                return operacionesLogicasDisponibles.RemoverRango(new[]
	                {
		                EOperacionLogica.O, EOperacionLogica.Y,
		                EOperacionLogica.No, EOperacionLogica.Mayor,
		                EOperacionLogica.Menor, EOperacionLogica.MayorIgual,
		                EOperacionLogica.MenorIgual
	                }).ToList();

	        }

	        var resultado = operacionesLogicasDisponibles.ToList();

	        resultado.Remove(EOperacionLogica.NINGUNA);

	        return resultado;
        }

        public static List<ETipoHabilidad> ObtenerTiposDeHabilidadDisponibles(this ETipoPersonaje tipoPersonaje)
        {
	        var tiposDeHabilidad = Enum.GetValues(typeof(ETipoHabilidad)).Cast<ETipoHabilidad>().ToList();

	        tiposDeHabilidad.Remove(ETipoHabilidad.NINGUNO);

	        if (tipoPersonaje != ETipoPersonaje.Servant)
		        tiposDeHabilidad.Remove(ETipoHabilidad.NoblePhantasm);

	        return tiposDeHabilidad;
        }

        /// <summary>
        /// Obtiene el <see cref="EComportamientoAcumulativoFlags"/> correspondiente para <paramref name="comportamientoAcumulativo"/>
        /// </summary>
        /// <param name="comportamientoAcumulativo">Comportamiento acumulativo para el cual obtener la flag</param>
        /// <returns><see cref="EComportamientoAcumulativoFlags"/> equivalente a <paramref name="comportamientoAcumulativo"/></returns>
        public static EComportamientoAcumulativoFlags ObtenerFlag(this EComportamientoAcumulativo comportamientoAcumulativo)
        {
	        switch (comportamientoAcumulativo)
	        {
                case EComportamientoAcumulativo.Contar:
	                return EComportamientoAcumulativoFlags.Contar;

                case EComportamientoAcumulativo.Esperar:
	                return EComportamientoAcumulativoFlags.Esperar;

                case EComportamientoAcumulativo.Solapar:
	                return EComportamientoAcumulativoFlags.Solapar;

                case EComportamientoAcumulativo.SumarTurnos:
	                return EComportamientoAcumulativoFlags.SumarTurnos;

                case EComportamientoAcumulativo.SeleccionManual:
	                return EComportamientoAcumulativoFlags.SeleccionManual;

                default:
                {
                    SistemaPrincipal.LoggerGlobal.Log($"{nameof(comportamientoAcumulativo)} contiene un valor no soportado {comportamientoAcumulativo}", ESeveridad.Error);

                    return EComportamientoAcumulativoFlags.NINGUNO;
                }
	        }
        }

        /// <summary>
        /// Avanza un valor en la enum.
        /// </summary>
        /// <typeparam name="T">Generico del enum del que se avanza</typeparam>
        /// <param name="e">Enum del que se desea obtener el valor que le sigue</param>
        /// <returns></returns>
        public static T Siguiente<T>(this T e) where T : struct
        {
            if (!typeof(T).IsEnum) 
                throw new ArgumentException(String.Format($"El argumento {typeof(T).FullName} no es un Enum"));

            T[] valores = (T[])Enum.GetValues(e.GetType());
            
            int indice = Array.IndexOf<T>(valores, e) + 1;
            
            return (valores.Length==indice) ? valores[0] : valores[indice];            
        }

        /// <summary>
        /// Retrocede un valor en la enum.
        /// </summary>
        /// <typeparam name="T">Generico del enum del que se retrocede</typeparam>
        /// <param name="e">Enum del que se desea obtener el valor que le anterior</param>
        /// <returns></returns>
        public static T Anterior<T>(this T e) where T : struct
        {
            if (!typeof(T).IsEnum) 
                throw new ArgumentException(String.Format($"El argumento {typeof(T).FullName} no es un Enum"));

            T[] valores = (T[])Enum.GetValues(e.GetType());
            
            int indice = Array.IndexOf<T>(valores, e) - 1;
            
            return (indice < 0) ? valores.Max() : valores[indice];          
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="ENumeroParty"/>.
        /// </summary>
        /// <param name="numeroParty"><see cref="ENumeroParty"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringNumeroParty(this ENumeroParty numeroParty)
        {
	        switch (numeroParty)
            {
                case ENumeroParty.Party_0:
	                return string.Intern("Party 0");
                case ENumeroParty.Party_Saber: 
	                return string.Intern("Party Saber");
                case ENumeroParty.Party_Lancer:
	                return string.Intern("Party Lancer");
                case ENumeroParty.Party_Archer:
	                return string.Intern("Party Archer");
                case ENumeroParty.Party_Rider:
	                return string.Intern("Party Rider");
                case ENumeroParty.Party_Berserker:
	                return string.Intern("Party Berserker");
                case ENumeroParty.Party_Assassin:
	                return string.Intern("Party Assassin");
                case ENumeroParty.Party_Caster:
	                return string.Intern("Party Caster");
                default:
	                return string.Empty;
            }
        }

        public static string ToStringTipoItem(this ETipoItem tipoItem)
        {
	        switch (tipoItem)
	        {
                case ETipoItem.Arma:
	                return string.Intern("Arma a distancia");
                case ETipoItem.Defensivo:
	                return string.Intern("Defensivo");
                case ETipoItem.Item:
					return string.Intern("Item");
                case ETipoItem.MysticCode:
	                return string.Intern("Mystic Code");
                case ETipoItem.Ropa:
	                return string.Intern("Ropita");
                default:
	                return string.Empty;
	        }
        }

        /// <summary>
        /// Obtiene los valores de tipo de deteccion de daño disponible para el valor de <paramref name="estrategiaDeDeteccion"/>
        /// </summary>
        /// <param name="estrategiaDeDeteccion">Tipo de deteccion para el que se queiren obtener los valores disponibles</param>
        /// <returns><see cref="List{T}"/> con los valores de deteccion disponibles para <paramref name="estrategiaDeDeteccion"/></returns>
        public static List<Enum> ObtenerValoresDeDeteccionDeDañoDisponibles(this EEstrategiaDeDeteccionDeDaño estrategiaDeDeteccion)
        {
            switch (estrategiaDeDeteccion)
            {
                case EEstrategiaDeDeteccionDeDaño.Nivel:
                    return EnumHelpers.NivelesDeMagiaDisponibles.Cast<Enum>().ToList();
                case EEstrategiaDeDeteccionDeDaño.Rango:
	                return ObtenerValoresEnum<ERangoFlags>().Cast<Enum>().ToList();
                case EEstrategiaDeDeteccionDeDaño.TipoDeDaño:
                    return ObtenerValoresEnum<ETipoDeDaño>().Cast<Enum>().ToList();
                case EEstrategiaDeDeteccionDeDaño.FuenteDelDaño:
                {
                    SistemaPrincipal.LoggerGlobal.Log($"el valor de {nameof(estrategiaDeDeteccion)} no puede ser {EEstrategiaDeDeteccionDeDaño.FuenteDelDaño}", ESeveridad.Error);

                    return new List<Enum>();
                }
            }

            return new List<Enum>();
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EClima"/>.
        /// </summary>
        /// <param name="clima"><see cref="EClima"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringClima(this EClima clima)
        {
            switch (clima)
            {
                case EClima.Granizo: 
                    return string.Intern("Granizo");
                case EClima.Lluvia: 
                    return string.Intern("Lluvia");
                case EClima.Neblina: 
                    return string.Intern("Neblina");
                case EClima.Nieve: 
                    return string.Intern("Nieve");
                case EClima.Nublado: 
                    return string.Intern("Nublado");
                case EClima.Soleado: 
                    return string.Intern("Soleado");
                case EClima.Tormenta: 
                    return string.Intern("Tormenta");
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EViento"/>.
        /// </summary>
        /// <param name="viento"><see cref="EViento"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringViento(this EViento viento)
        {
            switch (viento)
            {
                case EViento.Brisa: 
                    return string.Intern("Brisa");
                case EViento.Rafagas: 
                    return string.Intern("Rafagas");
                case EViento.Viento: 
                    return string.Intern("Viento");
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EHumedad"/>.
        /// </summary>
        /// <param name="humedad"><see cref="EHumedad"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringHumedad(this EHumedad humedad)
        {
            switch (humedad)
            {
                case EHumedad.Humedad: 
                    return string.Intern("Humedad");
                case EHumedad.MuchaHumedad: 
                    return string.Intern("MuchaHumedad");
                case EHumedad.Seco: 
                    return string.Intern("Seco");
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="ETemperatura"/>.
        /// </summary>
        /// <param name="temperatura"><see cref="ETemperatura"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringTemperatura(this ETemperatura temperatura)
        {
            switch (temperatura)
            {
                case ETemperatura.Frio: 
                    return string.Intern("Frio");
                case ETemperatura.Calor: 
                    return string.Intern("Calor");
                case ETemperatura.Templado: 
                    return string.Intern("Templado");
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EDiaSemana"/>.
        /// </summary>
        /// <param name="diaSemana"><see cref="EDiaSemana"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringDiaSemana(this EDiaSemana diaSemana)
        {
            switch (diaSemana)
            {
                case EDiaSemana.Lunes: 
                    return string.Intern("Lunes");
                case EDiaSemana.Martes: 
                    return string.Intern("Martes");
                case EDiaSemana.Miercoles: 
                    return string.Intern("Miercoles");
                case EDiaSemana.Jueves: 
                    return string.Intern("Jueves");
                case EDiaSemana.Viernes: 
                    return string.Intern("Viernes");
                case EDiaSemana.Sabado: 
                    return string.Intern("Sabado");
                case EDiaSemana.Domingo: 
                    return string.Intern("Domingo");
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EClaseServant"/>.
        /// </summary>
        /// <param name="claseServant"><see cref="EClaseServant"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringClaseServant(this EClaseServant claseServant)
        {
            switch (claseServant)
            {
                case EClaseServant.Saber: 
                    return string.Intern("Saber");
                case EClaseServant.Archer: 
                    return string.Intern("Archer");
                case EClaseServant.Lancer: 
                    return string.Intern("Lancer");
                case EClaseServant.Rider: 
                    return string.Intern("Rider");
                case EClaseServant.Berserker: 
                    return string.Intern("Berserker");
                case EClaseServant.Caster: 
                    return string.Intern("Caster");
                case EClaseServant.Assassin: 
                    return string.Intern("Assassin");
                default:
                    return string.Empty;
            }
        }

        /// Obtiene una representacion textual del valor de <paramref name="nivelMagia"/>
        /// </summary>
        /// <param name="nivelMagia">Valor de <see cref="ENivelMagia"/> que pasar a cadena</param>
        /// <returns></returns>
        public static string ToStringNivelMagia(this ENivelMagia nivelMagia)
        {
            switch (nivelMagia)
            {
                case ENivelMagia.Cero:
                    return "0";
                case ENivelMagia.Uno:
                    return "1";
                case ENivelMagia.Dos:
                    return "2";
                case ENivelMagia.Tres:
                    return "3";
                case ENivelMagia.Cuatro:
                    return "4";
                case ENivelMagia.Cinco:
                    return "5";
                case ENivelMagia.Seis:
                    return "6";
                case ENivelMagia.Siete:
                    return "7";
                case ENivelMagia.Ocho:
                    return "8";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Devuelve una cadena con todas las flags activas de un <typeparamref name="TEnum"/>
        /// </summary>
        /// <typeparam name="TEnum">Tipo del enum</typeparam>
        /// <param name="flags">Flags activas</param>
        /// <returns>Cadena con todos los valores activos de <paramref name="flags"/></returns>
        public static string FlagsActivasEnumToString<TEnum>(this TEnum flags)
            where TEnum : struct, Enum
        {
            StringBuilder sBuilder = new StringBuilder();

            List<TEnum> flagsActivas = new List<TEnum>();

            foreach (var valor in Enum.GetValues<TEnum>())
            {
                //Revisamos si la flag actual esta activada
                if (flags.HasFlag(valor))
                    flagsActivas.Add(valor);
            }

            return sBuilder.AppendJoin(", ", flagsActivas).ToString();
        }

        /// <summary>
        /// Obtiene el <see cref="Type"/> del modelo representado por un <see cref="ETipoDañable"/>
        /// </summary>
        /// <param name="tipoDañable">Valor de <see cref="ETipoDañable"/></param>
        /// <returns> <see cref="Type"/> del modelo representado por <paramref name="tipoDañable"/></returns>
        public static Type ObtenerTipoModelo(this ETipoDañable tipoDañable)
        {
	        switch (tipoDañable)
	        {
                case ETipoDañable.Item:
	                return typeof(ModeloItem);

                case ETipoDañable.Slot:
	                return typeof(ModeloSlot);

                case ETipoDañable.ParteDelCuerpo:
	                return typeof(ModeloParteDelCuerpo);

                case ETipoDañable.Personaje:
	                return typeof(ModeloPersonaje);

                default:
                    SistemaPrincipal.LoggerGlobal.Log($"{nameof(tipoDañable)}({tipoDañable}) valor no soportado", ESeveridad.Error);
	                return null;
	        }
        }

        /// <summary>
        /// Obtiene el <see cref="Type"/> del modelo representado por un <see cref="ETipoInfligidorDaño"/>
        /// </summary>
        /// <param name="tipoInfligidorDaño">Valor de <see cref="ETipoInfligidorDaño"/></param>
        /// <returns> <see cref="Type"/> del modelo representado por <paramref name="tipoInfligidorDaño"/></returns>
        public static Type ObtenerTipoModelo(this ETipoInfligidorDaño tipoInfligidorDaño)
        {
	        switch (tipoInfligidorDaño)
	        {
                case ETipoInfligidorDaño.Personaje:
	                return typeof(ModeloPersonaje);

                case ETipoInfligidorDaño.Habilidad:
	                return typeof(ModeloHabilidad);

                case ETipoInfligidorDaño.Item:
	                return typeof(ModeloItem);

                default:
	                SistemaPrincipal.LoggerGlobal.Log($"{nameof(tipoInfligidorDaño)}({tipoInfligidorDaño}) valor no soportado", ESeveridad.Error);
	                return null;
            }
        }

        public static ERango ARango(this int rango)
        {
	        rango = Math.Clamp(rango, 14, 22);

	        return (ERango) rango;
        }

        public static bool TieneFlagRango(this ERangoFlags flags, ERango rango)
        {
	        return ((int)flags & Convert.ToInt32(Math.Pow(2, (int) rango - 14))) != 0;
        }

        public static bool TieneFlagMagia(this ENivelMagiaFlags flags, ENivelMagia nivel)
        {
	        return ((int)flags & Convert.ToInt32(Math.Pow(2, (int)nivel))) != 0;
        }

        public static bool EsAceptarOFinalizar(this EResultadoViewModel resultado) => resultado is EResultadoViewModel.Aceptar or EResultadoViewModel.Finalizar;
    }
}