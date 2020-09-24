using System.Collections.Generic;

namespace AppGMCore
{
    public class ModeloPersonajeJugable : ModeloPersonaje
    {
        public ModeloCaracteristicas Caracteristicas { get; set; }

        public List<ModeloInvocacion> Invocaciones { get; set; }

        private List<ControladorInvocacion<ModeloInvocacion>> ControladorInvocaciones { get; set; }
    }
}