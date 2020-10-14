using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloHabilidad : IDescripcion
    {
        //Id
        [Key]
        public int IdHabilidad { get; set; }
        
        //Costos de mana que tiene la habilidad para ser utilizada
        public ushort CostoDeMana { get; set; }
        //Turnos que dura la habilidad
        public ushort TurnosDeDuracion { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(2000)]
        public string Descripcion { get; set; }

        public TIHabilidadLimitador LimiteDeUsos { get; set; }
        public TIHabilidadCargasHabilidad CargasHabilidad { get; set; }
        
        public TIHabilidadTiradaDeDaño TiradaDeDaño { get; set; }
        
        //Primer indice es el item que invoca, el resto de indices son los items que cuesta
        public List<TIHabilidadItem> ItemsQueCuestaItemInvocacion { get; set; }

        public List<TIHabilidadInvocacion> Invocacion { get; set; }
        
        public List<TIHabilidadTiradaBase> TiradasDeUso { get; set; }
        
        //Primer indice son los efectos sobre el usuario, segundo indice son los efectos sobre el objetivo
        public List<TIHabilidadEfecto> EfectosSobreUsuarioEfectoSobreObjetivo { get; set; }

    }

    public class ModeloPerk : ModeloHabilidad
    {
        //Rango de la Perk
        public ERango Rango { get; set; }
    }

    public class ModeloMagia : ModeloHabilidad
    {
        public byte Nivel { get; set; }
    }

    public class ModeloNoblePhantasm : ModeloHabilidad
    {
        public ERango Rango { get; set; }
    }
}