using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para habilidades de un <see cref="ModeloPersonaje"/>
    /// </summary>
    public partial class ModeloHabilidad : ModeloConVariablesYTiradas
    {
	    /// <summary>
        /// Indica si la habilidad esta actualmente activa
        /// </summary>
        private bool EstaActiva;

        /// <summary>
        /// Costos de od o prana (tipos de energia magica) que tiene la habilidad para ser utilizada
        /// </summary>
        public int CostoDeOdOPrana { get; set; }

        /// <summary>
        /// Costos de mana (energia magica concentrada) que tiene la habilidad para ser utilizada
        /// </summary>
        public int CostoDeMana { get; set; }
        
        /// <summary>
        /// Turnos que dura la habilidad
        /// </summary>
        public int TurnosDeDuracion { get; set; }

        /// <summary>
        /// Turnos restantes de la habilidad
        /// </summary>
        private int TurnosRestantes;

        /// <summary>
        /// Nombre de la habilidad
        /// </summary>
        [StringLength(50)]
        [MaxLength(50)]
        public string Nombre { get; set; }
        /// <summary>
        /// Descripcion de la habilidad
        /// </summary>
        [StringLength(2000)]
        [MaxLength(2000)]
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

        /// <summary>
        /// <see cref="ModeloPersonaje"/> que posee esta habilidad
        /// </summary>
        public virtual ModeloPersonaje Dueño { get; set; }

        /// <summary>
        /// Tiradas para utilizar la habilidad
        /// </summary>
        public virtual List<ModeloTiradaBase> TiradasDeUso { get; set; } = new List<ModeloTiradaBase>();

        /// <summary>
        /// Efectos de la habilidad
        /// </summary>
        public virtual List<ModeloEfecto> Efectos { get; set; } = new List<ModeloEfecto>();
        
        /// <summary>
        /// Funciones que requiere la habilidad para funcionar
        /// </summary>
        public virtual List<TIFuncionHabilidad> Funciones { get; set; } = new List<TIFuncionHabilidad>();

		public override IReadOnlyList<ModeloVariableBase> ObtenerVariablesDisponibles()
		{
            var variablesDisponibles = new List<ModeloVariableBase>(Variables);

            variablesDisponibles.AddRange(Dueño.Variables);

            return variablesDisponibles.AsReadOnly();
		}
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
	    /// <summary>
        /// Nivel que califica a la magia
        /// </summary>
        public byte Nivel { get; set; }

        /// <summary>
        /// Indica si se trata de un hechizo unico por parte del personaje.
        /// Al ser particular, no tiene un limite de niveles.
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