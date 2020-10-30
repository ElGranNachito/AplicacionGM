

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
            //TODO: Deberiamos obtener el controlador desde el sistema principal
        }

        #endregion

        #region Funciones
        public string ObtenerPathAImagen()
        {
            StringBuilder sb = new StringBuilder("../../../Media/Imagenes/Posiciones/");

            if (modelo.ETipoUnidad == ETipoUnidad.Iglesia)
                sb.Append("Iglesia");
            else if (
                (modelo.ETipoUnidad & (ETipoUnidad.Master | ETipoUnidad.Servant)) != 0
                && modelo is ModeloUnidadMapaMasterServant mms)
            {
                if (modelo.ETipoUnidad == ETipoUnidad.Master)
                    sb.Append("Master_");

                sb.Append(mms.EClaseServant);
            }
            else if (modelo is ModeloUnidadMapaInvocacionTrampa mi)
            {
                if (modelo.ETipoUnidad == ETipoUnidad.Invocacion)
                    sb.Append("Invocacion_");
                else
                    sb.Append("Trampa_");

                if (mi.EsDeMaster)
                    sb.Append("Master_");

                sb.Append(mi.EClaseServant);
            }

            sb.Append(".png");

            return sb.ToString();
        }

        #endregion

        #region Propiedades

        public bool        EsIglesia  => modelo.ETipoUnidad == ETipoUnidad.Iglesia;
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
