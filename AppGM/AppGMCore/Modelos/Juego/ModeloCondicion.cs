namespace AppGM.Core
{
    //TODO: Verificar si esta clase aun es necesaria teniendo en cuenta los cambios en el diseño.
    public class ModeloCondicion : ModeloBase
    {
        public ControladorCondicion controladorCondicion;

        public string NombrePropiedad { get; set; }
        public string OperacionARealizar { get; set; }
        public string ValorContraElQueComparar { get; set; }
    }
}