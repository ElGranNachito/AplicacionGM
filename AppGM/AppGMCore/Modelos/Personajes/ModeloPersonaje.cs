﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje : ModeloBase
    {
        //Nombre del personaje
        [StringLength(50)]
        public string Nombre { get; set; }

        //Stats del personaje
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public ushort Str { get; set; }
        public ushort End { get; set; }
        public ushort Agi { get; set; }
        public ushort Intel { get; set; }
        public ushort Lck { get; set; }

        //Estado del personaje (en combate o no en combate)
        public bool EstaEnCombate { get; set; }

        //Posicion del personaje en el mapa
        public ModeloVector2 Posicion { get; set; }

        public List<TIPersonajeEfecto>     Efectos    { get; set; } = new List<TIPersonajeEfecto>();
        public List<TIPersonajeUtilizable> Inventario { get; set; } = new List<TIPersonajeUtilizable>();
        public List<TIPersonajeDefensivo>  Armadura   { get; set; } = new List<TIPersonajeDefensivo>();
        public List<TIPersonajePersonaje>  Aliados    { get; set; } = new List<TIPersonajePersonaje>();
        public List<TIPersonajePerk>       Perks      { get; set; } = new List<TIPersonajePerk>();
        public List<TIPersonajeHabilidad>  Skills     { get; set; } = new List<TIPersonajeHabilidad>();
        public List<TIPersonajeMagia>      Magias     { get; set; } = new List<TIPersonajeMagia>();
        public List<TIPersonajeModificadorDeDefensa> ModificadoresDeDefensa { get; set; } = new List<TIPersonajeModificadorDeDefensa>();
        public List<TIPersonajeArmaDistancia>        ArmasDistancia         { get; set; } = new List<TIPersonajeArmaDistancia>();

        public ControladorPersonaje controlador;
    }
}
