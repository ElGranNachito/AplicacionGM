﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de una tirada que no especifica su tipo
    /// </summary>
    public partial class ModeloTiradaBase : ModeloBase
    {
        /// <summary>
        /// Nombre de la tirada
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion de la tirada
        /// </summary>
        [MaxLength(500)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Cadena que representa la tirada
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Tirada { get; set; }

        /// <summary>
        /// Descripcion de la variable extra que requiere la tirada
        /// </summary>
        [MaxLength(512)]
        public string DescripcionVariableExtra { get; set; }

        /// <summary>
        /// Indica si esta tirada es para alguna accion en la que se tenga especialidad
        /// </summary>
        public int MultiplicadorDeEspecialidad { get; set; }

        /// <summary>
        /// Tipo de esta tirada
        /// </summary>
        public ETipoTirada TipoTirada { get; set; }

	    /// <summary>
        /// Modelo del personaje que contiene la tirada
        /// </summary>
	    public virtual ModeloPersonaje PersonajeContenedor { get; set; }

        /// <summary>
        /// Modelo de la habilidad que contiene la tirada
        /// </summary>
        public virtual ModeloHabilidad HabilidadContenedora { get; set; }

        /// <summary>
        /// Modelo del utilizable que contiene la tirada
        /// </summary>
        public virtual ModeloItem ItemContenedor { get; set; }

        /// <summary>
        /// Modelo del la funcion que contiene la tirada
        /// </summary>
        public virtual ModeloFuncion FuncionContenedora { get; set; }

        /// <summary>
        /// Historial de lanzamientos de esta tirada
        /// </summary>
        [NoCopiar]
        public virtual List<ModeloHistorialTirada> Historial { get; set; } = new List<ModeloHistorialTirada>();
    }

    /// <summary>
    /// Tirada de daño
    /// </summary>
    public class ModeloTiradaDeDaño : ModeloTiradaBase
    {
	    /// <summary>
	    /// Stat a tener en cuenta durante la tirada
	    /// </summary>
	    public EStat StatDeLaQueDepende { get; set; }

        /// <summary>
        /// Nivel de la magia (en caso de que sea una tirada de daño para una magia)
        /// </summary>
	    public ENivelMagia NivelMagia { get; set; }

        /// <summary>
        /// Rango del daño
        /// </summary>
	    public ERango Rango { get; set; }

        /// <summary>
        /// Tipo de daño que aplica la tirada
        /// </summary>
        public ETipoDeDaño TipoDeDaño { get; set; }

	    /// <summary>
	    /// Fuentes de daño abarcadas por esta tirada
	    /// </summary>
	    public virtual List<ModeloFuenteDeDaño> FuentesDeDañoAbarcadas { get; set; } = new List<ModeloFuenteDeDaño>();
    }
}