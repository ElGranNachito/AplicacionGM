namespace AppGM.Core
{
    public class ModeloCaracteristicas
    {
        //Id
        public int IdCaracteristica { get; set; }

        //Edad del personaje
        public ushort Edad { get; set; }
        //Estatura del personaje
        public ushort Estatura { get; set; }

        //Mas datos de suma importancia
        public EAlineamiento EAlineamiento { get; set; }
        public EManoDominante EManoDominante { get; set; }
        public ESexo ESexo { get; set; }

        //Nacionalidad del personaje
        public string Nacionalidad { get; set; }
        //Contextura fisica
        public string Contextura { get; set; }
    }
}