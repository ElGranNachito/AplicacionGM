using System;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
    public static class EnumHelpers
    {
        public static List<ERango>              RangosDisponibles               => Enum.GetValues(typeof(ERango)).Cast<ERango>().ToList();
        public static List<ECondicionClimatica> CondicionesClimaticas           => Enum.GetValues(typeof(ECondicionClimatica)).Cast<ECondicionClimatica>().ToList();
        public static List<ETemporada>          TemporadaDelAño                 => Enum.GetValues(typeof(ETemporada)).Cast<ETemporada>().ToList();
        public static List<EArquetipo>          ArquetiposDisponibles           => Enum.GetValues(typeof(EArquetipo)).Cast<EArquetipo>().ToList();
        public static List<EBienestar>          BienestarPersonaje              => Enum.GetValues(typeof(EBienestar)).Cast<EBienestar>().ToList();
        public static List<EClaseServant>       ClasesDisponibles               => Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();
        public static List<ETipoPersonaje>      TiposDePersonajesDisponibles    => Enum.GetValues(typeof(ETipoPersonaje)).Cast<ETipoPersonaje>().ToList();
        public static List<EManoDominante>      TiposDeManoDominanteDisponibles => Enum.GetValues(typeof(EManoDominante)).Cast<EManoDominante>().ToList();
        public static List<EParteDelCuerpo>     PartesDelCuerpoDisponibles      => Enum.GetValues(typeof(EParteDelCuerpo)).Cast<EParteDelCuerpo>().ToList();
        public static List<ESexo>               SexosDisponibles                => Enum.GetValues(typeof(ESexo)).Cast<ESexo>().ToList();
        public static List<EStat>               StatsDisponibles                => Enum.GetValues(typeof(EStat)).Cast<EStat>().ToList();
        public static List<ETipoDeDaño>         TiposDeDañoDisponibles          => Enum.GetValues(typeof(ETipoDeDaño)).Cast<ETipoDeDaño>().ToList();
        public static List<EUsoDeHabilidad>     UsosDeHabilidadDisponibles      => Enum.GetValues(typeof(EUsoDeHabilidad)).Cast<EUsoDeHabilidad>().ToList();
        public static List<ETipoHabilidad>      TiposDeHabilidadDisponibles     => Enum.GetValues(typeof(ETipoHabilidad)).Cast<ETipoHabilidad>().ToList();

        public static int ObtenerModificador(this ERango rango)
        {
            //Hacemos este if pare prevenir una division por cero
            return rango == ERango.F ? 2 : 2 + (int)rango / 2;
        } 
    }
}
