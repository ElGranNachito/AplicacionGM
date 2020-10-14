using System.Collections.Generic;
using System.Text;
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
        public ViewModelCombateItem()
        {
            Combate = new ControladorAdministradorDeCombate
            {
                modelo = new ModeloAdministradorDeCombate
                {
                    IndicePersonajeTurnoActual = 2,
                    Nombre = "Combate super feroz",
                    Participantes = new List<TIAdministradorDeCombateParticipante>
                    {
                        new TIAdministradorDeCombateParticipante
                        {
                            Participante = new ModeloParticipante
                            {
                                Personaje = new TIParticipantePersonaje
                                {
                                    Personaje = new ModeloPersonaje
                                    {
                                        Nombre = "Juanito"
                                    }
                                }
                            }
                        }
                    }
                }
            };

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
        #endregion
    }

}
