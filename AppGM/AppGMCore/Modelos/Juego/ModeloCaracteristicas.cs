using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloCaracteristicas : ModeloBase
    {
        /// <summary>
        /// Edad del personaje
        /// </summary>
        public ushort Edad { get; set; }
        /// <summary>
        /// Estatura del personaje
        /// </summary>
        public ushort Estatura { get; set; }
        /// <summary>
        /// Peso del personaje
        /// </summary>
        public ushort Peso { get; set; }

        //Datos relevantes
        /// <summary>
        /// Sexo del personaje
        /// </summary>
        public ESexo          ESexo          { get; set; }
        /// <summary>
        /// Tipo de personalidad del personaje
        /// </summary>
        public EArquetipo     EArquetipo     { get; set; }
        /// <summary>
        /// Mano dominante
        /// </summary>
        public EManoDominante EManoDominante { get; set; }

        /// <summary>
        /// Breve descripcion de su contextura fisica
        /// </summary>
        [StringLength(100)]
        public string Fisico { get; set; }

        /// <summary>
        /// Nacionalidad del personaje
        /// </summary>
        [StringLength(50)]
        public string Nacionalidad { get; set; }
        
    }
}