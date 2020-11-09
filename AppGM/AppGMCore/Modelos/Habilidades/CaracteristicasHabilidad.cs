using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloLimitador : ModeloBase
    {
        //Cantidad de veces que la habilidad puede ser utiliza
        public int LimiteDeUsos { get; set; }
        
        public int DiasDeEnfriamiento { get; set; }
    }
    public class ModeloCargasHabilidad : ModeloBase
    {
        //Maximo de cargas para una habilidad
        public int CargasMaximas { get; set; }
    }
}