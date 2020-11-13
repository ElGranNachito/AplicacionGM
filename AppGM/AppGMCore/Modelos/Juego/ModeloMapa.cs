using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloMapa : ModeloBase
    {
        public string NombreMapa { get; set; }
        public EFormatoImagen EFormatoImagen { get; set; }

        public List<TIMapaUnidadMapa> PosicionesUnidades { get; set; }  = new List<TIMapaUnidadMapa>();
    }
}
