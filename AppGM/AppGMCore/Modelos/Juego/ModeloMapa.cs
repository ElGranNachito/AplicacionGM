﻿using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloMapa : ModeloBase
    {
        public string NombreMapa { get; set; }
        public EFormatoImagen EFormatoImagen { get; set; }

        //Posiciones de todos los elementos del mapa. El orden debe ser el siguiente:
        //Iglesia -> Saber -> Lancer -> Assassin -> Rider -> Archer -> Caster -> Berserker
        public List<TIMapaVector2>    PosicionesElementos { get; set; } = new List<TIMapaVector2>();

        public List<TIMapaUnidadMapa> PosicionesUnidades { get; set; }  = new List<TIMapaUnidadMapa>();
    }
}
