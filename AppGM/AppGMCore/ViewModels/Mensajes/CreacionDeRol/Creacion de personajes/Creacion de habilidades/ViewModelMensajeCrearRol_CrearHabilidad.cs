using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_CrearHabilidad : ViewModelMensajeBase
    {
        #region Miembros

        private ViewModelMensajeCrearRol_CrearPersonaje mVMCrearPersonaje;
        
        #endregion

        #region Proopiedades

        public bool PuedeFinalizar => PuedeFinalizarCreacion();

        public ushort CostoDeMana { get; set; }

        public ETipoHabilidad TipoDeHabilidadSeleccionado { get; set; } = ETipoHabilidad.NINGUNO;
        public ERango RangoHabilidadSeleccionado          { get; set; }

        public ViewModelListaItems ContenedorListaEfectos          { get; set; }
        public ViewModelListaItems ContenedorListaItemsQueConsume  { get; set; }
        public ViewModelListaItems ContenedorListaCondiciones      { get; set; }
        public ViewModelListaItems ContenedorListaLimitadores      { get; set; }
        public ViewModelListaItems ContenedorListaTiradas          { get; set; }

        public ICommand ComandoFinalizar { get; set; }
        public ICommand ComandoCancelar  { get; set; }

        #endregion

        #region Constructor

        public ViewModelMensajeCrearRol_CrearHabilidad(ViewModelMensajeCrearRol_CrearPersonaje _vmCrearPersonaje)
        {
            mVMCrearPersonaje = _vmCrearPersonaje;

            ContenedorListaCondiciones     = new ViewModelListaItems(()=>{}, true, "Condiciones");
            ContenedorListaEfectos         = new ViewModelListaItems(()=>{}, true, "Efectos");
            ContenedorListaLimitadores     = new ViewModelListaItems(()=>{}, true, "Limitadores");
            ContenedorListaTiradas         = new ViewModelListaItems(()=>{}, true, "Tiradas");
            ContenedorListaItemsQueConsume = new ViewModelListaItems(()=>{}, true, "Items que consume");

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
