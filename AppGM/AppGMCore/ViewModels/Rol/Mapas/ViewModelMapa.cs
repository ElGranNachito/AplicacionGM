using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMapa : BaseViewModel
    {
        #region Miembros

        public ICommand                          ComandoAñadirParticipante { get; set; }
        public ObservableCollection<ViewModelIngresoPosicion>    Posiciones                { get; set; } = new ObservableCollection<ViewModelIngresoPosicion>();

        public ControladorMapa                   controladorMapa;

        #endregion

        #region Propiedades
        public string PathImagen { get; set; }
        public double TamañoCanvasX
        {
            get => TamañoCanvas.X.Round(1);
            set
            {
                TamañoCanvas.X = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MitadTamañoCanvasX)));
            }
        }

        public double TamañoCanvasY
        {
            get => TamañoCanvas.Y.Round(1);
            set
            {
                TamañoCanvas.Y = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MitadTamañoCanvasY)));
            }
        }

        public double MitadTamañoCanvasX => (TamañoCanvas.X / 2.0f).Round(1);
        public double MitadTamañoCanvasY => (TamañoCanvas.Y / 2.0f).Round(1);
        public ViewModelVector2 TamañoCanvas { get; set; } = new ViewModelVector2();

        //Tamaño de las imagenes
        public ViewModelVector2 TamañoImagenesPosicion { get; set; } = new ViewModelVector2(101.25, 138.75);

        public ViewModelVector2 MitadTamañoImagenesPosicion => new ViewModelVector2(
            -(TamañoImagenesPosicion.X / 2.0f).Round(1),
            -(TamañoImagenesPosicion.Y / 2.0f).Round(1));

        #endregion

        #region Constructores

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
        public ViewModelMapa()
        {
            controladorMapa = new ControladorMapa
            {
                modelo = new ModeloMapa
                {
                    EFormatoImagen = EFormatoImagen.Png,
                    NombreMapa = "Seoul",
                    PosicionesElementos = new List<TIMapaVector2>
                    {
                        new TIMapaVector2
                        {
                            Posicion = new ModeloVector2
                            {
                                X = 50,
                                Y = 150
                            }
                        }
                    }
                }
            };

            PathImagen = "../../../Media/Imagenes/Mapas/" +
                          controladorMapa.modelo.NombreMapa + controladorMapa.ObtenerExtension();

            ComandoAñadirParticipante = new Comando(AñadirUnidad);
        } 
        #endregion

        private async void AñadirUnidad()
        {
            ViewModelMensajeCrearUnidadMapa vm = new ViewModelMensajeCrearUnidadMapa(this);

            await SistemaPrincipal.Aplicacion.VentanaPopups.Mostrar(vm, true);

            if (vm.vmResultado is ViewModelIngresoPosicion vmNuevaUndiad)
                Posiciones.Add(vmNuevaUndiad);
        }
    }
}
