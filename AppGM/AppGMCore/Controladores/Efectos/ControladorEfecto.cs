using System;

namespace AppGM.Core
{
    public class ControladorEfecto<TipoEfecto> : ControladorBase<ModeloEfecto>
    {
        public bool EstaSiendoAplicado { get; set; }

        //Func<ControladorPersonaje, bool> PuedeSerAplicado

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
}
