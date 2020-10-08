using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje
    {
        //ID
        [Key]
        public int IdPersonaje { get; set; }

        //Nombre del personaje
        [StringLength(50)]
        public string Nombre { get; set; }

        //Stats del personaje
        public short Hp { get; set; }
        public ushort Str { get; set; }
        public ushort End { get; set; }
        public ushort Agi { get; set; }
        public ushort Intel { get; set; }
        public ushort Lck { get; set; }

        //Estado del personaje (en combate o no en combate)
        public bool EstaEnCombate { get; set; }

        //Posicion del personaje en el mapa
        public ModeloVector2 Posicion { get; set; }

        public List<TIPersonajeEfecto> Efectos { get; set; }
        public List<TIPersonajeUtilizable> Inventario { get; set; }
        public List<TIPersonajeDefensivo> Armadura { get; set; }
        public List<TIPersonajePersonaje> Aliados { get; set; }
        public List<TIPersonajePerk> Perks { get; set; }
        public List<TIPersonajeHabilidad> Skills { get; set; }
        public List<TIPersonajeMagia> Magias { get; set; }
        public List<TIPersonajeModificadorDeDefensa> ModificadoresDeDefensa { get; set; }
    }
}
