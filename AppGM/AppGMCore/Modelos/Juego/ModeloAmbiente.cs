using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el ambiente
    /// </summary>
    public class ModeloAmbiente : ModeloBase
    {
        public ControladorAmbiente controladorAmbiente;

        /// <summary>
        /// Define que tipo de ambiente es y que consecuencias puede tener sobre los personajes, habilidades, efectos, utilizables, etc.
        /// </summary>
        public ECaracteristicasAmbiente CaracteristicasAmbiente { get; set; }

        /// <summary>
        /// Total de casillas disponibles en este ambiente.
        /// </summary>
        public int CantidadCasillas { get; set; }

        /// <summary>
        /// Temperatura exacta en grados centigrados que hace dentro del ambiente.
        /// </summary>
        public int TemperaturaC { get; set; }
        /// <summary>
        /// Humedad relativa en el aire expresada en porcentaje.
        /// </summary>
        public int Humedad      { get; set; }

        /// <summary>
        /// Mapa en el que se encuentra el ambiente.
        /// </summary>
        public TIMapaAmbiente MapaDelAmbiente { get; set; }
    }
}
    