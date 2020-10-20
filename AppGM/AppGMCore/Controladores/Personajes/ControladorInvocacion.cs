using System;

namespace AppGM.Core
{
    public class ControladorInvocacion : ControladorPersonaje
    {
        #region Controladores

        public ControladorPersonaje ControladorInvocador { get; set; }

        public ControladorEfecto ControladorEfecto { get; set; }

        #endregion

        #region Constructores
        public ControladorInvocacion(ModeloPersonaje _modeloInvocacion) : base(_modeloInvocacion) { } 
        #endregion

        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar
        }

        #endregion
    }

    public class ControladorInvocacionTemporal : ControladorInvocacion
    {
        #region Constructores
        public ControladorInvocacionTemporal(ModeloInvocacionTemporal _modeloInvocacionTemporal) : base(_modeloInvocacionTemporal) { } 
        #endregion

        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Restar los turnos de vida que le quedan
        }

        #endregion
    }

    public class ControladorInvocacionCondicionada : ControladorInvocacion
    {
        public Func<ControladorPersonaje, bool> DebeDesaparecer;

        public ControladorInvocacionCondicionada(ModeloInvocacionCondicionada _modeloInvocacionCondicionada) : base(_modeloInvocacionCondicionada){}

        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Revisar si se cumplen las condiciones para que desaparezca
        }

        #endregion
    }
}