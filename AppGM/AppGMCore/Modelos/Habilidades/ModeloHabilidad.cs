using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para habilidades de un <see cref="ModeloPersonaje"/>
    /// </summary>
    public class ModeloHabilidad : ModeloBase
    {
        /// <summary>
        /// Controlador
        /// </summary>
        public ControladorHabilidad controladorHabilidad;

        /// <summary>
        /// Indica si la habilidad esta actualmente activa
        /// </summary>
        private bool EstaActiva;

        /// <summary>
        /// Costos de od o prana (tipos de energia magica) que tiene la habilidad para ser utilizada
        /// </summary>
        public ushort CostoDeOdOPrana { get; set; }

        /// <summary>
        /// Costos de mana (energia magica concentrada) que tiene la habilidad para ser utilizada
        /// </summary>
        public ushort CostoDeMana { get; set; }
        
        /// <summary>
        /// Turnos que dura la habilidad
        /// </summary>
        public ushort TurnosDeDuracion { get; set; }

        /// <summary>
        /// Turnos restantes de la habilidad
        /// </summary>
        private ushort TurnosRestantes;

        /// <summary>
        /// Nombre de la habilidad
        /// </summary>
        [StringLength(50)]
        public string Nombre { get; set; }
        /// <summary>
        /// Descripcion de la habilidad
        /// </summary>
        [StringLength(2000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Que tipo de habilidad es
        /// </summary>
        public ETipoHabilidad TipoDeHabilidad { get; set; }

        /// <summary>
        /// Rango (F-EX) en el que se calificaria a la habilidad
        /// Si es magia no se utiliza
        /// </summary>
        public ERango Rango { get; set; }

        /// <summary>
        /// Verdadero si el uso de esta habilidad no se ve afectado por las condiciones del ambiente.
        /// </summary>
        public bool IgnoraAmbiente { get; set; }

        //Caracteristicas de la habilidad:
        /// <summary>
        /// Total de usos que se le puede dar a la habilidad
        /// </summary>
        public virtual TIHabilidadLimitador       LimiteDeUsos    { get; set; }
        /// <summary>
        /// Cantidad de cargas aplicaples a la habilidad
        /// </summary>
        public virtual TIHabilidadCargasHabilidad CargasHabilidad { get; set; }
        
        /// <summary>
        /// Tirada del daño que produce la habilidad
        /// Solo si es una habilidad ofensiva
        /// </summary>
        public virtual TIHabilidadTiradaDeDaño    TiradaDeDaño    { get; set; }
        
        /// <summary>
        /// Items utilizados para invocar otro item
        /// Primer indice es el item que invoca, el resto de indices son los items que cuesta
        /// </summary>
        public virtual List<TIHabilidadItem>       ItemsQueCuestaItemInvocacion { get; set; } = new List<TIHabilidadItem>();

        /// <summary>
        /// Personaje de clase <see cref="ModeloInvocacion"/> que es invocado por la habilidad
        /// </summary>
        public virtual List<TIHabilidadInvocacion> Invocacion                   { get; set; } = new List<TIHabilidadInvocacion>();
        
        /// <summary>
        /// Tiradas para utilizar la habilidad
        /// </summary>
        public virtual List<TIHabilidadTiradaBase> TiradasDeUso                 { get; set; } = new List<TIHabilidadTiradaBase>();
        
        /// <summary>
        /// Efectos que aplica la habilidad
        /// Primer indice son los efectos sobre el usuario, segundo indice son los efectos sobre el objetivo
        /// </summary>
        public virtual List<TIHabilidadEfecto> EfectosSobreUsuarioEfectoSobreObjetivo { get; set; } = new List<TIHabilidadEfecto>();

    }

    /// <summary>
    /// Modelo para las perks de un <see cref="ModeloPersonaje"/>
    /// </summary>
    public class ModeloPerk : ModeloHabilidad
    {
       
        
    }

    /// <summary>
    /// Modelo para las habilidades magicas de un <see cref="ModeloPersonaje"/>
    /// </summary>
    public class ModeloMagia : ModeloHabilidad
    {
        public ControladorMagia controladorMagia;

        /// <summary>
        /// Nivel que califica a la magia
        /// </summary>
        public byte Nivel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EsParticular { get; set; }
    }

    /// <summary>
    /// Modelo para los NPs de un <see cref="ModeloServant"/>
    /// </summary>
    public class ModeloNoblePhantasm : ModeloHabilidad
    {
        
    }
}