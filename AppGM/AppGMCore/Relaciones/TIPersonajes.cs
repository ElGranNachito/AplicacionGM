using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public abstract class TIPersonaje : ModeloBaseSK
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        
        public ModeloPersonaje Personaje { get; set; }
    }

    public class TIPersonajeEfecto : TIPersonaje
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    public class TIPersonajeUtilizable : TIPersonaje
    {
        [ForeignKey(nameof(Utilizable))]
        public int IdUtilizable { get; set; }
        public ModeloUtilizable Utilizable { get; set; }

    }

    public class TIPersonajeDefensivo : TIPersonaje
    {
        [ForeignKey(nameof(Defensivo))]
        public int IdDefensivo { get; set; }
        public ModeloDefensivo Defensivo { get; set; }

    }

    public class TIPersonajeOfensivo : TIPersonaje
    {
        [ForeignKey(nameof(PortableOfensivo))]
        public int IdOfensivo { get; set; }
        public ModeloOfensivo PortableOfensivo { get; set; }
    }

    public class TIPersonajeArmaDistancia : TIPersonaje
    {
        [ForeignKey(nameof(ArmaDistancia))]
        public int IdArmaDistancia { get; set; }
        public ModeloArmasDistancia ArmaDistancia { get; set; }
    }

    public class TIPersonajePersonaje : TIPersonaje
    {
        [ForeignKey(nameof(Aliado))]
        public int IdAliado { get; set; }
        public ModeloPersonaje Aliado { get; set; }
    }

    public class TIPersonajeHabilidad : TIPersonaje
    {
        [ForeignKey(nameof(Habilidad))]
        public int IdHabilidad { get; set; }
        public ModeloHabilidad Habilidad { get; set; }

    }

    public class TIPersonajePerk : TIPersonaje
    {
        [ForeignKey(nameof(Perk))]
        public int IdPerk { get; set; }
        public ModeloPerk Perk { get; set; }
    }

    public class TIPersonajeMagia : TIPersonaje
    {
        [ForeignKey(nameof(Magia))]
        public int IdMagia { get; set; }
        public ModeloMagia Magia { get; set; }
    }

    public class TIPersonajeModificadorDeDefensa : TIPersonaje
    {
        [ForeignKey(nameof(ModificadorDeDefensa))]
        public int IdModificadorDefensa { get; set; }
        public ModeloModificadorDeDefensa ModificadorDeDefensa { get; set; }
    }

    // Personaje servant
    public class TIServantNoblePhantasm : ModeloBaseSK
    {
        [ForeignKey(nameof(Servant))]
        public int IdServant { get; set; }
        public ModeloServant Servant { get; set; }

        [ForeignKey(nameof(NoblePhantasm))]
        public int IdNoblePhantasm { get; set; }
        public ModeloNoblePhantasm NoblePhantasm { get; set; }
    }

    // Personaje invocacion
    public abstract class TIInvocacion : ModeloBaseSK
    {
        [ForeignKey(nameof(Invocacion))]
        public int IdInvocacion { get; set; }
        public ModeloInvocacion Invocacion { get; set; }
    }

    public class TIInvocacionPersonaje : TIInvocacion
    {
        [ForeignKey(nameof(PersonajeInvocador))]
        public int IdPersonaje { get; set; }
        public ModeloPersonaje PersonajeInvocador { get; set; }
    }

    public class TIInvocacionDatosInvocacion : TIInvocacion
    {
        [ForeignKey(nameof(DatosInvocacion))]
        public int IdDatos { get; set; }
        public ModeloDatosInvocacionBase DatosInvocacion { get; set; }
    }

    public class TIInvocacionEfecto : TIInvocacion
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Personaje jugable
    public abstract class TIPersonajeJugable : ModeloBaseSK
    {
        [ForeignKey(nameof(PersonajeJugable))]
        public int IdPersonajeJugable { get; set; }
        public ModeloPersonajeJugable PersonajeJugable { get; set; }
    }

    public class TIPersonajeJugableCaracteristicas : TIPersonajeJugable
    {
        [ForeignKey(nameof(Caracteristicas))]
        public int IdCaracteristica { get; set; }
        public ModeloCaracteristicas Caracteristicas { get; set; }
    }

    public class TIPersonajeJugableInvocacion : TIPersonajeJugable
    {
        [ForeignKey(nameof(Invocacion))]
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
