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
                case ETipoItem.ArmaDistancia:
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

        public static bool EsAceptarOFinalizar(this EResultadoViewModel resultado) => resultado is EResultadoViewModel.Aceptar or EResultadoViewModel.Finalizar;
    }
}
