using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa un <see cref="ModeloAdministradorDeCombate"/>
    /// </summary>
    public class ViewModelCombate : BaseViewModel
    {
        #region Campos & Propiedades

        //-----------------------------------CAMPOS-------------------------------------------


        private int indiceMapaActual = 0;

        public ControladorAdministradorDeCombate administradorDeCombate;

        public ControladorAdministradorDeCombate.dTurnoCambio HandlerTurnoCambio = delegate{};


        //--------------------------------PROPIEDADES------------------------------------------

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton de avanzar turno
        /// </summary>
        public ICommand ComandoAvanzarTurno { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton de retroceder turno
        /// </summary>
        public ICommand ComandoRetrocederTurno { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton 'Salir'
        /// </summary>
        public ICommand ComandoSalir { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el boton 'Tirada'
        /// </summary>
        public ICommand ComandoTirada { get; set; }

        /// <summary>
        /// Turno actual del combate
        /// </summary>
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
        public List<ViewModelMapa> Mapas { get; set; } = new List<ViewModelMapa>();

        /// <summary>
        /// Mapa actualmente siendo mostrado
        /// </summary>
        public ViewModelMapa MapaActual => Mapas[indiceMapaActual];

        #endregion

        #region Constructores
        public ViewModelCombate()
        {
	        HandlerTurnoCambio = (ref int turno) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TurnoActual)));

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
            //Desubscribimos los delegado del controlador anterior
            if(administradorDeCombate != null)
				administradorDeCombate.OnTurnoCambio    -= HandlerTurnoCambio;

            administradorDeCombate = _administradorDeCombate;

            Participantes = new ViewModelListaParticipantes(_administradorDeCombate.ControladoresParticipantes, this);

            for(int i = 0; i < administradorDeCombate.ControladoresMapas.Count; ++i)
                Mapas.Add(new ViewModelMapa(administradorDeCombate.ControladoresMapas[i]));

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MapaActual)));

            administradorDeCombate.OnTurnoCambio    += HandlerTurnoCambio;
        } 

        #endregion
    }
}
