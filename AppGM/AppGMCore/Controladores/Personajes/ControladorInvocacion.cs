namespace AppGM.Core
{
    public class ControladorInvocacion<TipoInvocacion> : ControladorPersonaje<ModeloInvocacion>
    {
        #region Controladores

        public ControladorPersonaje<ModeloPersonaje> ControladorInvocador { get; set; }

        public ControladorEfecto<ModeloEfecto> ControladorEfecto { get; set; }

        #endregion

        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar
        }

        #endregion
    }

    public class ControladorInvocacionTemporal : ControladorInvocacion<ModeloInvocacionTemporal>
    {
        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Restar los turnos de vida que le quedan
        }

        #endregion
    }

    public class ControladorInvocacionCondicionada : ControladorInvocacion<ModeloInvocacionCondicionada>
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