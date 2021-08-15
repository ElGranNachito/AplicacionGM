using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un mapa
    /// </summary>
    public class ViewModelMapa : ViewModel
    {
        #region Campos & Propiedades

        //------------------------------------CAMPOS-------------------------------------

        /// <summary>
        /// Indica si el indicador de iglesia en el mapa debe ser visible o no.
        /// </summary>
        private bool mostrarIglesia           = true;

        /// <summary>
        /// Indica si los indicadores de masters en el mapa deben ser visibles o no.
        /// </summary>
        private bool mostrarMasters           = true;
        /// <summary>
        /// Indica si los indicadores de masters en el mapa deben ser visibles o no.
        /// </summary>
        private bool mostrarServants          = true;

        /// <summary>
        /// Indica si los indicadores de invocaciones en el mapa deben ser visibles o no.
        /// </summary>
        private bool mostrarInvocaciones      = true;
        /// <summary>
        /// Indica si los indicadores de trampas en el mapa deben ser visibles o no.
        /// </summary>
        private bool mostrarTrampas           = true;

        /// <summary>
        /// Indica si los indicadores de cadaveres de masters en el mapa deben ser visibles o no.
        /// </summary>
        private bool mostrarCadaveresMasters  = true;
        /// <summary>
        /// Indica si los indicadores de cadaveres de servants en el mapa deben ser visibles o no.
        /// </summary>
        private bool mostrarCadaveresServants = true;

        /// <summary>
        /// Indica si solo los indicadores de servants y masters (parties) deben ser visibles o no.
        /// </summary>
        private bool mostrarParties           = true;
        /// <summary>
        /// Indica si las insignia de alianzas de personajes deben ser visibles sobre los indicadores de los mismos.
        /// </summary>
        private bool mostrarAlianzas          = true;

        /// <summary>
        /// Controlador del mapa
        /// </summary>
        public ControladorMapa controladorMapa;

        
        //---------------------------------PROPIEDADES-----------------------------------

        /// <summary>
        /// Comando que se jecutara al presionar el boton 'AñadirParticipante'
        /// </summary>
        public ICommand ComandoAñadirParticipante { get; set; }

        /// <summary>
        /// VM de para el ingreso y visualizacion de las posiciones de las diferentes entidades presentes en el mapa
        /// </summary>
        public ObservableCollection<ViewModelIngresoPosicion> Posiciones   { get; set; } = new ObservableCollection<ViewModelIngresoPosicion>();

        /// <summary>
        /// VM de para el ingreso y visualizacion de las posiciones de las diferentes entidades presentes en el mapa
        /// </summary>
        public ObservableCollection<ViewModelUnidadParty> PosicionesParties { get; set; } = new ObservableCollection<ViewModelUnidadParty>();

        /// <summary>
        /// Ruta completa a la imagen del mapa
        /// </summary>
        public string PathImagen { get; set; }

        /// <summary>
        /// Tamaño del canvas que contiene la imagen del mapa
        /// </summary>
        public ViewModelVector2 TamañoCanvas { get; set; } = new ViewModelVector2();

        /// <summary>
        /// Tamaño de las imagenes de las unidades
        /// </summary>
        public ViewModelVector2 TamañoImagenesPosicion { get; set; } = new ViewModelVector2(101.25, 138.75);

        /// <summary>
        /// Tamaño del canvas en el eje X
        /// </summary>
        public double TamañoCanvasX
        {
            get => TamañoCanvas.X.Round(1);
            set
            {
                TamañoCanvas.X = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MitadTamañoCanvasX)));
            }
        }

        /// <summary>
        /// Tamaño del canvas en el eje Y
        /// </summary>
        public double TamañoCanvasY
        {
            get => TamañoCanvas.Y.Round(1);
            set
            {
                TamañoCanvas.Y = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MitadTamañoCanvasY)));
            }
        }

        /// <summary>
        /// Mitad del tamaño del canvas en el eje X
        /// </summary>
        public double MitadTamañoCanvasX => (TamañoCanvas.X / 2.0f).Round(1);

        /// <summary>
        /// Mitad del tamaño del canvas en el eje Y
        /// </summary>
        public double MitadTamañoCanvasY => (TamañoCanvas.Y / 2.0f).Round(1);

        /// <summary>
        /// Mitad del tamaño de las imagenes de las unidades
        /// </summary>
        public ViewModelVector2 MitadTamañoImagenesPosicion => new ViewModelVector2(
            -(TamañoImagenesPosicion.X / 2.0f).Round(1),
            -(TamañoImagenesPosicion.Y / 2.0f).Round(1));

        // Propiedades de visibilidad de unidades en el mapa:

        public bool MuestraUnidadeIglesia
        {
            get => mostrarIglesia;
            set
            {
                mostrarIglesia = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Iglesia)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesMasters
        {
            get => mostrarMasters;
            set
            {
                mostrarMasters = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Master)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesServants
        {
            get => mostrarServants;
            set
            {
                mostrarServants = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Servant)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesInvocaciones
        {
            get => mostrarInvocaciones;
            set
            {
                mostrarInvocaciones = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Invocacion)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesTrampas
        {
            get => mostrarTrampas;
            set
            {
                mostrarTrampas = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Trampa)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesCadaveresMasters
        {
            get => mostrarCadaveresMasters;
            set
            {
                mostrarCadaveresMasters = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.CadaverMaster)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesCadaveresServants
        {
            get => mostrarCadaveresServants;
            set
            {
                mostrarCadaveresServants = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.CadaverMaster)
                    {
                        Posiciones[i].ImagenPosicionEsVisible = value;
                    }
                }
            }
        }

        public bool MuestraUnidadesAlianzas
        {
            get => mostrarAlianzas;
            set
            {
                mostrarAlianzas = value;

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    Posiciones[i].InsigneasAlianzasSonVisibles = value;
                }
            }
        }

        public bool MuestraUnidadesParties
        {
            get => mostrarParties;
            set
            {
                mostrarParties = value;

                mostrarMasters  = !value;
                mostrarServants = !value;

                //TODO: Implementar sistemas de indicadores party por una unica unidad.
                //TODO: Al cambiar la posicion de dicha unidad se estaria cambiando l

                for (int i = 0; i < Posiciones.Count; ++i)
                {
                    if (Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Master || Posiciones[i].unidad.TipoUnidad == ETipoUnidad.Servant) 
                    {
                        Posiciones[i].ImagenPosicionEsVisible = !value;
                    }
                }

                for (int i = 0; i < PosicionesParties.Count; ++i)
                {
                    PosicionesParties[i].ImagenPosicionEsVisible = value;
                }
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_controlador">Controlador del mapa</param>
        public ViewModelMapa(ControladorMapa _controlador)
        {
            controladorMapa = _controlador;

            PathImagen = "../../../Media/Imagenes/Mapas/" +
                          controladorMapa.NombreMapa + controladorMapa.ObtenerExtension();

            Dictionary<ENumeroParty, ViewModelUnidadParty> posicionesParty = new Dictionary<ENumeroParty, ViewModelUnidadParty>();

            //Creamos los view models para el ingreso de las diferentes posiciones.
            for (int i = 0; i < controladorMapa.controladoresUnidadesMapa.Count; ++i)
            {
                Posiciones.Add(new ViewModelIngresoPosicion(this, controladorMapa.controladoresUnidadesMapa[i]));

                if (!posicionesParty.ContainsKey(Posiciones.Last().unidad.personaje.modelo.NumeroParty))
                {
                    posicionesParty.Add(Posiciones.Last().unidad.personaje.modelo.NumeroParty, new ViewModelUnidadParty(this, controladorMapa.controladoresUnidadesMapa[i]));
                }

                posicionesParty[Posiciones.Last().unidad.personaje.modelo.NumeroParty].PersonajesParty.Add(Posiciones[i]);
            }

            foreach (var party in posicionesParty)
            {
                PosicionesParties.Add(party.Value);
            }

            ComandoAñadirParticipante = new Comando(AñadirUnidad);
        }

        /// <summary>
        /// No utilizar
        /// </summary>
        public ViewModelMapa() {}

		#endregion

		#region Funciones

        /// <summary>
        /// Funcion llamada para añadir una nueva entidad al mapa
        /// </summary>
		private async void AñadirUnidad()
		{
            //VM para el contenido del popup
			ViewModelMensajeCrearUnidadMapa vm = new ViewModelMensajeCrearUnidadMapa(this);

            //Creamos el popup y esperamos a que se cierre
			await SistemaPrincipal.Aplicacion.VentanaMensajePrincipal.Mostrar(vm, "Añadir Unidad", true, -1, -1);

            //Si el resultado es valido entonces añadimos la nueva unidad
            if (vm.vmResultado is ViewModelIngresoPosicion vmNuevaUndiad)
            {
                switch (vmNuevaUndiad.unidad.TipoUnidad)
                {
                    case ETipoUnidad.Iglesia: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarIglesia; 
                        break;
                    case ETipoUnidad.Master: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarMasters; 
                        break;
                    case ETipoUnidad.Servant: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarServants; 
                        break;
                    case ETipoUnidad.Invocacion: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarInvocaciones; 
                        break;
                    case ETipoUnidad.Trampa: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarTrampas; 
                        break;
                    case ETipoUnidad.CadaverMaster: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarCadaveresMasters;
                        break;
                    case ETipoUnidad.CadaverServant: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarCadaveresServants;
                        break;
                    case ETipoUnidad.Party: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarParties;
                        break;
                    case ETipoUnidad.PersonajeConAlianza: vmNuevaUndiad.ImagenPosicionEsVisible = mostrarAlianzas;
                        break;
                }

                Posiciones.Add(vmNuevaUndiad);
            }
        } 

		#endregion
	}
}