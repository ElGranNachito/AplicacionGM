using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public abstract class TIPersonaje
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        
        public ModeloPersonaje Personaje { get; set; }
    }

    public class TIPersonajeEfecto : TIPersonaje
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    public class TIPersonajeUtilizable : TIPersonaje
    {
        public int IdUtilizable { get; set; }
        public ModeloUtilizable Utilizable { get; set; }

    }

    public class TIPersonajeDefensivo : TIPersonaje
    {
        public int IdDefensivo { get; set; }
        public ModeloDefensivo Defensivo { get; set; }

    }

    public class TIPersonajeOfensivo : TIPersonaje
    {
        public int IdOfensivo { get; set; }
        public ModeloOfensivo PortableOfensivo { get; set; }
    }

    public class TIPersonajeArmaDistancia : TIPersonaje
    {
        public int IdArmaDistancia { get; set; }
        public ModeloArmasDistancia ArmaDistancia { get; set; }
    }

    public class TIPersonajePersonaje : TIPersonaje
    {
        public int IdAliado { get; set; }
        public ModeloPersonaje Aliado { get; set; }
    }

    public class TIPersonajeHabilidad : TIPersonaje
    {
        public int IdHabilidad { get; set; }
        public ModeloHabilidad Habilidad { get; set; }

    }

    public class TIPersonajePerk : TIPersonaje
    {
        public int IdPerk { get; set; }
        public ModeloPerk Perk { get; set; }
    }

    public class TIPersonajeMagia : TIPersonaje
    {
        public int IdMagia { get; set; }
        public ModeloMagia Magia { get; set; }
    }

    public class TIPersonajeModificadorDeDefensa : TIPersonaje
    {
        public int IdModificadorDefensa { get; set; }
        public ModeloModificadorDeDefensa ModificadorDeDefensa { get; set; }
    }

    // Personaje servant
    public class TIServantNoblePhantasm
    {
        public int IdServant { get; set; }
        public ModeloServant Servant { get; set; }

        public int IdNoblePhantasm { get; set; }
        public ModeloNoblePhantasm NoblePhantasm { get; set; }
    }

    // Personaje invocacion
    public abstract class TIInvocacion
    {
        public int IdInvocacion { get; set; }
        public ModeloInvocacion Invocacion { get; set; }
    }

    public class TIInvocacionPersonaje : TIInvocacion
    {
        public int IdPersonaje { get; set; }
        public ModeloPersonaje PersonajeInvocador { get; set; }
    }

    public class TIInvocacionEfecto : TIInvocacion
    {
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Personaje jugable
    public abstract class TIPersonajeJugable
    {
        public int IdPersonajeJugable { get; set; }
        public ModeloPersonajeJugable PersonajeJugable { get; set; }
    }

    public class TIPersonajeJugableCaracteristicas : TIPersonajeJugable
    {
        public int IdCaracteristica { get; set; }
        public ModeloCaracteristicas Caracteristicas { get; set; }
    }

    public class TIPersonajeJugableInvocacion : TIPersonajeJugable
    {
        public int IdInvocacion { get; set; }
        public ModeloInvocacion Invocacion { get; set; }
    }

    public class TIPersonajeUnidadMapa : TIPersonaje
    {
        [ForeignKey(nameof(Unidad))]
        public int IdUnidadMapa { get; set; }
        public ModeloUnidadMapa Unidad { get; set; }
    }

}
