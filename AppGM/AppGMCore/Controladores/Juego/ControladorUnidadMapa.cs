

using System.Text;

namespace AppGM.Core
{
    public class ControladorUnidadMapa : Controlador<ModeloUnidadMapa>
    {
        #region Miembros

        public Vector2              posicion;
        public ControladorPersonaje personaje;

        #endregion

        #region Constructores

        public ControladorUnidadMapa(ModeloUnidadMapa _modelo)
        {
            modelo = _modelo;

            posicion  = new Vector2(modelo.Posicion.Posicion);
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

        public override void Eliminar()
        {
            SistemaPrincipal.EliminarModelo(modelo.Posicion);
            SistemaPrincipal.EliminarModelo(modelo.Personaje);
            SistemaPrincipal.EliminarModelo(modelo);

            posicion.Eliminar();
        }

        #endregion

        #region Propiedades
        public string      Path       => ObtenerPathAImagen();
        public string      Nombre     => modelo.Nombre;

        /// <summary>
        /// Cantidad de unidades en el grupo. Asegurarse de que al llamar a esta propiedad
        /// el modelo sea de tipo <see cref="ModeloUnidadMapaInvocacionTrampa"/>
        /// </summary>
        public int Cantidad           => ((ModeloUnidadMapaInvocacionTrampa) modelo).Cantidad;
        public ETipoUnidad TipoUnidad => modelo.ETipoUnidad; 

        #endregion

    }
}
