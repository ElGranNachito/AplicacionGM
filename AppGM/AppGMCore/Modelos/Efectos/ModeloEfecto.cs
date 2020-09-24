using System.Collections.Generic;

namespace AppGMCore
{
    /// <summary>
    /// Modelo de datos para efecto
    /// </summary>
    public class ModeloEfecto : IDescripcion
    {
        //Id
        public int IdEfecto { get; set; }

        //Nombre del efecto
        public string Nombre { get; set; }
        //Descripcion del efecto
        public string Descripcion { get; set; }

        //TODO: Terminar la db
        public List<ModeloModificadorDeStatBase> Modificaciones;
        private List<ControladorModificadorDeStatBase> ControladoresModificaciones;
    }
}
