using System.Collections.Generic;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM delmenu de seleccion de combates
    /// </summary>
    public class ViewModelMenuSeleccionCombate : ViewModel
    {
        #region Propiedades

        /// <summary>
        /// Comando que se ejecuta al presionar el boton 'Agregar'.
        /// </summary>
        public ICommand ComandoAgregarCombate { get; set; }

        /// <summary>
        /// Lista de combates existentes
        /// </summary>
        public ViewModelListaCombates Combates { get; set; }

        /// <summary>
        /// Globo que muestra la informacion del combate que el usuario tiene actualmente seleccionado
        /// </summary>
        public ViewModelGlobo<ViewModelInfoCombateGlobo> GloboInfoCombate { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_combates">Lista que contiene controladores de los administradores de combate existentes</param>
        public ViewModelMenuSeleccionCombate(List<ControladorAdministradorDeCombate> _combates)
        {
            Combates = new ViewModelListaCombates(_combates);

            GloboInfoCombate = new ViewModelGlobo<ViewModelInfoCombateGlobo>
            {
                ViewModelContenido = new ViewModelInfoCombateGlobo(),
                GloboVisible = false
            };

            ComandoAgregarCombate = new Comando(AgregarCombate);
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion llamada para agregar un nuevo participante al combate
        /// </summary>
        public async void AgregarCombate()
        {
            //VM para el contenido del popup
            ViewModelCrearCombate vm = new ViewModelCrearCombate();

            //Se crea el popup y se espera a que se cierre
            await SistemaPrincipal.MostrarMensajeAsync(vm, "Agregar combate", true, 150, 500);

            Combates.Combates.Add(new ViewModelCombateItem(vm.vmResultado.administradorDeCombate));

            await SistemaPrincipal.GuardarDatosAsync();
        }

        #endregion
    }
}