﻿using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorAdministradorDeCombate : Controlador<ModeloAdministradorDeCombate>
    {
        #region Miembros
        public List<ControladorParticipante> ControladoresParticipantes { get; set; } = new List<ControladorParticipante>();
        public List<ControladorMapa> ControladoresMapas { get; set; } = new List<ControladorMapa>();

        #endregion

        #region Eventos

        /// <summary>
        /// Representa un metodo que lidia con el cambio de turnos durante el combate
        /// </summary>
        /// <param name="TurnoActual"></param>
        public delegate void dTurnoCambio(ref int TurnoActual);

        /// <summary>
        /// Evento que se dispara cuando se cambia de turno
        /// </summary>
        public event dTurnoCambio OnTurnoCambio = delegate { };

        /// <summary>
        /// Representa un metodo que lidia con el avance de turno del personaje
        /// </summary>
        /// <param name="modeloAdministradorDeCombate"></param>
        public delegate void dAvanzarTurnoPersonaje(ModeloAdministradorDeCombate modeloAdministradorDeCombate);

        /// <summary>
        /// Evento que se dispara cuando se avanza con el turno de un personaje
        /// </summary>
        public event dAvanzarTurnoPersonaje OnAvanzarTurnoPersonaje = delegate { };

        #endregion

        #region Constructores
        public ControladorAdministradorDeCombate(ModeloAdministradorDeCombate _modeloAdministradorCombate)
			:base(_modeloAdministradorCombate)
        {
	        for (int i = 0; i < modelo.Participantes.Count; ++i) 
                ControladoresParticipantes.Add(modelo.Participantes[i].Participante.controladorParticipante);

            for (int i = 0; i < modelo.Mapas.Count; ++i)
                ControladoresMapas.Add(modelo.Mapas[i].Mapa.controladorMapa);
        }

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