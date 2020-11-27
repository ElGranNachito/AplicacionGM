using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public class TIRol : ModeloBase
    {
        [ForeignKey(nameof(Rol))]
        public int IdRol { get; set; }
        public ModeloRol Rol { get; set; }
    }

    public class TIRolPersonaje : TIRol
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        public ModeloPersonaje Personaje { get; set; }
    }

    public class TIRolCombate : TIRol
    {
        [ForeignKey(nameof(Combate))]
        public int IdCombate { get; set; }
        public ModeloAdministradorDeCombate Combate { get; set; }
    }

    public class TIRolMapa : TIRol
    {
        [ForeignKey(nameof(Mapa))]
        public int IdMapa { get; set; }
        public ModeloMapa Mapa { get; set; }
    }
}