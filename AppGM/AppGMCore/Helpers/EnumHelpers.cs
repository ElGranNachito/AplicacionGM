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
        public static List<ETipoEfecto>              TiposDeEfectoDisponibles                 => Enum.GetValues(typeof(ETipoEfecto)).Cast<ETipoEfecto>().ToList();
        public static List<EComportamientoAcumulativo> ComportamientosAcumulativosDisponibles => Enum.GetValues(typeof(EComportamientoAcumulativo)).Cast<EComportamientoAcumulativo>().ToList();
        public static List<EEstadoPortacion>         EstadosDePortacionDisponibles            => Enum.GetValues(typeof(EEstadoPortacion)).Cast<EEstadoPortacion>().ToList();
        public static List<ETipoItem>                TiposItemDisponibles                     => Enum.GetValues(typeof(ETipoItem)).Cast<ETipoItem>().ToList();
        public static List<EEstrategiaDeDeteccionDeDaño>   TiposDeDeteccionDeDañoDisponibles        => Enum.GetValues<EEstrategiaDeDeteccionDeDaño>().ToList();
        public static List<EMetodoDeReduccionDeDaño> MetodosDeReduccionDeDañoDisponibles      => Enum.GetValues<EMetodoDeReduccionDeDaño>().ToList();
        public static List<ENivelMagia>              NivelesDeMagiaDisponibles                => Enum.GetValues<ENivelMagia>().ToList();

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
            where TEnum: struct, Enum
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
	                return EnumHelpers.RangosDisponibles.Cast<Enum>().ToList();
                case EEstrategiaDeDeteccionDeDaño.TipoDeDaño:
	                return EnumHelpers.TiposDeDañoDisponibles.Cast<Enum>().ToList();
                case EEstrategiaDeDeteccionDeDaño.FuenteDelDaño:
                {
                    SistemaPrincipal.LoggerGlobal.Log($"el valor de {nameof(estrategiaDeDeteccion)} no puede ser {EEstrategiaDeDeteccionDeDaño.FuenteDelDaño}", ESeveridad.Error);

                    return new List<Enum>();
                }
	        }

	        return new List<Enum>();
        }

        public static bool EsAceptarOFinalizar(this EResultadoViewModel resultado) => resultado is EResultadoViewModel.Aceptar or EResultadoViewModel.Finalizar;
    }
}
