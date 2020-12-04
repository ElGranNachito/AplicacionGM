using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloLimitador : ModeloBase
    {
        public ControladorLimitador controladorLimitador;

        //Cantidad de veces que la habilidad puede ser utiliza
        public int LimiteDeUsos { get; set; }
        
        //Dias de enfriamiento para que se restablezcan los usos. Si no se pueden restablecer entonces el valor sera -1
        public int DiasDeEnfriamiento { get; set; }
    }
    public class ModeloCargasHabilidad : ModeloBase
    {
        public ControladorCargasHabilidad controladorCargasHabilidad;

        //Maximo de cargas para una habilidad
        public int CargasMaximas { get; set; }
    }
}