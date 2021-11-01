using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelCrearRol_DatosPersonajes : ViewModelPaso<ViewModelCrearRol>
    {
        #region Miembros

        private DatosCreacionRol mDatosCreacionRol;

        private bool mMostrarServants     = true;
        private bool mMostrarMasters      = true;
        private bool mMostrarInvocaciones = true;
        private bool mMostrarNPCs         = true;

        #endregion

        #region Propiedades

        public ViewModelMensajeCrearRol_ListaPersonajes ViewModelListaPersonajes { get; set; }

        public ICommand ComandoAñadirPersonaje { get; set; }
        
        #endregion

        #region Constructor

        public ViewModelCrearRol_DatosPersonajes(DatosCreacionRol _datosRol, ViewModelCrearRol vmCrearRol)
        {
            mDatosCreacionRol = _datosRol;

            ActualizarListaDePersonajes();

            ComandoAñadirPersonaje = new Comando(async () =>
            {
	            var vmCreacionPj = await new ViewModelCrearPersonaje(vm =>
	            {
		            SistemaPrincipal.Aplicacion.VentanaPrincipal.DataContextContenido = vmCrearRol;
	            }).Inicializar();

	            SistemaPrincipal.Aplicacion.VentanaPrincipal.DataContextContenido = vmCreacionPj;
            });
        }

        #endregion

        #region Funciones

        private void ActualizarListaDePersonajes()
        {
            List<ModeloPersonaje> PersonajesAListar = new List<ModeloPersonaje>();

            if (mMostrarMasters)
                PersonajesAListar.AddRange(mDatosCreacionRol.masters);
            if (mMostrarServants)
                PersonajesAListar.AddRange(mDatosCreacionRol.servants);
            if (mMostrarInvocaciones)
                PersonajesAListar.AddRange(mDatosCreacionRol.invocaciones);
            if (mMostrarNPCs)
                PersonajesAListar.AddRange(mDatosCreacionRol.npcs);

            ViewModelListaPersonajes = new ViewModelMensajeCrearRol_ListaPersonajes(mDatosCreacionRol, new ObservableCollection<ModeloPersonaje>(PersonajesAListar));
        }

        #endregion

        public bool MostrarServants
        {
            get => mMostrarServants;
            set
            {
                mMostrarServants = value;
                
                ActualizarListaDePersonajes();
            }
        }

        public bool MostrarMasters
        {
            get => mMostrarMasters;
            set
            {
                mMostrarMasters = value;

                ActualizarListaDePersonajes();
            }
        }

        public bool MostrarInvocaciones
        {
            get => mMostrarInvocaciones;
            set
            {
                mMostrarInvocaciones = value;

                ActualizarListaDePersonajes();
            }
        }

        public bool MostrarNPCs
        {
            get => mMostrarNPCs;
            set
            {
                mMostrarNPCs = value;

                ActualizarListaDePersonajes();
            }
        }
    }
}
