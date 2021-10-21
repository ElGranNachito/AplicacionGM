namespace AppGM.Core
{
    public class ModeloInvocacion : ModeloPersonaje
    {
        /// <summary>
        /// La invocacion es automata
        /// </summary>
        public bool EsAutomata { get; set; }

        /// <summary>
        /// Personaje que la invoca
        /// </summary>
        public virtual ModeloPersonaje Invocador { get; set; }

        /// <summary>
        /// Datos de la invocacion
        /// </summary>
        public virtual ModeloDatosInvocacionBase DatosInvocacion { get; set; }
    }

    public class ModeloInvocacionTemporal : ModeloInvocacion
    {
        /// <summary>
        /// Turnos que dura la invocacion
        /// </summary>
        public byte TurnosDeDuracion { get; set; }
    }

    public class ModeloInvocacionCondicionada : ModeloInvocacion
    {
        public ControladorInvocacionCondicionada controladorInvocacionCondicionada;
    }
}