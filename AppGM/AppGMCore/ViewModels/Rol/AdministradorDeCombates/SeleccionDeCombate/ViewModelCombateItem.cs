using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelCombateItem : BaseViewModel
    {
        #region Propiedades
        public ControladorAdministradorDeCombate Combate { get; set; }
        public ICommand ComandoClickeado { get; set; }
        public ICommand ComandoMouseEnter { get; set; }
        public ICommand ComandoMouseLeave { get; set; }

        #endregion

        #region Constructores
        public ViewModelCombateItem(ControladorAdministradorDeCombate _combate)
        {
            Combate = _combate;

            EstablecerComandos();
        }

        #endregion

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

                    SistemaPrincipal.RolSeleccionado.EMenuActual = EMenuActualRol.Combate;
                });
        }

        public string Nombre    => Combate.modelo.Nombre;
        public uint TurnoActual => Combate.modelo.TurnoActual;
        public int CantidadParticipantes => Combate.modelo.Participantes.Count;
    }

}
