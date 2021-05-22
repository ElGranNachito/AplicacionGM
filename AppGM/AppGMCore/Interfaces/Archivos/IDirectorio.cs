using System.Collections.Generic;

namespace AppGM.Core
{
    public interface IDirectorio
    {
        #region Propiedades

        string Ruta { get; set; }
        string Nombre { get; set; }

        IDirectorio DirectorioPadre { get; set; }

        #endregion

        #region Funciones

        List<IDirectorio> ObtenerDirectorios(string patronDeBusqueda);
        List<IArchivo> ObtenerArchivos(string patronDeBusqueda);

        void Borrar(bool recursivo); 

        #endregion
    }
}
