using System.CodeDom;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorAdministradorDeCombate : ControladorBase<ModeloAdministradorDeCombate>
    {
        #region Controladores
        public List<ControladorParticipante> ControladoresParticipantes { get; set; }

        #endregion

        #region Eventos

        public delegate void dAvanzarTurno(ref int TurnoActual);

        public event dAvanzarTurno OnAvanzarTurno = delegate { };

        public delegate void dRetrocederTurno(ref int TurnoActual);

        public event dRetrocederTurno OnRetrocederTurno = delegate { };

        public delegate void dAvanzarTurnoPersonaje(ModeloAdministradorDeCombate modeloAdministradorDeCombate);

        public event dAvanzarTurnoPersonaje OnAvanzarTurnoPersonaje = delegate { };

        #endregion

        #region Constructores
        public ControladorAdministradorDeCombate(ModeloAdministradorDeCombate _modeloAdministradorCombate)
        {
            modelo = _modeloAdministradorCombate;

            for (int i = 0; i < modelo.Participantes.Count; ++i) 
                ControladoresParticipantes.Add(new ControladorParticipante(modelo.Participantes[i].Participante));
        }

        /// <summary>
        /// Constructor default, solo para pruebas
        /// </summary>
        public ControladorAdministradorDeCombate(){}

        #endregion

        #region Funciones

        public void AvanzarTurno()
        {
            if (modelo.IndicePersonajeTurnoActual == modelo.Participantes.Count - 1)
                modelo.IndicePersonajeTurnoActual = 0;
            else
                ++modelo.IndicePersonajeTurnoActual;

            int turnoActualTmp = modelo.IndicePersonajeTurnoActual;

            OnAvanzarTurno(ref turnoActualTmp);

            modelo.IndicePersonajeTurnoActual = turnoActualTmp;
        }

        public void RetrocederTurno()
        {
            if (modelo.IndicePersonajeTurnoActual == 0)
                modelo.IndicePersonajeTurnoActual = modelo.Participantes.Count - 1;
            else
                --modelo.IndicePersonajeTurnoActual;

            int turnoActualTmp = modelo.IndicePersonajeTurnoActual;

            OnRetrocederTurno(ref turnoActualTmp);

            modelo.IndicePersonajeTurnoActual = turnoActualTmp;
        }

        #endregion
    }
}