using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloLimitador : ModeloBase
    {
        public ControladorLimitador controladorLimitador;

        //Total de veces que la habilidad puede ser utiliza
        public int LimiteDeUsos  { get; set; }
        //Usos restantes de la habilidad
        public int UsosRestantes { get; set; }
        
        //Dias de enfriamiento para restablecer los usos. De no poder restablecerse, el valor sera -1
        public int DiasDeEnfriamiento { get; set; }
        //Dias restantes para restablecer los usos de la habilidad
        public int DiasRestantes { get; set; }
    }
    public class ModeloCargasHabilidad : ModeloBase
    {
        public ControladorCargasHabilidad controladorCargasHabilidad;

        //Maximo de cargas para la habilidad
        public int CargasMaximas  { get; set; }
        //Cargas actualmente utilizadas por la habilidad
        public int CargasActuales { get; set; }
    }
}