using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloParticipante : ModeloBase
    {
        public ControladorParticipante controladorParticipante;

        public int TiradaIniciativa { get; set; }
        public bool EsSuTurno { get; set; }

        //Personaje participante
        public virtual TIParticipantePersonaje Personaje { get; set; }

        //Acciones realizadas por el participante
        public virtual List<TIParticipanteAccion> AccionesRealizadas { get; set; } = new List<TIParticipanteAccion>();

        public virtual TIAdministradorDeCombateParticipante Combate { get; set; }
    }
}