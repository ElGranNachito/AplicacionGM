﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje : ModeloConVariablesYTiradas
    {
        /// <summary>
        /// Relacion rol
        /// </summary>
        public virtual ModeloRol Rol { get; set; }

        /// <summary>
        /// Nombre del personaje
        /// </summary>
        [StringLength(50)]
        [MaxLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Puede ser un personaje Master, Servant, Invocacion, o NPC
        /// </summary>
        public ETipoPersonaje TipoPersonaje { get; set; }

        /// <summary>
        /// Numero de party (equipo) a la que el personaje pertenece.
        /// </summary>
        public ENumeroParty NumeroParty { get; set; }

        //Stats del personaje
        public int MaxHp { get; set; }
        public int Hp    { get; set; }
        public int Str   { get; set; }
        public int End   { get; set; }
        public int Agi   { get; set; }
        public int Int   { get; set; }
        public int Lck   { get; set; }
               
        //Ventajas stats del personaje
        public int VentajaStr   { get; set; }
        public int VentajaEnd   { get; set; }
        public int VentajaAgi   { get; set; }
        public int VentajaInt   { get; set; }
        public int VentajaLck   { get; set; }

        /// <summary>
        /// Maxima cantidad de pesos que puede cargar el personaje
        /// </summary>
        public decimal PesoMaximoCargable   { get; set; }
        /// <summary>
        /// Cantidad de peso siendo cargado actualmente
        /// </summary>
        public decimal PesoCargado { get; set; }

        /// <summary>
        /// Ruta relativa a la imagen del personaje
        /// </summary>
        [Column(TypeName = "varchar(260)")]
        public string PathImagenRelativo { get; set; }

        /// <summary>
        /// Ruta completa a la imagen del personaje
        /// </summary>
        [Column(TypeName = "varchar(260)")]
        public string PathImagenAbsoluto { get; set; }

        /// <summary>
        /// Estado del personaje (en combate o no en combate)
        /// </summary>
        public bool EstaEnCombate { get; set; }

        /// <summary>
        /// Formato de la imagen asignada al personaje
        /// </summary>
        public EFormatoImagen FormatoImagen { get; set; }

        /// <summary>
        /// Posicion del personaje en el mapa
        /// </summary>
        public virtual ModeloVector2 Posicion { get; set; }

        /// <summary>
        /// Efectos aplicados sobre el personaje
        /// </summary>
        public virtual List<ModeloEfectoSiendoAplicado> EfectosAplicandose { get; set; } = new List<ModeloEfectoSiendoAplicado>();
        
        //Equipamiento del personaje
        public virtual List<ModeloUtilizable> Inventario { get; set; } = new List<ModeloUtilizable>();
        public virtual List<ModeloDefensivo> Armadura { get; set; } = new List<ModeloDefensivo>();
        public virtual List<ModeloArmasDistancia> ArmasDistancia  { get; set; } = new List<ModeloArmasDistancia>();

        /// <summary>
        /// Contratos realizados con tros personajes
        /// </summary>
        public virtual List<ModeloContrato> Contratos  { get; set; } = new List<ModeloContrato>();

        /// <summary>
        /// Alianzas con otros personajes
        /// </summary>
        public virtual List<ModeloAlianza> Alianzas   { get; set; } = new List<ModeloAlianza>();

        /// <summary>
        /// Habilidades de este personaje. Incluye todos los tipos de habilidad
        /// </summary>
        public virtual List<ModeloHabilidad> Habilidades { get; set; } = new List<ModeloHabilidad>();
                                              
        /// <summary>
        /// Pasivas
        /// </summary>
        public virtual List<ModeloPerk> Perks { get; set; } = new List<ModeloPerk>();

        /// <summary>
        /// Habilidades
        /// </summary>
        public virtual List<ModeloHabilidad> Skills { get; set; } = new List<ModeloHabilidad>();

        /// <summary>
        /// Magias
        /// </summary>
        public virtual List<ModeloMagia> Magias { get; set; } = new List<ModeloMagia>();

        /// <summary>
        /// Es participante en algun combate.
        /// TODO: Solo puede participar de un combate activo a la vez.
        /// </summary>
        public virtual List<ModeloParticipante> ParticipacionEnCombates { get; set; } = new List<ModeloParticipante>();

        /// <summary>
        /// Especialidades de este personaje
        /// </summary>
        public virtual List<ModeloEspecialidad> Especialidades { get; set; } = new List<ModeloEspecialidad>();

        /// <summary>
        /// Unidades que representan a este personaje
        /// </summary>
        public virtual List<ModeloUnidadMapa> UnidadesEnMapa { get; set; } = new List<ModeloUnidadMapa>();

        /// <summary>
        /// Invocaciones creadas por este personaje
        /// </summary>
        public virtual List<ModeloInvocacion> Invocaciones { get; set; } = new List<ModeloInvocacion>();
    }
}