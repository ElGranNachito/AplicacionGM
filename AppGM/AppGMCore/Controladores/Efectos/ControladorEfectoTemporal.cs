using AppGMCore;

namespace AppGMCore
{
    class ControladorEfectoTemporal : ControladorEfecto<ModeloEfectoTemporal>
    {
        public ushort TurnosRestantes { get; set; }

        #region Eventos

        public delegate void dQuitarEfecto(ControladorPersonaje<ModeloPersonaje> instigador, ControladorPersonaje<ModeloPersonaje>[] objetivos,
            ControladorEfecto<ModeloEfecto> efectoAplicado);

        public event dQuitarEfecto OnQuitarEfecto = delegate { };

        public delegate void dReducirTurno(ControladorPersonaje<ModeloPersonaje> instigador,
            ControladorPersonaje<ModeloPersonaje>[] objetivos, ControladorEfecto<ModeloEfecto> efectoAplicado);

        public event dReducirTurno OnReducirEfecto = delegate { };

        #endregion

        #region Funciones

        public override void AplicarEfecto(ControladorPersonaje<ModeloPersonaje> p)
        {
            base.AplicarEfecto(p);
        }
        public void AlPasarTurno()
        {
            //TODO: Disminuir turnos restantes
        }

        #endregion
    }
}
