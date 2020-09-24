namespace AppGM.Core
{
    public class ModeloInvocacion : ModeloPersonaje
    {
        //La invocacion es automata
        public bool EsAutomata { get; set; }

        //Personaje que la invoca
        public ModeloPersonaje Invocador { get; set; }
        private ControladorPersonaje<ModeloPersonaje> ControladorInvocador { get; set; }

        //Efecto que la invocacion produce
        public ModeloEfecto Efecto { get; set; }
        private ControladorEfecto<ModeloEfecto> ControladorEfecto { get; set; }
    }

    public class ModeloInvocacionTemporal : ModeloInvocacion
    {
        //Turnos que dura la invocacion
        public bool TurnosDeDuracion { get; set; }
    }

    public class ModeloInvocacionCondicionada : ModeloInvocacion
    {
    }
}