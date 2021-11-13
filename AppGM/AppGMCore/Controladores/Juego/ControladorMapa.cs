using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloMapa"/>
    /// </summary>
    public class ControladorMapa : Controlador<ModeloMapa>
    {
        #region Propiedades

        /// <summary>
        /// Controladore de las unidades en el mapa
        /// </summary>
        public List<ControladorUnidadMapa> controladoresUnidadesMapa = new List<ControladorUnidadMapa>();

        /// <summary>
        /// Nombre del mapa
        /// </summary>  
        public string NombreMapa => modelo.NombreMapa;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloMapa">Modelo del mapa</param>
        public ControladorMapa(ModeloMapa _modeloMapa)
			:base(_modeloMapa)
        {
	        for (int i = 0; i < modelo.PosicionesUnidades.Count; ++i)
                controladoresUnidadesMapa.Add(new ControladorUnidadMapa(_modeloMapa.PosicionesUnidades[i]));
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Añade una unidad al mapa
        /// </summary>
        /// <param name="unidad">Unidad que añadir</param>
        public void AñadirUnidad(ModeloUnidadMapa unidad)
        {
            modelo.PosicionesUnidades.Add(unidad);

            SistemaPrincipal.GuardarModelo(unidad);
        }

        #endregion
    }
}
