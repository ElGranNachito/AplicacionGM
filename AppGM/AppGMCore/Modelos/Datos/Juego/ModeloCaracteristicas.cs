using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public class ModeloCaracteristicas : ModeloBase
    {
        /// <summary>
        /// Edad del personaje
        /// </summary>
        public int Edad { get; set; }
        /// <summary>
        /// Estatura del personaje
        /// </summary>
        public int Estatura { get; set; }
        /// <summary>
        /// Peso del personaje
        /// </summary>
        public int Peso { get; set; }

        //Datos relevantes
        /// <summary>
        /// Sexo del personaje
        /// </summary>
        public ESexo ESexo { get; set; }
        /// <summary>
        /// Tipo de personalidad del personaje
        /// </summary>
        public EArquetipo EArquetipo { get; set; }
        /// <summary>
        /// Mano dominante
        /// </summary>
        public EManoDominante EManoDominante { get; set; }

        /// <summary>
        /// Breve descripcion de su contextura fisica
        /// </summary>
        [StringLength(100)]
        [MaxLength(100)]
        public string Fisico { get; set; }

        /// <summary>
        /// Nacionalidad del personaje
        /// </summary>
        [StringLength(50)]
        [MaxLength(100)]
        public string Nacionalidad { get; set; }
        
        /// <summary>
        /// Clave foranea que referencia al <see cref="ModeloPersonajeJugable"/> al que pertenecen estas caracteristicas
        /// </summary>
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }

        /// <summary>
        /// Personaje al que pertenecen estas caracteristicas
        /// </summary>
        public virtual ModeloPersonajeJugable Personaje { get; set; }
    }
}