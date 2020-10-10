using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorHabilidad<TipoHabilidad> : ControladorBase<ModeloHabilidad>
    {
        #region Controladores

        public ControladorLimitador ControladorLimiteDeUsos { get; set; }
        public ControladorCargasHabilidad ControladorCargasHabilidad { get; set; }

        public List<IControladorTiradaBase> ControladorTiradasDeUso { get; set; }
        public ControladorTiradaVariable ControladorTiradaDeDaño { get; set; }

        public List<ControladorUtilizable<ModeloItem>> ControladorItemInvocacion { get; set; }
        public List<ControladorUtilizable<ModeloItem>> ControladorItemsQueCuesta { get; set; }

        public List<ControladorInvocacion<ModeloInvocacion>> ControladorInvocacion { get; set; }

        public List<ControladorEfecto<ModeloEfecto>> ControladorEfectosSobreUsuario { get; set; }
        public List<ControladorEfecto<ModeloEfecto>> ControladorEfectoSobreObjetivo { get; set; }

        #endregion

        #region Miembros

        private ushort TurnosRestantes;
        private bool EstaActiva;

        private Func<ControladorPersonaje<ModeloPersonaje>, bool> mPuedeSerUtilizada;
        private Func<ControladorPersonaje<ModeloPersonaje>, ControladorPersonaje<ModeloPersonaje>[], bool> mPuedeSerUtilizadaConObjetivos;
        
        #endregion

        #region Eventos

        public delegate void dUtilizarHabilidad(ControladorHabilidad<ModeloHabilidad> habilidad, ControladorPersonaje<ModeloPersonaje> usuario, ControladorPersonaje<ModeloPersonaje>[] objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        #endregion

        #region Funciones

        public virtual void IntentarUtilizar(ControladorPersonaje<ModeloPersonaje> usuario)
        {
            //TODO: Realizar una tirada de casteo sin activar la habilidad y devolver el resultado.
        }
        private void AlAvanzarTurno()
        {
        }
        private void AlCambiarDeDia(int dia)
        {
        }

        #endregion
    }

    public class ControladorMagia : ControladorHabilidad<ModeloMagia>
    {
        #region Funciones

        public virtual void CancelarCasteo(ControladorPersonaje<ModeloPersonaje> usuario)
        {
            //TODO: Devolver el mana al usuario
        }

        #endregion
    }
}