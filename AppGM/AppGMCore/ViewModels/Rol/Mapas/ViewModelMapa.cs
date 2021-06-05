using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un mapa
    /// </summary>
    public class ViewModelMapa : BaseViewModel
    {
        #region Campos & Propiedades

        //------------------------------------CAMPOS-------------------------------------

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
        public ObservableCollection<ViewModelIngresoPosicion> Posiciones { get; set; } = new ObservableCollection<ViewModelIngresoPosicion>();

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

            //Creamos los view models para el ingreso de las diferentes posiciones
            for(int i = 0; i < controladorMapa.controladoresUnidadesMapa.Count; ++i)
                Posiciones.Add(new ViewModelIngresoPosicion(this, controladorMapa.controladoresUnidadesMapa[i]));

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
				Posiciones.Add(vmNuevaUndiad);
		} 

		#endregion
	}
}