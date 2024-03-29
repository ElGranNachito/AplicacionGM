﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de un slot. Se trata de un espacio perteciente a algun <see cref="ModeloItem"/> donde se pueden almacenar 
    /// objetos
    /// </summary>
    public partial class ModeloSlot : ModeloBase
    {
	    public override bool EsValido { get; set; } = true;

        /// <summary>
        /// Nombre del slot
        /// </summary>
        [StringLength(100)]
        public string NombreSlot { get; set; }

        /// <summary>
        /// Espacio total que ofrece el slot
        /// </summary>
        public decimal EspacioTotal      { get; set; }

        /// <summary>
        /// Personaje que posee este slot
        /// </summary>
        [BackingField(nameof(mPersonajeContenedor))]
        [CopiarSuperficialmente]
        public virtual ModeloPersonaje PersonajeContenedor
        {
	        get
	        {
		        if (mPersonajeContenedor != null)
			        return mPersonajeContenedor;

		        return ParteDelCuerpoDueña?.PersonajeContenedor ?? ItemDueño?.PersonajePortador;
	        }
	        set => mPersonajeContenedor = value;
        }

        /// <summary>
        /// Parte del cuerpo almacenada en este slot
        /// </summary>
        public virtual ModeloParteDelCuerpo ParteDelCuerpoAlmacenada { get; set; }

        /// <summary>
        /// Parte del cuerpo a la que pertenece el slot
        /// </summary>
        [CopiarSuperficialmente]
        public virtual ModeloParteDelCuerpo ParteDelCuerpoDueña { get; set; }

        /// <summary>
        /// Item al que pertenece este slot
        /// </summary>
        [CopiarSuperficialmente]
        public virtual ModeloItem ItemDueño { get; set; }

        /// <summary>
        /// Items actualmente almacenados en el slot
        /// </summary>
        [CopiarSuperficialmente]
        public virtual List<ModeloItem> ItemsAlmacenados { get; set; } = new List<ModeloItem>();

        /// <summary>
        /// Historial del daño recibido por este slot
        /// </summary>
        [CopiarSuperficialmente]
        public virtual List<ModeloDañable> HistorialDañoRecibido { get; set; } = new List<ModeloDañable>();
    }
}
