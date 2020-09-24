
using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ViewModelPaginaPrincipal : BaseViewModel
    {
        public ViewModelListaRoles Roles { get; set; } = new ViewModelListaRoles
        {
            Roles = new List<ViewModelRolItem>
            {
                new ViewModelRolItem
                {
                    ModeloRol = new ModeloRol
                    {
                        Nombre = "Super Rol",
                        Descripcion =
                            "Este es un rol muy piola. Era hace una vez la legendaria historia de un sujeto muy genial",
                        FechaUltimaSesion = DateTime.UtcNow.AddMonths(-8).AddHours(2).AddDays(6)
                    }
                },

                new ViewModelRolItem
                {
                    ModeloRol = new ModeloRol
                    {
                        Nombre = "Mega Rol",
                        Descripcion = "Increible rol",
                        FechaUltimaSesion = DateTime.UtcNow.AddHours(8)
                    }
                },

                new ViewModelRolItem
                {
                    ModeloRol = new ModeloRol
                    {
                        Nombre = "Rol rol rol",
                        Descripcion = "Me quede sin descripciones",
                        FechaUltimaSesion = DateTime.UtcNow.ToLocalTime()
                    }
                }

            }
        };

        public ViewModelGlobo GloboInfoRol { get; set; } = new ViewModelGlobo
        {
            ColaGloboVisible = false,

            ViewModelContenido = new ViewModelContenidoGloboInfoRol
            {
                ModeloRol = new ModeloRol
                {
                    Descripcion = "Super rol increible",
                    Nombre = "Rol"
                }
            }
        };
    }
}
