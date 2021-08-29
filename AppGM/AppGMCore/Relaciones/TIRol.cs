using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion con <see cref="ModeloRol"/>
    /// </summary>
    public class TIRol : ModeloBaseSK
    {
        [ForeignKey(nameof(Rol))]
        public int IdRol { get; set; }
        public virtual ModeloRol Rol { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloRol"/> con el <see cref="ModeloPersonaje"/> que incluye
    /// </summary>
    public class TIRolPersonaje : TIRol
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        public virtual ModeloPersonaje Personaje { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloRol"/> con el <see cref="ModeloAdministradorDeCombate"/> que incluye
    /// </summary>
    public class TIRolCombate : TIRol
    {
        [ForeignKey(nameof(Combate))]
        public int IdCombate { get; set; }
        public virtual ModeloAdministradorDeCombate Combate { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloRol"/> con el <see cref="ModeloMapa"/> que incluye
    /// </summary>
    public class TIRolMapa : TIRol
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public virtual ModeloMapa Mapa { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloRol"/> con el <see cref="ModeloAmbiente"/> dentro de este.
    /// </summary>
    public class TIRolAmbiente : TIRol
    {
        [ForeignKey(nameof(Ambiente))]
        public int IdAmbiente { get; set; }
        public virtual ModeloAmbiente Ambiente { get; set; }
    }

}