namespace AppGM.Core
{
    public class ControladorInvocacion<TipoInvocacion> : ControladorPersonaje<TipoInvocacion>
        where TipoInvocacion : ModeloInvocacion, new()
    {
        #region Controladores

        public ControladorPersonaje<ModeloPersonaje> ControladorInvocador { get; set; }

        public ControladorEfecto<ModeloEfecto> ControladorEfecto { get; set; }

        #endregion

        #region Constructor

        public ControladorInvocacion()
        {
        }

        public ControladorInvocacion(ModeloInvocacion _modeloInvocacion)
        {
            modelo = (TipoInvocacion) _modeloInvocacion;
        }

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
        #region Constructor

        public ControladorInvocacionTemporal(ModeloInvocacionTemporal _modeloInvocacionTemporal)
        {
            modelo = _modeloInvocacionTemporal;
        }

        #endregion

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

        #region Constructor

        public ControladorInvocacionCondicionada(ModeloInvocacionCondicionada _modeloInvocacionCondicionada)
        {
            modelo = _modeloInvocacionCondicionada;
        }

        #endregion

        #region Funciones

        public override void AvanzarTurno()
        {
            //TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Revisar si se cumplen las condiciones para que desaparezca
        }

        #endregion
    }
}