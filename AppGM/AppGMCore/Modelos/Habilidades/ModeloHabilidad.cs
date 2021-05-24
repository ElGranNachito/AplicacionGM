using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para la habilidad
    /// </summary>
    public class ModeloHabilidad : ModeloBase
    {
        public ControladorHabilidad controladorHabilidad;

        //Costos de mana que tiene la habilidad para ser utilizada
        public ushort CostoDeMana { get; set; }
        //Turnos que dura la habilidad
        public ushort TurnosDeDuracion { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(2000)]
        public string Descripcion { get; set; }

        public ETipoHabilidad TipoDeHabilidad { get; set; }

        //Si es magia no se utiliza
        public ERango Rango { get; set; }

        public TIHabilidadLimitador       LimiteDeUsos    { get; set; }
        public TIHabilidadCargasHabilidad CargasHabilidad { get; set; }
        
        public TIHabilidadTiradaDeDaño    TiradaDeDaño    { get; set; }
        
        //Primer indice es el item que invoca, el resto de indices son los items que cuesta
        public List<TIHabilidadItem>       ItemsQueCuestaItemInvocacion { get; set; } = new List<TIHabilidadItem>();

        public List<TIHabilidadInvocacion> Invocacion                   { get; set; } = new List<TIHabilidadInvocacion>();
        
        public List<TIHabilidadTiradaBase> TiradasDeUso                 { get; set; } = new List<TIHabilidadTiradaBase>();
        
        //Primer indice son los efectos sobre el usuario, segundo indice son los efectos sobre el objetivo
        public List<TIHabilidadEfecto> EfectosSobreUsuarioEfectoSobreObjetivo { get; set; } = new List<TIHabilidadEfecto>();

    }

    public class ModeloPerk : ModeloHabilidad
    {
       
        
    }

    public class ModeloMagia : ModeloHabilidad
    {
        public ControladorMagia controladorMagia;

        public bool EsParticular { get; set; }
        public byte Nivel { get; set; }
    }

    public class ModeloNoblePhantasm : ModeloHabilidad
    {
        
    }
}