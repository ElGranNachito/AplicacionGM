using System.Collections.Generic;

namespace AppGMCore
{
    public class ModeloAdministradorDeCombate
    {
        public List<ModeloParticipante> Participantes { get; set; }

        public int IndicePersonajeTurnoActual { get; set; }
        public uint TurnoActual { get; set; }
    }
}