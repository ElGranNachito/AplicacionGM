using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloCaracteristicas : ModeloBase
    {
        //Edad del personaje
        public ushort Edad { get; set; }
        //Estatura del personaje
        public ushort Estatura { get; set; }
        //Peso del personaje
        public ushort Peso { get; set; }

        //Datos relevantes
        public ESexo          ESexo          { get; set; }
        public EArquetipo     EArquetipo     { get; set; }
        public EManoDominante EManoDominante { get; set; }

        //Breve descripcion de su contextura fisica
        [StringLength(100)]
        public string Fisico { get; set; }

        //Nacionalidad del personaje
        [StringLength(50)]
        public string Nacionalidad { get; set; }
        
    }
}