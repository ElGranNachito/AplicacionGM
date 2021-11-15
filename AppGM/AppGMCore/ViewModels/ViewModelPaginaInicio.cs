using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM utilizado por el control de la pagina principal
    /// </summary>
    public class ViewModelPaginaInicio : ViewModel
    {
        #region Campos & Propiedades

        //------------------------CAMPOS-----------------------------

        /// <summary>
        /// IndiceZ del rol actualmente seleccionado.
        /// </summary>
        private int mIndiceRolActual = 0;

        /// <summary>
        /// Indica si se esta cargando el rol seleccionado.
        /// </summary>
        private bool mEstaCargando;

        /// <summary>
        /// Animacion del fondo.
        /// </summary>
        private Animacion mAnimacionFondoMenuPrincipalLoop;


        //----------------------PROPIEDADES--------------------------

        /// <summary>
        /// Comando que se ejecuta para avanzar en la lista de roles disponibles
        /// </summary>
        public ICommand ComandoAvanzarIndiceRol { get; set; }

        /// <summary>
        /// Comando que se ejecuta para retroceder en la lista de roles disponibles
        /// </summary>
        public ICommand ComandoRetrocederIndiceRol { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando se presiona el boton crear rol
        /// </summary>
        public ICommand ComandoCrearRol { get; set; }

        /// <summary>
        /// Indica si el mouse esta actualmente sobre una carta de rol
        /// </summary>
        public bool MouseSobreCartaRol { get; set; } = false;

        /// <summary>
        /// Ruta del fotograma actual
        /// </summary>
        public string FotogramaActualAnimacionFondo { get; set; }

        /// <summary>
        /// Devuelve el rol seleccionado actualmente
        /// </summary>
        public ModeloRol RolActual
        {
	        get
	        {
		        if (mIndiceRolActual >= Roles.Count)
			        return null;

		        return Roles[mIndiceRolActual];
	        }
        }

        /// <summary>
        /// Viewmodel del globo que muestra la info del rol actualmente seleccionado
        /// </summary>
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

        //TODO:Sacar
        public ViewModelCarta CartaPiola { get; set; } = new ViewModelCarta
        {

        };

        /// <summary>
        /// VM Carta de creacion de rol
        /// </summary>
        public ViewModelCarta CartaAñadirRol { get; set; } = new ViewModelCarta
        {
            ZIndex = 1
        };

        /// <summary>
        /// VM Carta de seleccion de rol
        /// </summary>
        public ViewModelCarta CartaSeleccionarRol { get; set; } = new ViewModelCarta
        {
            ZIndex = 0,
        };

        /// <summary>
        /// Lista de los roles disponibles
        /// </summary>
        public List<ModeloRol> Roles = new List<ModeloRol>();
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelPaginaInicio()
        {
	        CartaAñadirRol.Comando = new Comando(CrearRol);

            //Comando que se ejecuta al presionar la carta de seleccionar rol
            CartaSeleccionarRol.Comando = new Comando(async () =>
            {
                if (mEstaCargando)
                    return;

                mEstaCargando = true;

	            await SistemaPrincipal.CargarRolAsincronicamente(RolActual.Id);

                mEstaCargando = false;

	            SistemaPrincipal.Aplicacion.PaginaActual =
		            EPagina.PaginaPrincipalRol;
            });

            //Comando que se ejecuta cuando el mouse se posiciona sobre la carta de seleccionar rol
            CartaSeleccionarRol.ComandoMouseEnter = new Comando(() =>
            {
	            CartaSeleccionarRol.ZIndex = 1;

	            MouseSobreCartaRol = true;
	            GloboInfoRol.GloboVisible = true;

	            GloboInfoRol.ViewModelContenido.ModeloRol = RolActual;
            });

            //Comando que se ejecuta cuando el mouse deja de estar sobre la carta de seleccionar rol
            CartaSeleccionarRol.ComandoMouseLeave = new Comando(() =>
            {
	            CartaSeleccionarRol.ZIndex = 0;

	            MouseSobreCartaRol = false;
	            GloboInfoRol.GloboVisible = false;
            });

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

            //Creamos la animacion del fondo
            mAnimacionFondoMenuPrincipalLoop = new Animacion(path =>
                {
                    FotogramaActualAnimacionFondo = (string)path;
                },
                1.2f,
                6,
                Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioAnimaciones, $"FondoMenuPrincipal{Path.DirectorySeparatorChar}FondoMenuPrincipal_"),
                    EFormatoImagen.Jpg,
                true);

            //Añadimos la animacion al controlador
            ControladorDeAnimaciones.AñadirAnimacionAsincronicamente(mAnimacionFondoMenuPrincipalLoop);
        }

        #endregion

        #region Metodos

        public async Task CargarRolesDisponibles()
        {
	        await using RolContext rolContext = new RolContext();

	        Roles.AddRange(rolContext.Roles);
        }

        /// <summary>
        /// Crea el popup de creacion de rol
        /// </summary>
        private async void CrearRol()
        {
	        SistemaPrincipal.Aplicacion.PaginaActual = EPagina.CreacionDeRol;

	        var vmCreacionRol = (ViewModelCrearRol) SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;
           vmCreacionRol.OnResultadoEstablecido += res =>
		        {
                    if(res.EsAceptarOFinalizar())
						Roles.Add(vmCreacionRol.modeloRol);
		        };
        }
        #endregion
    }
}