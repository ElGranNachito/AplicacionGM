using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    class ViewModelMensajeCrearRol_CrearHabilidad : ViewModelMensajeBase
    {
        #region Miembros

        private ViewModelMensajeCrearRol_CrearPersonaje mVMCrearPersonaje;
        
        #endregion

        #region Proopiedades

        public bool PuedeFinalizar => PuedeFinalizarCreacion();

        public ushort CostoDeMana { get; set; }

        public ETipoHabilidad TipoDeHabilidadSeleccionado { get; set; } = ETipoHabilidad.NINGUNO;
        public ERango RangoHabilidadSeleccionado { get; set; }

        public ViewModelListaItems ListaEfectos { get; set; }
        public ViewModelListaItems ListaItemsQueConsume { get; set; }
        public ViewModelListaItems ListaCondiciones { get; set; }
        public ViewModelListaItems ListaLimitadores { get; set; }
        public ViewModelListaItems ListaTiradas { get; set; }

        public ICommand ComandoFinalizar { get; set; }
        public ICommand ComandoCancelar { get; set; }

        #endregion

        #region Constructor

        public ViewModelMensajeCrearRol_CrearHabilidad(ViewModelMensajeCrearRol_CrearPersonaje _vmCrearPersonaje)
        {
            mVMCrearPersonaje = _vmCrearPersonaje;

            ListaCondiciones     = new ViewModelListaItems(()=>{}, true, "Condiciones");
            ListaEfectos         = new ViewModelListaItems(()=>{}, true, "Efectos");
            ListaLimitadores     = new ViewModelListaItems(()=>{}, true, "Limitadores");
            ListaTiradas         = new ViewModelListaItems(()=>{}, true, "Tiradas");
            ListaItemsQueConsume = new ViewModelListaItems(()=>{}, true, "Items que consume");

            ComandoCancelar = new Comando(() =>
            {
                SistemaPrincipal.Aplicacion.VentanaPopups.EstablecerViewModel(mVMCrearPersonaje);
            });

            PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName != nameof(PuedeFinalizar))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizar)));
            };
        }

        #endregion

        #region Funciones

        private void FinalizarCreacion()
        {
            
        }

        private bool PuedeFinalizarCreacion()
        {
            return false;
        } 

        #endregion
    }
}
