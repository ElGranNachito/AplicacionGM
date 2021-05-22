using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
    public class ModeloDatosInvocacionBase : ModeloBase {}

    public class ModeloDatosInvocacion_Espiritual : ModeloDatosInvocacionBase
    {
        public int ConsumoDeManaPorTurno { get; set; }
    }

    public class ModeloDatosInvocacion_SemiEspiritual : ModeloDatosInvocacionBase
    {
        public int ConsumoDeManaDiario { get; set; }
        // Energia magica de la invocacion
        public int Prana { get; set; }
    }

    public class ModeloDatosInvocacion_Fisica : ModeloDatosInvocacionBase
    {
        //Energia magica de la invocacion fisica
        public int Od         { get; set; }
        public int OdActual   { get; set; }
        public int Mana       { get; set; }
        public int ManaActual { get; set; }
    }
}
