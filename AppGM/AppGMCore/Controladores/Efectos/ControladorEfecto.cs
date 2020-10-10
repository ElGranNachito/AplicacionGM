using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorEfecto<TipoEfecto> : ControladorBase<ModeloEfecto>
    {
        //TODO: Organizar

        public bool EstaSiendoAplicado { get; set; }

        //Func<ControladorPersonaje, bool> PuedeSerAplicado

        #region Controladores

        public List<ControladorModificadorDeStatBase<ModeloModificadorDeStatBase>> ControladoresModificaciones;

        #endregion

        #region Eventos

        public delegate void dAplicarEfecto(ControladorPersonaje<ModeloPersonaje> instigador, ControladorPersonaje<ModeloPersonaje>[] objetivos,
            ControladorEfecto<ModeloEfecto> efectoAplicado);

        public event dAplicarEfecto OnAplicarEfecto = delegate { };

        #endregion

        #region Funciones

        public virtual bool PuedeAplicarEfecto(ControladorPersonaje<ModeloPersonaje> personaje)
        {
            //TODO: Revisar que las condiciones para aplicar el efecto sean cumplidas

            return false;
        }

        public virtual void AplicarEfecto(ControladorPersonaje<ModeloPersonaje> personaje)
        {
            //TODO: Separamos el string con los valores correspondientes para cada stat y sumamos
        }

        #endregion
    }

    public class ControladorEfectoTemporal : ControladorEfecto<ModeloEfectoTemporal>
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
