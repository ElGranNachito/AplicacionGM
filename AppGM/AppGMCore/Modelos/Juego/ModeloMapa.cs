using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloMapa
    {
        [Key]
        public int Id { get; set; }
        public string NombreMapa { get; set; }
        public EFormatoImagen EFormatoImagen { get; set; }

        //Posiciones de todos los elementos del mapa. El orden debe ser el siguiente:
        //Iglesia -> Saber -> Lancer -> Assassin -> Rider -> Archer -> Caster -> Berserker
        public List<TIMapaVector2> PosicionesElementos { get; set; }
    }
}
