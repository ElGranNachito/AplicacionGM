using System.Text;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloUnidadMapa"/>>
    /// </summary>
    public class ControladorUnidadMapa : Controlador<ModeloUnidadMapa>
    {
        #region Campos & Propiedades

        //---------------------------------CAMPOS------------------------------------


        /// <summary>
        /// Posicion de la unidad en el mapa
        /// </summary>
        public Vector2              posicion;

        /// <summary>
        /// <see cref="ControladorPersonaje"/> que es representado por esta unidad
        /// </summary>
        public ControladorPersonaje personaje;


        //-------------------------------PROPIEDADES----------------------------------


        /// <summary>
        /// Devuelve la ruta completa a la imagen de esta unidad
        /// </summary>
        public string Path => ObtenerPathAImagen();

        /// <summary>
        /// Devuelve el nombre de la unidad
        /// </summary>
        public string Nombre => modelo.Nombre;

        /// <summary>
        /// Cantidad de unidades en el grupo
        /// Asegurarse de que al llamar a esta propiedad el modelo sea de tipo <see cref="ModeloUnidadMapaInvocacionTrampa"/>
        /// </summary>
        public int Cantidad => ((ModeloUnidadMapaInvocacionTrampa)modelo).Cantidad;

        /// <summary>
        /// Tipo de esta unidad
        /// </summary>
        public ETipoUnidad TipoUnidad => modelo.ETipoUnidad;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modelo">Modelo que representara este controlador</param>
        public ControladorUnidadMapa(ModeloUnidadMapa _modelo)
			:base(_modelo)
        {
	        posicion  = new Vector2(modelo.Posicion.Posicion);
            personaje = modelo.Personaje.Personaje.controlador;
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Genera el path a la imagen de la unidad actual en base a su tipo y estado
        /// </summary>
        /// <returns>Path a la imagen de la unidad</returns>
        public string ObtenerPathAImagen()
        {
            StringBuilder sb = new StringBuilder("../../../Media/Imagenes/Posiciones/");

            switch (modelo)
            {
                case ModeloUnidadMapaInvocacionTrampa mi:

                    if (modelo.ETipoUnidad == ETipoUnidad.Invocacion)
                        sb.Append("Invocacion_");
                    else
                        sb.Append("Trampa_");

                    if (mi.EsDeMaster)
                        sb.Append("Master_");

                    sb.Append(mi.EClaseServant);

                    break;

                case ModeloUnidadMapaMasterServant mms:

                    if (modelo.Personaje != null && !modelo.Personaje.Personaje.controlador.EstaVivo)
                        sb.Append("Cadaver_");

                    if (modelo.ETipoUnidad == ETipoUnidad.Master)
                        sb.Append("Master_");

                    sb.Append(mms.EClaseServant);

                    break;

                default:
                    sb.Append("Iglesia");
                    break;

            }

            sb.Append(".png");

            return sb.ToString();
        }

        /// <summary>
        /// Elimina el modelo de la base de datos
        /// </summary>
        public override void Eliminar()
        {
            SistemaPrincipal.EliminarModelo(modelo.Posicion);
            SistemaPrincipal.EliminarModelo(modelo.Personaje);
            SistemaPrincipal.EliminarModelo(modelo);

            posicion.Eliminar();
        }

        #endregion

    }
}