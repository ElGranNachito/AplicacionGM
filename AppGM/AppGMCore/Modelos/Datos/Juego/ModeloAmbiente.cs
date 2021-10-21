using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el ambiente
    /// </summary>
    public class ModeloAmbiente : ModeloBase
    {   
        /// <summary>
        /// Define que tipo de ambiente es y que consecuencias puede tener sobre los personajes, habilidades, efectos, utilizables, etc.
        /// </summary>
        public ECaracteristicasAmbiente CaracteristicasAmbiente { get; set; }

        /// <summary>
        /// Total de casillas disponibles en este ambiente.
        /// </summary>
        public int CantidadCasillas { get; set; } = Constantes.CasillasPorDefecto;

        /// <summary>
        /// Temperatura exacta en grados centigrados que hace dentro del ambiente.
        /// </summary>
        public float TemperaturaActual { get; set; } = Constantes.TemperaturaPorDefecto;

        /// <summary>
        /// Humedad relativa en el aire expresada en porcentaje.
        /// </summary>
        public float HumedadActual { get; set; } = Constantes.HumedadPorDefecto;

        /// <summary>
        /// Clave foranea que referencia al <see cref="ModeloMapa"/> de este ambiente
        /// </summary>
        [ForeignKey(nameof(MapaDelAmbiente))]
        public int IdMapa { get; set; }

        /// <summary>
        /// Mapa en el que se encuentra el ambiente.
        /// </summary>
        public virtual ModeloMapa MapaDelAmbiente { get; set; }

        /// <summary>
        /// Clave foranea que referencia al <see cref="ModeloRol"/> al que pertenece este ambiente
        /// </summary>
        [ForeignKey(nameof(RolAlQuePertenece))]
        public int? IdRol { get; set; }

        /// <summary>
        /// Rol al que pertenece este ambiente
        /// </summary>
        public virtual ModeloRol RolAlQuePertenece { get; set; }

        /// <summary>
        /// Clave foranea que referencia al <see cref="ModeloAdministradorDeCombate"/> al que pertenece este ambiente
        /// </summary>
        [ForeignKey(nameof(CombateAlQuePertenece))]
        public int? IdCombateAlQuePertenece { get; set; }

        /// <summary>
        /// Combate al que pertenece este ambiente
        /// </summary>
        public virtual ModeloAdministradorDeCombate CombateAlQuePertenece { get; set; }
    }
}
    