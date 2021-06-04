﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje : ModeloBase
    {
        public ControladorPersonaje controlador;

        /// <summary>
        /// Relacion rol
        /// </summary>
        public TIRolPersonaje RolPersonaje { get; set; }

        /// <summary>
        /// Nombre del personaje
        /// </summary>
        [StringLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Puede ser un personaje Master, Servant, Invocacion, o NPC
        /// </summary>
        public ETipoPersonaje TipoPersonaje { get; set; }

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
        /// Imagencita del personaje
        /// </summary>
        public string PathImagen { get; set; }

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
        public ModeloVector2 Posicion { get; set; }

        /// <summary>
        /// Efectos aplicados sobre el personaje
        /// </summary>
        public List<TIPersonajeEfectoSiendoAplicado> EfectosAplicandose { get; set; } = new List<TIPersonajeEfectoSiendoAplicado>();
        
        //Equipamiento del personaje
        public List<TIPersonajeUtilizable>           Inventario { get; set; } = new List<TIPersonajeUtilizable>();
        public List<TIPersonajeDefensivo>            Armadura   { get; set; } = new List<TIPersonajeDefensivo>();
        public List<TIPersonajeArmaDistancia>        ArmasDistancia         { get; set; } = new List<TIPersonajeArmaDistancia>();

        /// <summary>
        /// Contratos realizados con tros personajes
        /// </summary>
        public List<TIPersonajeContrato>      Contratos  { get; set; } = new List<TIPersonajeContrato>();
        /// <summary>
        /// Alianzas con otros personajes
        /// </summary>
        public List<TIPersonajeAlianza>       Alianzas   { get; set; } = new List<TIPersonajeAlianza>();
                                              
        //Habilidades del personaje
        public List<TIPersonajePerk>          Perks      { get; set; } = new List<TIPersonajePerk>();
        public List<TIPersonajeHabilidad>     Skills     { get; set; } = new List<TIPersonajeHabilidad>();
        public List<TIPersonajeMagia>         Magias     { get; set; } = new List<TIPersonajeMagia>();
        
        /// <summary>
        /// Modificadores de defensa del personaje
        /// </summary>
        public List<TIPersonajeModificadorDeDefensa> ModificadoresDeDefensa { get; set; } = new List<TIPersonajeModificadorDeDefensa>();
        
        /// <summary>
        /// Es participante en algun combate:
        /// </summary>
        public List<TIParticipantePersonaje> ParticipacionEnCombates { get; set; } = new List<TIParticipantePersonaje>();

    }
}
