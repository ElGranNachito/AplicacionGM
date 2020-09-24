using System.Collections.Generic;

namespace AppGMCore
{
    public class ModeloParticipante
    {
        public int TiradaIniciativa { get; set; }

        //Personaje participante
        public ModeloPersonaje Personaje { get; set; }
        private ControladorPersonaje<ModeloPersonaje> ControladorPersonaje { get; set; }

        //Posicion del participante en el mapa
        public Vector2D PosicionCombate { get; set; }

        //Acciones realizadas por el participante
        public List<ModeloAccion> AccionesRealizadas { get; set; }
    }
}