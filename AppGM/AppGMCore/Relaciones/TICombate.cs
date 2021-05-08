using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion con <see cref="ModeloParticipante"/>
    /// </summary>
    public abstract class TIParticipante : ModeloBaseSK
    {
        [ForeignKey(nameof(Participante))]
        public int IdParticipante { get; set; }
        public virtual ModeloParticipante Participante { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloParticipante"/> con el <see cref="ModeloPersonaje"/> que representa
    /// </summary>
    public class TIParticipantePersonaje : TIParticipante
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        public virtual ModeloPersonaje Personaje { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloParticipante"/> con una <see cref="ModeloAccion"/>
    /// </summary>
    public class TIParticipanteAccion : TIParticipante
    {
        [ForeignKey(nameof(Accion))]
        public int IdAccion { get; set; }
        public ModeloAccion Accion { get; set; }
    }

    /// <summary>
    /// Representa una relacion con <see cref="ModeloAdministradorDeCombate"/>
    /// </summary>
    public class TIAdministradorDeCombate : ModeloBaseSK
    { 
        [ForeignKey(nameof(AdministradorDeCombate))]
        public int IdAdministradorDeCombate { get; set; }
        public ModeloAdministradorDeCombate AdministradorDeCombate { get; set; }
    }

    /// <summary>
    /// Representa una relacion entre un <see cref="ModeloPersonaje"/> con un <see cref="ModeloAdministradorDeCombate"/>
    /// </summary>
    public class TIAdministradorDeCombateParticipante : TIAdministradorDeCombate
    {
        [ForeignKey(nameof(Participante))]
        public int IdParticipante { get; set; }
        public ModeloParticipante Participante { get; set; }
    }

    /// <summary>
    /// Representa una relacion entre un <see cref="ModeloAdministradorDeCombate"/> con un <see cref="ModeloMapa"/>
    /// </summary>
    public class TIAdministradorDeCombateMapa : TIAdministradorDeCombate
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }
    }
}