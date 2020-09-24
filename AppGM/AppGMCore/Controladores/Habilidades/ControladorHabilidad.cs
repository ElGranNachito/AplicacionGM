using System;
using System.Collections.Generic;

namespace AppGMCore
{
    public class ControladorHabilidad<TipoHabilidad> : ControladorBase<ModeloHabilidad>
    {
        #region Miembros

        private ushort TurnosRestantes;
        private bool EstaActiva;

        private ControladorLimitador ControladorLimitador;
        private ControladorCargasHabilidad ControladorCargasHabilidad;
        private List<ControladorTiradaBase<ModeloTiradaBase>> ControladoresTiradas;


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

    class ControladorMagia : ControladorHabilidad<ModeloMagia>
    {
        #region Funciones

        public virtual void CancelarCasteo(ControladorPersonaje<ModeloPersonaje> usuario)
        {
            //TODO: Devolver el mana al usuario
        }

        #endregion
    }
}