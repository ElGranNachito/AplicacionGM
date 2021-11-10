using System;
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

	    //Todas estas propiedades simplemente devuelven los valores de un enum como una lista para su uso en ComboBoxes
        public static List<ERango>                   RangosDisponibles               => Enum.GetValues(typeof(ERango)).Cast<ERango>().ToList();
        public static List<ETemporada>               TemporadaDelAño                 => Enum.GetValues(typeof(ETemporada)).Cast<ETemporada>().ToList();
        public static List<ECondicionClimatica>      CondicionesClimaticas           => Enum.GetValues(typeof(ECondicionClimatica)).Cast<ECondicionClimatica>().ToList();
        public static List<ECaracteristicasAmbiente> CaracteristicasAmbiente         => Enum.GetValues(typeof(ECaracteristicasAmbiente)).Cast<ECaracteristicasAmbiente>().ToList();
        public static List<EArquetipo>               ArquetiposDisponibles           => Enum.GetValues(typeof(EArquetipo)).Cast<EArquetipo>().ToList();
        public static List<EBienestar>               BienestarPersonaje              => Enum.GetValues(typeof(EBienestar)).Cast<EBienestar>().ToList();
        public static List<EClaseServant>            ClasesDisponibles               => Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();
        public static List<ETipoPersonaje>           TiposDePersonajesDisponibles    => Enum.GetValues(typeof(ETipoPersonaje)).Cast<ETipoPersonaje>().ToList();
        public static List<EManoDominante>           TiposDeManoDominanteDisponibles => Enum.GetValues(typeof(EManoDominante)).Cast<EManoDominante>().ToList();
        public static List<EParteDelCuerpo>          PartesDelCuerpoDisponibles      => Enum.GetValues(typeof(EParteDelCuerpo)).Cast<EParteDelCuerpo>().ToList();
        public static List<ESexo>                    SexosDisponibles                => Enum.GetValues(typeof(ESexo)).Cast<ESexo>().ToList();
        public static List<EStat>                    StatsDisponibles                => Enum.GetValues(typeof(EStat)).Cast<EStat>().ToList();
        public static List<ETipoDeDaño>              TiposDeDañoDisponibles          => Enum.GetValues(typeof(ETipoDeDaño)).Cast<ETipoDeDaño>().ToList();
        public static List<EUsoDeHabilidad>          UsosDeHabilidadDisponibles      => Enum.GetValues(typeof(EUsoDeHabilidad)).Cast<EUsoDeHabilidad>().ToList();
        public static List<ETipoHabilidad>           TiposDeHabilidadDisponibles     => Enum.GetValues(typeof(ETipoHabilidad)).Cast<ETipoHabilidad>().ToList();
        public static List<ETipoTirada>              TiposDeTiradasDisponibles       => Enum.GetValues(typeof(ETipoTirada)).Cast<ETipoTirada>().ToList();
        public static List<ETipoEfecto>              TiposDeEfectoDisponibles => Enum.GetValues(typeof(ETipoEfecto)).Cast<ETipoEfecto>().ToList();
        public static List<EComportamientoAcumulativo> ComportamientosAcumulativosDisponibles => Enum.GetValues(typeof(EComportamientoAcumulativo)).Cast<EComportamientoAcumulativo>().ToList();
        public static List<ETipoItem> TiposItemDisponibles => Enum.GetValues(typeof(ETipoItem)).Cast<ETipoItem>().ToList();
        public static List<EEstadoPortacion> EstadosDePortacionDisponibles => Enum.GetValues(typeof(EEstadoPortacion)).Cast<EEstadoPortacion>().ToList();

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
            //Hacemos este if pare prevenir una division por cero
            return rango == ERango.F ? 2 : 2 + (int)rango / 2;
        }

        /// <summary>
        /// Converte un rango a su valor numerico correspondiente.
        /// </summary>
        /// <param name="rango">Rango que convertir</param>
        /// <returns>Valor numerico equivalente a este rango</returns>
        /// La verdad es que esta funcion no hace mucho pero me parece que queda mas prolijo que hacer un casteo
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AValorNumerico(this ERango rango) => (int) rango;

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
            
            return (valores.Length==indice) ? valores[0] : valores[indice];            
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
	                return string.Intern("Arma");
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
        /// Obtiene el valor string correspondiente al <see cref="EClima"/>.
        /// </summary>
        /// <param name="clima"><see cref="EClima"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringClima(this EClima clima)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (clima)
            {
                case EClima.Granizo: stringBuilder.Append("Granizo");
                    break;
                case EClima.Lluvia: stringBuilder.Append("Lluvia");
                    break;
                case EClima.Neblina: stringBuilder.Append("Neblina");
                    break;
                case EClima.Nieve: stringBuilder.Append("Nieve");
                    break;
                case EClima.Nublado: stringBuilder.Append("Nublado");
                    break;
                case EClima.Soleado: stringBuilder.Append("Soleado");
                    break;
                case EClima.Tormenta: stringBuilder.Append("Tormenta");
                    break;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EViento"/>.
        /// </summary>
        /// <param name="viento"><see cref="EViento"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringViento(this EViento viento)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (viento)
            {
                case EViento.Brisa: stringBuilder.Append("Brisa");
                    break;
                case EViento.Rafagas: stringBuilder.Append("Rafagas");
                    break;
                case EViento.Viento: stringBuilder.Append("Viento");
                    break;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EHumedad"/>.
        /// </summary>
        /// <param name="humedad"><see cref="EHumedad"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringHumedad(this EHumedad humedad)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (humedad)
            {
                case EHumedad.Humedad: stringBuilder.Append("Humedad");
                    break;
                case EHumedad.MuchaHumedad: stringBuilder.Append("MuchaHumedad");
                    break;
                case EHumedad.Seco: stringBuilder.Append("Seco");
                    break;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="ETemperatura"/>.
        /// </summary>
        /// <param name="temperatura"><see cref="ETemperatura"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringTemperatura(this ETemperatura temperatura)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (temperatura)
            {
                case ETemperatura.Frio: stringBuilder.Append("Frio");
                    break;
                case ETemperatura.Calor: stringBuilder.Append("Calor");
                    break;
                case ETemperatura.Templado: stringBuilder.Append("Templado");
                    break;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Obtiene el valor string correspondiente al <see cref="EDiaSemana"/>.
        /// </summary>
        /// <param name="diaSemana"><see cref="EDiaSemana"/>del que se desea obtener un valor string</param>
        /// <returns></returns>
        public static string ToStringDiaSemana(this EDiaSemana diaSemana)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (diaSemana)
            {
                case EDiaSemana.Lunes: stringBuilder.Append("Lunes");
                    break;
                case EDiaSemana.Martes: stringBuilder.Append("Martes");
                    break;
                case EDiaSemana.Miercoles: stringBuilder.Append("Miercoles");
                    break;
                case EDiaSemana.Jueves: stringBuilder.Append("Jueves");
                    break;
                case EDiaSemana.Viernes: stringBuilder.Append("Viernes");
                    break;
                case EDiaSemana.Sabado: stringBuilder.Append("Sabado");
                    break;
                case EDiaSemana.Domingo: stringBuilder.Append("Domingo");
                    break;
            }

            return stringBuilder.ToString();
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

        public static bool EsAceptarOFinalizar(this EResultadoViewModel resultado) => resultado is EResultadoViewModel.Aceptar or EResultadoViewModel.Finalizar;
    }
}
