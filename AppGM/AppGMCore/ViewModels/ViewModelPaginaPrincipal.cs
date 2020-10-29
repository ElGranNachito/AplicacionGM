using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    //TODO: Probablemente hacer este view model una clase estatica para que no sea tanto lio
    public class ViewModelPaginaPrincipal : BaseViewModel
    {
        #region Propiedades

        private int mIndiceRolActual = 0;

        public bool MouseSobreCartaRol { get; set; } = false;

        public ICommand ComandoAvanzarIndiceRol { get; set; }
        public ICommand ComandoRetrocederIndiceRol { get; set; }

        public ModeloRol RolActual => Roles[mIndiceRolActual];

        public ViewModelGlobo<ViewModelContenidoGloboInfoRol> GloboInfoRol { get; set; } = new ViewModelGlobo<ViewModelContenidoGloboInfoRol>
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

        public ViewModelCarta CartaAñadirRol { get; set; } = new ViewModelCarta
        {
            ZIndex = 1
        };

        public ViewModelCarta CartaSeleccionarRol { get; set; } = new ViewModelCarta
        {
            ZIndex = 0,
        };

        public List<ModeloRol> Roles = new List<ModeloRol>
        {
            new ModeloRol
            {
                Nombre = "SuperRol",
                Descripcion = "Increible rol super genial con mucha emocion y accion",
                FechaUltimaSesion = DateTime.UtcNow.AddDays(5)
            },

            new ModeloRol
            {
                Nombre = "MegaRol",
                Descripcion = "Super, super, super rol",
                FechaUltimaSesion = DateTime.UtcNow.AddYears(-8)
            }
        };
        #endregion

        #region Constructor
        public ViewModelPaginaPrincipal()
        {
            EstablecerComandosCartaSeleccionarRol();

            ComandoAvanzarIndiceRol = new Comando(() =>
            {
                if (mIndiceRolActual < Roles.Count - 1)
                    ++mIndiceRolActual;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(RolActual)));
            });

            ComandoRetrocederIndiceRol = new Comando(() =>
            {
                if (mIndiceRolActual != 0)
                    --mIndiceRolActual;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(RolActual)));
            });
        }
        #endregion

        #region Funciones
        private void EstablecerComandosCartaSeleccionarRol()
        {
            CartaSeleccionarRol.Comando = new Comando(async () =>
            {
                await SistemaPrincipal.CargarRol(RolActual);

                SistemaPrincipal.Aplicacion.EPaginaActual =
                    EPaginaActual.PaginaPrincipalRol;
            });

            CartaSeleccionarRol.ComandoMouseEnter = new Comando(() =>
            {
                CartaSeleccionarRol.ZIndex = 1;

                MouseSobreCartaRol = true;
                GloboInfoRol.GloboVisible = true;

                GloboInfoRol.ViewModelContenido.ModeloRol = RolActual;
            });

            CartaSeleccionarRol.ComandoMouseLeave = new Comando(() =>
            {
                CartaSeleccionarRol.ZIndex = 0;

                MouseSobreCartaRol = false;
                GloboInfoRol.GloboVisible = false;
            });
        } 
        #endregion
    }
}
