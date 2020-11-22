using System.Collections.Generic;

namespace AppGM.Core
{
    public interface IDirectorio
    {
        string Ruta { get; set; }
        string Nombre { get; set; }

        IDirectorio DirectorioPadre { get; set; }

        List<IDirectorio> ObtenerDirectorios(string patronDeBusqueda);
        List<IArchivo> ObtenerArchivos(string patronDeBusqueda);

        void Borrar(bool recursivo);
    }
}
