using System;
using System.Text;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloAlianza"/>
    /// </summary>
    public class ControladorAlianza : Controlador<ModeloAlianza>
    {
        #region Campos & Propiedades

        // Propiedades ---


        /// <summary>
        /// Tipo de icono que tendra la alianza como identificador de la misma.
        /// TODO: Eliminar si se descarta la idea de iconos predeterminados.
        /// </summary>
        public EIconoAlianza IconoAlianza => modelo.EIconoAlianza;

        /// <summary>
        /// Devuelve la ruta completa a la imagen del icono de esta alianza.
        /// </summary>
        public string Path => ObtenerPathAImagen();

        /// <summary>
        /// Devuelve el nombre de la alianza.
        /// </summary>
        public string Nombre => modelo.Nombre;

        /// <summary>
        /// Devuelve la descripcion de la alianza.
        /// </summary>
        public string Descripcion => modelo.Descripcion;

        /// <summary>
        /// Devuelve verdadero si la alianza sigue vigente.
        /// </summary>
        public bool EstaVigente => modelo.EsVigente;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloAlianza">Modelo de la alianza</param>
        public ControladorAlianza(ModeloAlianza _modeloAlianza)
            :base(_modeloAlianza) {}

        #endregion

        #region Funciones

        /// <summary>
        /// Obtiene el path a la imagen del icono de la alianza.
        /// </summary>
        /// <returns>Ruta absoluta del icono de la alianza</returns>
        public string ObtenerPathAImagen()
        {
            StringBuilder sb = new StringBuilder("../../../Media/Imagenes/Iconos/Alianzas/");

            sb.Append(modelo.PathImagenIcono + Enum.GetName(typeof(EFormatoImagen), modelo.FormatoImagen));

            return sb.ToString();
        }

        /// <summary>
        /// Elimina el modelo de la base de datos
        /// </summary>
        public override void Eliminar()
        {
            SistemaPrincipal.EliminarModelo(modelo.ContratoDeAlianza);
            SistemaPrincipal.EliminarModelo(modelo);
        }

        #endregion
    }
}
