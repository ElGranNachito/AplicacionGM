using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloCaracteristicas : ModeloBase
    {
        //Edad del personaje
        public ushort Edad { get; set; }
        //Estatura del personaje
        public ushort Estatura { get; set; }

        //Mas datos de suma importancia
        public EAlineamiento EAlineamiento { get; set; }
        public EManoDominante EManoDominante { get; set; }
        public ESexo ESexo { get; set; }

        //Nacionalidad del personaje
        [StringLength(50)]
        public string Nacionalidad { get; set; }
        //Contextura fisica
        [StringLength(100)]
        public string Contextura { get; set; }
    }
}