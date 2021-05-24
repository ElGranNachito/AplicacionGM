﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion con <see cref="ModeloPersonaje"/>
    /// </summary>
    public abstract class TIPersonaje : ModeloBaseSK
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        
        public ModeloPersonaje Personaje { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con el <see cref="ModeloMapa"/> donde se encuentre
    /// </summary>
    public class TIPersonajeUnidadMapa : TIPersonaje
    {
        [ForeignKey(nameof(Unidad))]
        public int IdUnidadMapa { get; set; }
        public ModeloUnidadMapa Unidad { get; set; }
    }

    public class TIPersonajeContrato : TIPersonaje
    {
        [ForeignKey(nameof(Contrato))]
        public int IdContrato { get; set; }
        public ModeloContrato Contrato { get; set; }
    }

    public class TIPersonajeAlianza : TIPersonaje
    {
        [ForeignKey(nameof(Alianza))]
        public int IdAlianza { get; set; }
        public ModeloAlianza Alianza { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con el <see cref="ModeloEfecto"/> que tiene
    /// </summary>
    public class TIPersonajeEfecto : TIPersonaje
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con un <see cref="ModeloUtilizable"/ que tenga equipado>
    /// </summary>
    public class TIPersonajeUtilizable : TIPersonaje
    {
        [ForeignKey(nameof(Utilizable))]
        public int IdUtilizable { get; set; }
        public ModeloUtilizable Utilizable { get; set; }

    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con un <see cref="ModeloDefensivo"/ que tenga equipado>
    /// </summary>
    public class TIPersonajeDefensivo : TIPersonaje
    {
        [ForeignKey(nameof(Defensivo))]
        public int IdDefensivo { get; set; }
        public ModeloDefensivo Defensivo { get; set; }

    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con un <see cref="ModeloOfensivo"/ que tenga equipado>
    /// </summary>
    public class TIPersonajeOfensivo : TIPersonaje
    {
        [ForeignKey(nameof(PortableOfensivo))]
        public int IdOfensivo { get; set; }
        public ModeloOfensivo PortableOfensivo { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con un <see cref="ModeloArmasDistancia"/ que tenga equipada>
    /// </summary>
    public class TIPersonajeArmaDistancia : TIPersonaje
    {
        [ForeignKey(nameof(ArmaDistancia))]
        public int IdArmaDistancia { get; set; }
        public ModeloArmasDistancia ArmaDistancia { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con otro <see cref="ModeloPersonaje"/ que sea su aliado>
    /// TODO: Eliminar si no muestra ser de uso en el futuro
    /// </summary>
    public class TIPersonajePersonaje : TIPersonaje
    {
        [ForeignKey(nameof(Aliado))]
        public int IdAliado { get; set; }
        public ModeloPersonaje Aliado { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con una <see cref="ModeloHabilidad"/ que posea>
    /// </summary>
    public class TIPersonajeHabilidad : TIPersonaje
    {
        [ForeignKey(nameof(Habilidad))]
        public int IdHabilidad { get; set; }
        public ModeloHabilidad Habilidad { get; set; }

    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con una <see cref="ModeloPerk"/ que posea>
    /// </summary>
    public class TIPersonajePerk : TIPersonaje
    {
        [ForeignKey(nameof(Perk))]
        public int IdPerk { get; set; }
        public ModeloPerk Perk { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con una <see cref="ModeloMagia"/ que posea>
    /// </summary>
    public class TIPersonajeMagia : TIPersonaje
    {
        [ForeignKey(nameof(Magia))]
        public int IdMagia { get; set; }
        public ModeloMagia Magia { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonaje"/> con un <see cref="ModeloModificadorDeDefensa"/ que tenga>
    /// </summary>
    public class TIPersonajeModificadorDeDefensa : TIPersonaje
    {
        [ForeignKey(nameof(ModificadorDeDefensa))]
        public int IdModificadorDefensa { get; set; }
        public ModeloModificadorDeDefensa ModificadorDeDefensa { get; set; }
    }

    // Personaje servant:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloServant"/> con un <see cref="ModeloNoblePhantasm"/ que posea>
    /// </summary>
    public class TIServantNoblePhantasm : ModeloBaseSK
    {
        [ForeignKey(nameof(Servant))]
        public int IdServant { get; set; }
        public ModeloServant Servant { get; set; }

        [ForeignKey(nameof(NoblePhantasm))]
        public int IdNoblePhantasm { get; set; }
        public ModeloNoblePhantasm NoblePhantasm { get; set; }
    }

    // Personaje invocacion:

    /// <summary>
    /// Representa una relacion con <see cref="ModeloInvocacion"/>
    /// </summary>
    public abstract class TIInvocacion : ModeloBaseSK
    {
        [ForeignKey(nameof(Invocacion))]
        public int IdInvocacion { get; set; }
        public ModeloInvocacion Invocacion { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloInvocacion"/> con el <see cref="ModeloPersonaje"/> que la haya invocado
    /// </summary>
    public class TIInvocacionPersonaje : TIInvocacion
    {
        [ForeignKey(nameof(PersonajeInvocador))]
        public int IdPersonaje { get; set; }
        public ModeloPersonaje PersonajeInvocador { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloInvocacion"/> con el <see cref="ModeloDatosInvocacionBase"/> que tenga
    /// </summary>
    public class TIInvocacionDatosInvocacion : TIInvocacion
    {
        [ForeignKey(nameof(DatosInvocacion))]
        public int IdDatos { get; set; }
        public ModeloDatosInvocacionBase DatosInvocacion { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloInvocacion"/> con el <see cref="ModeloEfecto"/> que aplique
    /// </summary>
    public class TIInvocacionEfecto : TIInvocacion
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    // Personaje jugable:

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonajeJugable"/>
    /// </summary>
    public abstract class TIPersonajeJugable : ModeloBaseSK
    {
        [ForeignKey(nameof(PersonajeJugable))]
        public int IdPersonajeJugable { get; set; }
        public ModeloPersonajeJugable PersonajeJugable { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonajeJugable"/> con el <see cref="ModeloCaracteristicas"/> que posea
    /// </summary>
    public class TIPersonajeJugableCaracteristicas : TIPersonajeJugable
    {
        [ForeignKey(nameof(Caracteristicas))]
        public int IdCaracteristica { get; set; }
        public ModeloCaracteristicas Caracteristicas { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPersonajeJugable"/> con el <see cref="ModeloInvocacion"/> que tenga activa
    /// </summary>
    public class TIPersonajeJugableInvocacion : TIPersonajeJugable
    {
        [ForeignKey(nameof(Invocacion))]
        public int IdInvocacion { get; set; }
        public ModeloInvocacion Invocacion { get; set; }
    }
}
