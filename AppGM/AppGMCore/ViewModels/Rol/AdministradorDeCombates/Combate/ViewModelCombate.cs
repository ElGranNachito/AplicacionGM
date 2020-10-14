using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelCombate : BaseViewModel
    {
        #region Miembros

        public ControladorAdministradorDeCombate administradorDeCombate = new ControladorAdministradorDeCombate();

        public ControladorAdministradorDeCombate.dAvanzarTurno HandlerAvanzarTurno;
        public ControladorAdministradorDeCombate.dRetrocederTurno HandlerRetrocederTurno;

        #endregion

        #region Propiedades
        public ICommand ComandoAvanzarTurno { get; set; }
        public ICommand ComandoRetrocederTurno { get; set; }
        public ICommand ComandoSalir { get; set; }
        public ICommand ComandoTirada { get; set; }

        public uint TurnoActual => administradorDeCombate.modelo.TurnoActual;

        public ViewModelListaParticipantes Participantes { get; set; }

        #endregion

        #region Constructores
        public ViewModelCombate()
        {
            ComandoAvanzarTurno    = new Comando(administradorDeCombate.AvanzarTurno);
            ComandoRetrocederTurno = new Comando(administradorDeCombate.RetrocederTurno);

            HandlerAvanzarTurno = (ref int turno) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TurnoActual)));
            };

            HandlerRetrocederTurno = (ref int turno) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TurnoActual)));
            };
        }

        #endregion

        #region Funciones
        public void ActualizarCombateActual(ControladorAdministradorDeCombate _administradorDeCombate)
        {
            //Desuscribimos los delegado del controlador anterior
            administradorDeCombate.OnAvanzarTurno    -= HandlerAvanzarTurno;
            administradorDeCombate.OnRetrocederTurno -= HandlerRetrocederTurno;

            administradorDeCombate = _administradorDeCombate;

            Participantes = new ViewModelListaParticipantes(_administradorDeCombate.ControladoresParticipantes);

            administradorDeCombate.OnAvanzarTurno    += HandlerAvanzarTurno;
            administradorDeCombate.OnRetrocederTurno += HandlerRetrocederTurno;
        } 

        #endregion
    }
}
