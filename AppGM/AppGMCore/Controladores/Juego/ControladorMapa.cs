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
                controladoresUnidadesMapa.Add(new ControladorUnidadMapa(_modeloMapa.PosicionesUnidades[i].Unidad));
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Obtiene la extension de la imagen del mapa
        /// </summary>
        /// <returns>extension de la imagen del mapa</returns>
        public string ObtenerExtension() => modelo.EFormatoImagen.Valor();

        /// <summary>
        /// Añade una unidad al mapa
        /// </summary>
        /// <param name="unidad">Unidad que añadir</param>
        public void AñadirUnidad(ModeloUnidadMapa unidad)
        {
            TIMapaUnidadMapa mapaUnidad = new TIMapaUnidadMapa
            {
                Unidad = unidad,
                Mapa   = modelo
            };

            modelo.PosicionesUnidades.Add(mapaUnidad);

            SistemaPrincipal.GuardarModelo(mapaUnidad);
        }

        #endregion
    }
}
