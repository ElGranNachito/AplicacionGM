using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_CrearHabilidad : ViewModelMensajeBase
    {
        #region Miembros

        private ViewModelMensajeCrearRol_CrearPersonaje mVMCrearPersonaje;
        private ModeloPersonaje                         mModeloPersonaje;

        private string mCostoDeMana = "0";

        #endregion

        #region Propiedades

        public string TextoNivelMagia => $"Lv.{ObtenerNivelDeMagia()}";

        public bool PuedeFinalizar => PuedeFinalizarCreacion();
        public bool EsMagia        => VMSeleccionTipoHabilidad.OpcionSeleccionada == ETipoHabilidad.Magia;
        public bool PuedeElegirSiEsMagiaParticular => PuedeAñadirMagiasParticulares();
        public bool RequiereRango           { get; set; }
        public bool PuedeElegirSiTieneRango { get; set; }

        public string CostoDeMana
        {
            get => mCostoDeMana;
            set
            {
                mCostoDeMana = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
            }
        }

        public ViewModelListaItems ContenedorListaEfectos          { get; set; }
        public ViewModelListaItems ContenedorListaItemsQueConsume  { get; set; }
        public ViewModelListaItems ContenedorListaCondiciones      { get; set; }
        public ViewModelListaItems ContenedorListaLimitadores      { get; set; }
        public ViewModelListaItems ContenedorListaTiradas          { get; set; }

        public ViewModelComboBoxConDescripcion<ETipoHabilidad> VMSeleccionTipoHabilidad  { get; set; } = new ViewModelComboBoxConDescripcion<ETipoHabilidad>();
        public ViewModelComboBoxConDescripcion<ERango>         VMSeleccionRangoHabilidad { get; set; } = new ViewModelComboBoxConDescripcion<ERango>();

        public ICommand ComandoFinalizar { get; set; }
        public ICommand ComandoCancelar  { get; set; }

        #endregion

        #region Constructor

        public ViewModelMensajeCrearRol_CrearHabilidad(ViewModelMensajeCrearRol_CrearPersonaje _vmCrearPersonaje, ModeloPersonaje _modeloPersonaje)
        {
            mVMCrearPersonaje = _vmCrearPersonaje;
            mModeloPersonaje  = _modeloPersonaje;

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

            VMSeleccionTipoHabilidad.PropertyChanged += (sender, args) =>
            {
                RequiereRango           = VMSeleccionTipoHabilidad.OpcionSeleccionada == ETipoHabilidad.NoblePhantasm;
                PuedeElegirSiTieneRango = !RequiereRango && !EsMagia;

                if (EsMagia)
                {
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMagia)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeElegirSiEsMagiaParticular)));
                }
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

        private bool PuedeAñadirMagiasParticulares()
        {
            if (!EsMagia || mModeloPersonaje.TipoPersonaje != ETipoPersonaje.Master)
                return false;

            if (mModeloPersonaje.Magias.Count(ti => ti.Magia.EsParticular) >= 2)
                return false;

            return true;
        }

        private byte ObtenerNivelDeMagia()
        {
            ushort costo = ushort.Parse(CostoDeMana);

            if (costo < 10)
                return 0;
            if (costo < 20)
                return 1;
            if (costo < 40)
                return 2;
            if (costo < 50)
                return 3;
            if (costo < 100)
                return 4;
            if (costo < 150)
                return 5;
            if (costo < 200)
                return 6;
            if (costo < 250)
                return 7;

            return 8;
        }

        #endregion
    }
}
