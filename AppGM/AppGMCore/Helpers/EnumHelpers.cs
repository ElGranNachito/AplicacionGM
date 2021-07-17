﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
	        var operacionesLogicasDisponibles = Enum.GetValues(typeof(EOperacionLogica)).Cast<EOperacionLogica>().ToList();

	        switch (tipo)
	        {
                case Type t when t == typeof(bool):

	                operacionesLogicasDisponibles.RemoverRango(new[]
	                {
		                EOperacionLogica.Igual, EOperacionLogica.NoIgual,
		                EOperacionLogica.Mayor, EOperacionLogica.Menor,
		                EOperacionLogica.MayorIgual, EOperacionLogica.MenorIgual
	                });

	                break;

                case Type t when !t.IsByRef && (
                    t == typeof(int)   || t == typeof(uint)   ||
                    t == typeof(short) || t == typeof(ushort) ||
                    t == typeof(byte)  || t == typeof(sbyte)  ||
                    t == typeof(long)  || t == typeof(ulong)  ||
                    t == typeof(float) || t == typeof(double)):

	                operacionesLogicasDisponibles.RemoverRango(new[] {EOperacionLogica.O, EOperacionLogica.Y, EOperacionLogica.No});

	                break;

                case Type t when t.IsByRef:

	                operacionesLogicasDisponibles.RemoverRango(new[]
	                {
		                EOperacionLogica.O, EOperacionLogica.Y,
		                EOperacionLogica.No, EOperacionLogica.Mayor,
		                EOperacionLogica.Menor, EOperacionLogica.MayorIgual,
		                EOperacionLogica.MenorIgual
	                });

	                break;

	        }

	        return operacionesLogicasDisponibles;
        }
    }
}
