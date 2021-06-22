using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que facilita propiedades comunes de un <see cref="ModeloAdministradorDeCombate"/> para mostrar en forma de resumen
    /// </summary>
    public class ViewModelCombateItem : ViewModel
    {
        #region Propiedades

        /// <summary>
        /// Controlador del combate
        /// </summary>
        /// TODO: Pasar esto a que sea directamente un modelo ya que no necesitamos de las funciones del controlador
        public ControladorAdministradorDeCombate Combate { get; set; }

        /// <summary>
        /// Comando que se ejecutara cuando el usuario haga click sobre este item
        /// </summary>
        public ICommand ComandoClickeado { get; set; }

        /// <summary>
        /// Comando que se ejecutara cuando el cursor se pose sobre este item
        /// </summary>
        public ICommand ComandoMouseEnter { get; set; }

        /// <summary>
        /// Comando que se ejecutara cuando el cursor deje de estar sobre este item
        /// </summary>
        public ICommand ComandoMouseLeave { get; set; }

        /// <summary>
        /// Nombre del combate actual
        /// </summary>
        public string Nombre => Combate.modelo.Nombre;

        /// <summary>
        /// Turno en el que se encuentra el combate
        /// </summary>
        public uint TurnoActual => Combate.modelo.TurnoActual;

        /// <summary>
        /// Cantidad de participantes del combate
        /// </summary>
        public int CantidadParticipantes => Combate.modelo.Participantes.Count;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_combate">Controlador del <see cref="ModeloAdministradorDeCombate"/> que representa este item</param>
        public ViewModelCombateItem(ControladorAdministradorDeCombate _combate)
        {
            Combate = _combate;

            EstablecerComandos();
        }

        #endregion

        /// <summary>
        /// Establece los comandos
        /// </summary>
        private void EstablecerComandos()
        {
            ComandoMouseEnter = new Comando(
                () =>
                {
                    SistemaPrincipal.MenuSeleccionCombate.GloboInfoCombate.ViewModelContenido.Combate = Combate.modelo;

                    SistemaPrincipal.MenuSeleccionCombate.GloboInfoCombate.GloboVisible = true;
                });
            ComandoMouseLeave = new Comando(
                () =>
                {
                    SistemaPrincipal.MenuSeleccionCombate.GloboInfoCombate.ViewModelContenido.Combate = null;

                    SistemaPrincipal.MenuSeleccionCombate.GloboInfoCombate.GloboVisible = false;
                }
            );

            ComandoClickeado = new Comando(
                () =>
                {
                    SistemaPrincipal.CombateActual.ActualizarCombateActual(Combate);

                    SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.Combate;
                });
        }
    }

}
