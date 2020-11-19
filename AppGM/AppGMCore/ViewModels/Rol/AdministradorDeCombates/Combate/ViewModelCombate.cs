using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelCombate : BaseViewModel
    {
        #region Miembros

        public ControladorAdministradorDeCombate administradorDeCombate = new ControladorAdministradorDeCombate();

        public ControladorAdministradorDeCombate.dTurnoCambio HandlerTurnoCambio = delegate{};

        #endregion

        #region Propiedades
        public ICommand ComandoAvanzarTurno { get; set; }
        public ICommand ComandoRetrocederTurno { get; set; }
        public ICommand ComandoSalir { get; set; }
        public ICommand ComandoTirada { get; set; }

        public uint TurnoActual => administradorDeCombate.modelo.TurnoActual;

        /// <summary>
        /// Participantes del combate
        /// </summary>
        public ViewModelListaParticipantes Participantes { get; set; }

        /// <summary>
        /// Participante de quien es el turno actual
        /// </summary>
        public ViewModelParticipante ParticipanteTurnoActual { get; set; }

        /// <summary>
        /// Mapas del combate
        /// </summary>
        public List<ViewModelMapa> Mapas { get; set; }

        #endregion

        #region Constructores
        public ViewModelCombate()
        {
            ComandoAvanzarTurno    = new Comando(administradorDeCombate.AvanzarTurno);
            ComandoRetrocederTurno = new Comando(administradorDeCombate.RetrocederTurno);

            HandlerTurnoCambio = (ref int turno) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TurnoActual)));

                if (turno >= Participantes.Participantes.Count)
                    turno = 0;

                ParticipanteTurnoActual = Participantes.Participantes[turno];
            };
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Actualiza el combate actual borrando los datos del anterior
        /// </summary>
        /// <param name="_administradorDeCombate"></param>
        public void ActualizarCombateActual(ControladorAdministradorDeCombate _administradorDeCombate)
        {
            //Desuscribimos los delegado del controlador anterior
            administradorDeCombate.OnTurnoCambio    -= HandlerTurnoCambio;

            administradorDeCombate = _administradorDeCombate;

            Participantes = new ViewModelListaParticipantes(_administradorDeCombate.ControladoresParticipantes, this);
           
            for(int i = 0; i < administradorDeCombate.ControladoresMapas.Count; ++i)
                Mapas.Add(new ViewModelMapa(administradorDeCombate.ControladoresMapas[i]));

            administradorDeCombate.OnTurnoCambio    += HandlerTurnoCambio;
        } 

        #endregion
    }
}
