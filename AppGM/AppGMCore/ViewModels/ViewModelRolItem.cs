using System;
using AppGMCore;

namespace AppGM.Core
{
    public class ViewModelRolItem : BaseViewModel
    {
        //Id
        public int IdRol { get; set; }

        //Dia dentro del mundo del rol
        public ushort DiaEnRol { get; set; }

        //Nombre del rol
        public string Nombre { get; set; }

        //Descripcion del rol
        public string Descripcion { get; set; }

        //Anotaciones realizadas por el GM
        public string Registros { get; set; }

        // Ultima fecha de uso del rol (sesion)
        public DateTime FechaUltimaSesion { get; set; }
    }

}
