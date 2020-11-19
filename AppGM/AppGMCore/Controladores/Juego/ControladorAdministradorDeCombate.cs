using System.CodeDom;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorAdministradorDeCombate : Controlador<ModeloAdministradorDeCombate>
    {
        #region Miembros
        public List<ControladorParticipante> ControladoresParticipantes { get; set; } = new List<ControladorParticipante>();
        public List<ControladorMapa> ControladoresMapas { get; set; } = new List<ControladorMapa>();

        #endregion

        #region Eventos

        public delegate void dTurnoCambio(ref int TurnoActual);

        public event dTurnoCambio OnTurnoCambio = delegate { };

        public delegate void dAvanzarTurnoPersonaje(ModeloAdministradorDeCombate modeloAdministradorDeCombate);

        public event dAvanzarTurnoPersonaje OnAvanzarTurnoPersonaje = delegate { };

        #endregion

        #region Constructores
        public ControladorAdministradorDeCombate(ModeloAdministradorDeCombate _modeloAdministradorCombate)
        {
            modelo = _modeloAdministradorCombate;

            for (int i = 0; i < modelo.Participantes.Count; ++i) 
                ControladoresParticipantes.Add(modelo.Participantes[i].Participante.controladorParticipante);

            for (int i = 0; i < modelo.Mapas.Count; ++i)
                ControladoresMapas.Add(modelo.Mapas[i].Mapa.controladorMapa);
        }

        /// <summary>
        /// Constructor default, solo para pruebas
        /// </summary>
        public ControladorAdministradorDeCombate(){}

        #endregion

        #region Funciones

        public void AvanzarTurno()
        {
            if (modelo.IndicePersonajeTurnoActual >= modelo.Participantes.Count)
                modelo.IndicePersonajeTurnoActual = 0;
            else
                ++modelo.IndicePersonajeTurnoActual;

            int turnoActualTmp = modelo.IndicePersonajeTurnoActual;

            OnTurnoCambio(ref turnoActualTmp);

            modelo.IndicePersonajeTurnoActual = turnoActualTmp;
        }

        public void RetrocederTurno()
        {
            if (modelo.IndicePersonajeTurnoActual < 0)
                modelo.IndicePersonajeTurnoActual = modelo.Participantes.Count - 1;
            else
                --modelo.IndicePersonajeTurnoActual;

            int turnoActualTmp = modelo.IndicePersonajeTurnoActual;

            OnTurnoCambio(ref turnoActualTmp);

            modelo.IndicePersonajeTurnoActual = turnoActualTmp;
        }

        #endregion
    }
}