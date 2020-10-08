﻿namespace AppGM.Core
{
    public abstract class TIParticipante
    {
        public int IdParticipante { get; set; }
        public ModeloParticipante Participante { get; set; }
    }

    public class TIParticipantePersonaje : TIParticipante
    {
        public int IdPersonaje { get; set; }
        public ModeloPersonaje Personaje { get; set; }
    }

    public class TIParticipanteAccion : TIParticipante
    {
        public int IdAccion { get; set; }
        public ModeloAccion Accion { get; set; }
    }

    public class TIAdministradorDeCombateParticipante
    {
        public int IdAdministradorDeCombate { get; set; }
        public ModeloAdministradorDeCombate AdministradorDeCombate { get; set; }

        public int IdParticipante { get; set; }
        public ModeloParticipante Participante { get; set; }
    }

    public class TIModeloAdministradorDeCombateMapa
    {
        public int IdAdministradorDeCombate { get; set; }
        public ModeloAdministradorDeCombate AdministradorDeCombate { get; set; }

        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }
    }

}
