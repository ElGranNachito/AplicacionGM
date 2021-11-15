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
        /// VM de participante resultante de la creacion
        /// </summary>
        public ViewModelCombate vmResultado;


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
        public bool EstaActivo { get; set; }

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
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Crea el VM
        /// </summary>
        private void GenerarViewModel()
        {
            ModeloAdministradorDeCombate modeloAdministradorDeCombate = new ModeloAdministradorDeCombate
            {
	            Nombre = NombreCombate,
                EstaActivo = EstaActivo
            };

            ControladorAdministradorDeCombate controlador = new ControladorAdministradorDeCombate(modeloAdministradorDeCombate);

            SistemaPrincipal.DatosRolSeleccionado.CombatesActivos.Add(controlador);

            vmResultado = new ViewModelCombate();

            vmResultado.ActualizarCombateActual(controlador);

            Resultado = EResultadoViewModel.Aceptar;
        }

        #endregion
    }
}
