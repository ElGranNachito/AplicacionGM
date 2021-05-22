using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorEfecto : Controlador<ModeloEfecto>
    {
        #region Propiedades

        public ushort TurnosRestantes { get; set; }

        public bool EstaSiendoAplicado { get; set; }

        #endregion

        #region Controladores

        public List<IControladorModificadorDeStatBase> ControladoresModificaciones { get; set; }

        #endregion

        public Func<ControladorPersonaje, bool> PuedeSerAplicado;

        #region Constructor

        public ControladorEfecto()
        {
        }

        public ControladorEfecto(ModeloEfecto _modeloEfecto)
        {
            modelo = _modeloEfecto;
        }

        #endregion

        #region Eventos

        public delegate void dAplicarEfecto(ControladorPersonaje instigador, ControladorPersonaje[] objetivos,
            ControladorEfecto efectoAplicado);

        public delegate void dReducirTurno(ControladorPersonaje instigador,
            ControladorPersonaje[] objetivos, ControladorEfecto efectoAplicado);

        public delegate void dQuitarEfecto(ControladorPersonaje instigador, ControladorPersonaje[] objetivos,
            ControladorEfecto efectoAplicado);

        public event dAplicarEfecto OnAplicarEfecto  = delegate { };
        public event dReducirTurno  OnReducirEfecto  = delegate { };
        public event dQuitarEfecto  OnQuitarEfecto   = delegate { };

        #endregion

        #region Funciones

        public virtual bool PuedeAplicarEfecto(ControladorPersonaje personaje)
        {
            //TODO: Revisar que las condiciones para aplicar el efecto sean cumplidas

            return false;
        }

        public virtual void AplicarEfecto(ControladorPersonaje personaje)
        {
            //TODO: Separamos el string con los valores correspondientes para cada stat y sumamos, disminuimos un turno si la duracion es superior a 0
        }

        #endregion
    }
}
