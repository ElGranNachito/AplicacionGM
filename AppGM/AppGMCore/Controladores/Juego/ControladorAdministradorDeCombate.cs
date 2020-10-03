﻿namespace AppGM.Core
{
    public class ControladorAdministradorDeCombate : ControladorBase<ModeloAdministradorDeCombate>
    {
        #region Controladores

        public ControladorParticipante ControladorParticipante { get; set; }

        #endregion

        #region Eventos

        public delegate void dAvanzarTurno(ref uint TurnoActual);

        public event dAvanzarTurno OnAvanzarTurno = delegate { };

        public delegate void dAvanzarTurnoPersonaje(ModeloAdministradorDeCombate modeloAdministradorDeCombate);

        public event dAvanzarTurnoPersonaje OnAvanzarTurnoPersonaje = delegate { };

        #endregion

        #region Funciones

        private void AvanzarTurno()
        {
        }

        #endregion
    }
}