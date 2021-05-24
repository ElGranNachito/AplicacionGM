namespace AppGM.Core
{
    public class ModeloInvocacion : ModeloPersonaje
    {
        public ControladorInvocacion controladorInvocacion;

        /// <summary>
        /// La invocacion es automata
        /// </summary>
        public bool EsAutomata { get; set; }

        /// <summary>
        /// Personaje que la invoca
        /// </summary>
        public TIInvocacionPersonaje Invocador { get; set; }

        /// <summary>
        /// Datos de la invocacion
        /// </summary>
        public TIInvocacionDatosInvocacion DatosInvocacion { get; set; }

        /// <summary>
        /// Efecto que la invocacion produce
        /// </summary>
        public TIInvocacionEfecto Efecto { get; set; }
    }

    public class ModeloInvocacionTemporal : ModeloInvocacion
    {
        public ControladorInvocacionTemporal controladorInvocacionTemporal;

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