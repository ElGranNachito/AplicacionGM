
using System;

namespace AppGM.Core
{
    public static class Constantes
    {
	    /// <summary>
        /// Valor constante para la propiedad CaracteristicasAmbiente de un <see cref="ModeloAmbiente"/>
        /// </summary>
        public const ECaracteristicasAmbiente CaracteristicasAmbiente = ECaracteristicasAmbiente.NINGUNO;

        /// <summary>
        /// Temperatura por defecto que tienen los ambientes (valor imposible ya que es en celsius)
        /// </summary>
        public const float TemperaturaPorDefecto = -300.0f;

        /// <summary>
        /// Humedad por defecto que tienen los ambientes (valor imposible)
        /// </summary>
        public const float HumedadPorDefecto = -1;

        /// <summary>
        /// Cantidad de casillas por defecto que tiene un ambiente (valor imposible)
        /// </summary>
        public const int CasillasPorDefecto = -1;

        /// <summary>
        /// Bono
        /// </summary>
        public const int BonoEspecialidad = 2;

        /// <summary>
        /// Valor que se le asigna a los <see cref="ResultadoTirada"/> cuando son invalidos
        /// </summary>
        public const int ResultadoTiradaInvalido = int.MinValue;

        /// <summary>
        /// Espacio por defecto que tiene un slot al crearse
        /// </summary>
        public const decimal EspacioPorDefectoNuevoSlot = 1;
    }
}
