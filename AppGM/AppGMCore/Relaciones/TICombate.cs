using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public abstract class TIParticipante : ModeloBaseSK
    {
        [ForeignKey(nameof(Participante))]
        public int IdParticipante { get; set; }
        public virtual ModeloParticipante Participante { get; set; }
    }

    public class TIParticipantePersonaje : TIParticipante
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        public virtual ModeloPersonaje Personaje { get; set; }
    }

    public class TIParticipanteAccion : TIParticipante
    {
        [ForeignKey(nameof(Accion))]
        public int IdAccion { get; set; }
        public ModeloAccion Accion { get; set; }
    }

    public class TIAdministradorDeCombate : ModeloBaseSK
    { 
        [ForeignKey(nameof(AdministradorDeCombate))]
        public int IdAdministradorDeCombate { get; set; }
        public ModeloAdministradorDeCombate AdministradorDeCombate { get; set; }
    }

    public class TIAdministradorDeCombateParticipante : TIAdministradorDeCombate
    {
        [ForeignKey(nameof(Participante))]
        public int IdParticipante { get; set; }
        public ModeloParticipante Participante { get; set; }
    }

    public class TIAdministradorDeCombateMapa : TIAdministradorDeCombate
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }
    }

}
