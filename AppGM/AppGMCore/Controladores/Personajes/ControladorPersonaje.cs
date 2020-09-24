
namespace AppGM.Core
{
    public class ControladorPersonaje<TipoPersonaje> : ControladorBase<ModeloPersonaje>
    {

        #region Eventos

        public delegate void dSufrirDaño(ref int cantidad, ETipoDeDaño eTipo, ControladorPersonaje<ModeloPersonaje> objetivo, ControladorPersonaje<ModeloPersonaje> instigador);

        public event dSufrirDaño OnSufrirDaño = delegate { };

        public delegate void dModificarInventario(ControladorUtilizable<ModeloUtilizable>[] item, ControladorPersonaje<ModeloPersonaje> objetivo);

        public event dModificarInventario OnAñadirItem = delegate { };
        public event dModificarInventario OnQuitarItem = delegate { };

        public delegate void dModificarAliados(ControladorPersonaje<ModeloPersonaje>[] aliado, ControladorPersonaje<ModeloPersonaje> objetivo);

        public event dModificarAliados OnAñadirAliado = delegate { };
        public event dModificarAliados OnQuitarAliado = delegate { };

        public delegate void dRecibirEfecto(ControladorEfecto<ModeloPersonaje> efectoRecibido, ControladorPersonaje<ModeloPersonaje> objetivo, ControladorPersonaje<ModeloPersonaje>[] instigadores);

        public event dRecibirEfecto OnRecibirEfecto = delegate { };

        public delegate void dUtilizarHabilidad(ETipoHabilidad eTipo, string NombreHabilidad, ControladorPersonaje<ModeloPersonaje>[] Objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        public delegate void dMoverse(Vector2D posicionAntigua, ref Vector2D posicionNueva);

        public event dMoverse OnMoverse = delegate { };

        public delegate void dMorir(ControladorPersonaje<ModeloPersonaje> objetivo);

        public event dMorir OnMorir = delegate { };

        public delegate void dRealizarAccion(string descripcion);

        public event dRealizarAccion OnRealizarAccion = delegate { };

        #endregion

        #region Funciones

        public virtual void SufrirDaño(int cantidad, ETipoDeDaño ETipo, ControladorPersonaje<ModeloPersonaje> instigador)
        {
            //OnSufrirDaño(ref cantidad, ETipo, this, instigador);
        }
        public virtual void AvanzarTurno()
        {
            //TODO: Aplicar los efectos
        }
        public virtual void EntrarEnCombate()
        {
        }
        public virtual void SalirDeCombate()
        {
        }
        public void ModificarStat(string stat, object valor)
        {
        }

        #endregion
    }
}
