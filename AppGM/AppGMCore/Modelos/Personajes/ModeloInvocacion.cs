namespace AppGM.Core
{
    public class ModeloInvocacion : ModeloPersonaje
    {
        public ControladorInvocacion controladorInvocacion;

        //La invocacion es automata
        public bool EsAutomata { get; set; }

        //Personaje que la invoca
        public TIInvocacionPersonaje Invocador { get; set; }

        //Datos de la invocacion
        public TIInvocacionDatosInvocacion DatosInvocacion { get; set; }

        //Efecto que la invocacion produce
        public TIInvocacionEfecto Efecto { get; set; }
    }

    public class ModeloInvocacionTemporal : ModeloInvocacion
    {
        public ControladorInvocacionTemporal controladorInvocacionTemporal;

        //Turnos que dura la invocacion
        public byte TurnosDeDuracion { get; set; }
    }

    public class ModeloInvocacionCondicionada : ModeloInvocacion
    {
        public ControladorInvocacionCondicionada controladorInvocacionCondicionada;
    }
}