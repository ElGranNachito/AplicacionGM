using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloParticipante : ModeloBase
    {
        public int TiradaIniciativa { get; set; }
        public bool EsSuTurno { get; set; }

        //Personaje participante
        public TIParticipantePersonaje Personaje { get; set; }

        //Posicion del participante en el mapa
        public ModeloVector2 PosicionCombate { get; set; }

        //Acciones realizadas por el participante
        public List<TIParticipanteAccion> AccionesRealizadas { get; set; } = new List<TIParticipanteAccion>();

        public TIAdministradorDeCombateParticipante Combate { get; set; }
    }
}