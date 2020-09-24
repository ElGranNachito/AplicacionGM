namespace AppGMCore
{
    public class ModeloLimitador
    {
        //Id
        public int IdLimitador { get; set; }
        //Cantidad de veces que la habilidad puede ser utiliza
        public int LimiteDeUsos { get; set; }
        
        public int DiasDeEnfriamiento { get; set; }
    }
    public class ModeloCargasHabilidad
    {
        //Id
        public int IdCargasHabilidad { get; set; }
        //Maximo de cargas para una habilidad
        public int CargasMaximas { get; set; }
    }
}