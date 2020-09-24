﻿namespace AppGM.Core
{
    class ControladorInvocacion<TipoInvocacion> : ControladorPersonaje<ModeloInvocacion>
    {
        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar
        }

        #endregion
    }

    class ControladorInvocacionTemporal : ControladorInvocacion<ModeloInvocacionTemporal>
    {
        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Restar los turnos de vida que le quedan
        }

        #endregion
    }

    class ControladorInvocacionCondicionada : ControladorInvocacion<ModeloInvocacionCondicionada>
    {
        //public Func<ControladorPersonaje<ModeloPersonaje>, bool> DebeDesaparecer;

        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Revisar si se cumplen las condiciones para que desaparezca
        }

        #endregion
    }
}