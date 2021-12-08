using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa la creacion de un <see cref="ModeloAdministradorDeCombate"/>
    /// </summary>
    public class ViewModelCrearCombate : ViewModelConResultado<ViewModelCrearCombate>
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// Mapa actualmente seleccionado
        /// </summary>
        private ControladorMapa MapaSeleccionado;

        /// <summary>
        /// Controlador del combate a crear.
        /// </summary>
        public ControladorAdministradorDeCombate controlador;


        // Propiedades ---


        /// <summary>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Finalizar'
        /// </summary>
        public ICommand ComandoFinalizar { get; set; }

        /// <summary>
        /// Cantidad inicial de acciones que puede realizar por turno.
        /// </summary>
        public string NombreCombate { get; set; } = string.Empty;

        /// <summary>
        /// Indica si combate sigue activo.
        /// De ser falso se toma como concluido o en receso. 
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        /// <summary>
        /// Indica si la creacion puede finalizarse.
        /// </summary>
        public bool PuedeFinalizarCreacion
        {
            get
            {
                if (NombreCombate == string.Empty)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// VM del ComboBox con los mapas disponibles a seleccionar.
        /// </summary>
        public ViewModelComboBox<ControladorMapa> ComboBoxMapas { get; set; } = new ViewModelComboBox<ControladorMapa>(SistemaPrincipal.DatosRolSeleccionado.Mapas);

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_combate">Combate al que se agregara al participante</param>
        public ViewModelCrearCombate()
        {
            ComandoFinalizar = new Comando(GenerarViewModel);

            PropertyChanged += (obj, e) =>
            {
                //Si la propiedad no es el tipo sileccionado ni si podemos finalizar la creacion...
                if(e.PropertyName != nameof(PuedeFinalizarCreacion))
                    //Disparamos el evento property changed en PuedeFinalizarCreacion
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizarCreacion)));
            };

            ComboBoxMapas.OnValorSeleccionadoCambio += (anterior, actual) => MapaSeleccionado = actual.valor;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Crea el VM
        /// </summary>
        private async void GenerarViewModel()
        {
            ModeloAdministradorDeCombate modeloAdministradorDeCombate = new ModeloAdministradorDeCombate
            {
	            Nombre = NombreCombate,
                EstaActivo = EstaActivo
            };

            modeloAdministradorDeCombate.Mapas.Add(MapaSeleccionado.modelo);

            SistemaPrincipal.ModeloRolActual.Combates.Add(modeloAdministradorDeCombate);

            controlador = new ControladorAdministradorDeCombate(modeloAdministradorDeCombate);

            SistemaPrincipal.DatosRolSeleccionado.CombatesActivos.Add(controlador);

            SistemaPrincipal.GuardarModelo(modeloAdministradorDeCombate);

            await SistemaPrincipal.GuardarDatosAsync();

            Resultado = EResultadoViewModel.Aceptar;
        }

        #endregion
    }
}
