using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloServant : ModeloPersonajeJugable
    {
        public EClaseServant mEClaseDeServant { get; set; }
        public ERango mERangoNP { get; set; }

        public List<ModeloNoblePhantasm> NoblePhantasms { get; set; }
        private List<ControladorHabilidad<ModeloNoblePhantasm>> ControladorNoblePhantasms { get; set; }
    }
}