using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para efecto
    /// </summary>
    public class ModeloEfecto : IDescripcion
    {
        #region Propiedades

        //Id
        [Key]
        public int IdEfecto { get; set; }

        //Nombre del efecto
        [StringLength(50)]
        public string Nombre { get; set; }
        //Descripcion del efecto
        [StringLength(500)]
        public string Descripcion { get; set; }
        public List<TIEfectoModificadorDeStatBase> Modificaciones { get; set; } = new List<TIEfectoModificadorDeStatBase>();

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor default
        /// </summary>
        public ModeloEfecto() { }

        /// <summary>
        /// Crea un nuevo modelo efecto en base a los datos de uno existente
        /// </summary>
        /// <param name="_modelo">Modelo del que copiar los datos</param>
        public ModeloEfecto(ModeloEfecto _modelo)
        {
            Nombre = _modelo.Nombre;
            Descripcion = _modelo.Descripcion;

            for (int i = 0; i < _modelo.Modificaciones.Count; ++i)
                Modificaciones.Add(new TIEfectoModificadorDeStatBase
                {
                    Efecto = this,
                    Modificador = _modelo.Modificaciones[i].Modificador
                });
        } 

        #endregion
    }

    public class ModeloEfectoTemporal : ModeloEfecto
    {
        #region Propiedades

        //Turnos que dura el efecto
        public ushort TurnosDeDuracion { get; set; }

        #endregion

        #region Constructores

        public ModeloEfectoTemporal(){}

        /// <summary>
        /// Crea un nuevo modelo efecto en base a los datos de uno existente
        /// </summary>
        /// <param name="_modelo">Modelo del que copiar los datos</param>
        public ModeloEfectoTemporal(ModeloEfectoTemporal _modelo) : base(_modelo)
        {
            TurnosDeDuracion = _modelo.TurnosDeDuracion;
        } 

        #endregion
    }
}
