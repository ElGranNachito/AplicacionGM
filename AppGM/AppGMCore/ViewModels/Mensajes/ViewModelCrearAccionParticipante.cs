using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM para la creacion de la representacion de un participante en un <see cref="ViewModelCombate"/>.
    /// </summary>
    public class ViewModelCrearAccionParticipante : ViewModelConResultado<ViewModelCrearAccionParticipante>
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// VM del participante en el que se añadira la accion
        /// </summary>
        private ViewModelParticipante participante;

        /// <summary>
        /// VM de accion resultante de la creacion
        /// </summary>
        public ViewModelAccion vmResultado;


        // Propiedades ---


        /// <summary>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Finalizar'
        /// </summary>
        public ICommand ComandoFinalizar { get; set; }

        /// <summary>
        /// Comando que ejecutara cuando el usuario presione el boton 'Tirada'
        /// </summary>
        public ICommand ComandoTiradaHabilidad { get; set; }

        /// <summary>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Tirada con GuraScratch'
        /// </summary>
        public ICommand ComandoTiradaHabilidadGura { get; set; }

        /// <summary>
        /// VM del ComboBox con los distintos tipos de accion.
        /// </summary>
        public ViewModelComboBox<ETipoAccion> ComboBoxTipoAccion { get; set; } = new ViewModelComboBox<ETipoAccion>(EnumHelpers.TiposDeAccionesDisponibles);

        /// <summary>
        /// Tipo de accion que es seleccionada.
        /// </summary>
        public ETipoAccion TipoAccionSeleccionada { get; set; }

        /// <summary>
        /// Indica si la accion a crear va a consumir parte 
        /// </summary>
        public bool ConsumeLimiteTurno { get; set; } = true;

        /// <summary>
        /// Indica si se deben mostrar las opciones de tiradas en caso de que
        /// <see cref="TipoAccionSeleccionada"/> sea <see cref="ETipoAccion.Habilidad"/> o <see cref="ETipoAccion.Item"/> 
        /// </summary>
        public bool DebeMostrarTiradas { get; set; } = false;

        /// <summary>
        /// Descripcion de la accion.
        /// </summary>
        public string DescripcionAccion { get; set; } = string.Empty;

        /// <summary>
        /// Texto que muestra los caracteres restantes de la descripcion de la accion.
        /// </summary>
        public string TextoLetrasRestantesDescripcion => 2000 - DescripcionAccion.Length + "/2000";

        #endregion

        #region Constructor


        /// <summary>
        /// Constructor con parametro participante.
        /// </summary>
        /// <param name="_participante"></param>
        public ViewModelCrearAccionParticipante(ViewModelParticipante _participante)
        {
            participante = _participante;

            ComandoFinalizar = new Comando(GenerarViewModel);

            ComboBoxTipoAccion.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
                TipoAccionSeleccionada = actual.valor;

                if (TipoAccionSeleccionada == ETipoAccion.Habilidad || TipoAccionSeleccionada == ETipoAccion.Item)
                    DebeMostrarTiradas = true;
                else
                    DebeMostrarTiradas = false;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeMostrarTiradas)));
            };
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Crea el VM
        /// </summary>
        private void GenerarViewModel()
        {
            ModeloAccion modeloAccion = new ModeloAccion
            {
                TipoAccion = TipoAccionSeleccionada,
                ConsumeTurno = ConsumeLimiteTurno,
                Descripcion = DescripcionAccion
            };

            if (participante.controladorParticipante.modelo.AccionesRestantes >= participante.controladorParticipante.modelo.TotalAccionesPorTurno)
            {
                MensajeHelpers.MostrarMensajeConfirmacionAsync("¡¡¡Ojo!!!",
                    "Ya se alcanzo el numero maximo de acciones posibles por turno para este participante.");

                return;
            }

            modeloAccion.Participante = participante.controladorParticipante.modelo;

            participante.controladorParticipante.AñadirAccion(modeloAccion);

            ControladorAccion controlador = new ControladorAccion(modeloAccion);
            
            vmResultado = new ViewModelAccion(controlador, participante);

            SistemaPrincipal.GuardarModelo(modeloAccion);

            SistemaPrincipal.GuardarDatosAsync();

            Resultado = EResultadoViewModel.Aceptar;
        }

        #endregion
    }
}
