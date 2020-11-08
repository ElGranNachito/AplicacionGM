namespace AppGM.Core
{
    public class ModeloInvocacion : ModeloPersonaje
    {
        //La invocacion es automata
        public bool EsAutomata { get; set; }

        //Personaje que la invoca
        public TIInvocacionPersonaje Invocador { get; set; }

        //Efecto que la invocacion produce
        public TIInvocacionEfecto Efecto { get; set; }
    }

    public class ModeloInvocacionTemporal : ModeloInvocacion
    {
        //Turnos que dura la invocacion
        public byte TurnosDeDuracion { get; set; }
    }

    public class ModeloInvocacionCondicionada : ModeloInvocacion
    {
    }
}