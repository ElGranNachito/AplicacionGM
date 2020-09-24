namespace AppGM.Core
{
    public class ModeloCondicion
    {
        //Id
        public int IdCondicion { get; set; }

        public string NombrePropiedad { get; set; }
        public string OperacionARealizar { get; set; }
        public string ValorContraElQueComparar { get; set; }
    }
}