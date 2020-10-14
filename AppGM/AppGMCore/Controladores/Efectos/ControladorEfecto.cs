﻿using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorEfecto<TipoEfecto> : ControladorBase<TipoEfecto>
        where TipoEfecto : ModeloEfecto, new()
    {
        #region Propiedades

        public bool EstaSiendoAplicado { get; set; }

        #endregion

        #region Controladores

        public List<IControladorModificadorDeStatBase> ControladoresModificaciones { get; set; }

        #endregion

        //public Func<ControladorPersonaje<ModeloPersonaje>, bool> PuedeSerAplicado

        #region Constructor

        public ControladorEfecto()
        {
        }

        public ControladorEfecto(ModeloEfecto _modeloEfecto)
        {
            modelo = (TipoEfecto) _modeloEfecto;
        }

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
        #region Constructor

        public ControladorEfectoTemporal(ModeloEfectoTemporal _modeloEfectoTemporal)
        {
            modelo = _modeloEfectoTemporal;
        }

        #endregion

        #region Propiedades

        public ushort TurnosRestantes { get; set; }

        #endregion

        #region Eventos

        public delegate void dQuitarEfecto(ControladorPersonaje<ModeloPersonaje> instigador, ControladorPersonaje<ModeloPersonaje>[] objetivos,
            ControladorEfecto<ModeloEfecto> efectoAplicado);

        public event dQuitarEfecto OnQuitarEfecto = delegate { };

        public delegate void dReducirTurno(ControladorPersonaje<ModeloPersonaje> instigador,
            ControladorPersonaje<ModeloPersonaje>[] objetivos, ControladorEfecto<ModeloEfecto> efectoAplicado);

        public event dReducirTurno OnReducirEfecto = delegate { };

        #endregion

        #region Funciones

        public override void AplicarEfecto(ControladorPersonaje<ModeloPersonaje> personaje)
        {
            base.AplicarEfecto(personaje);
        }

        public void AlPasarTurno()
        {
            //TODO: Disminuir turnos restantes
        }

        #endregion
    }
}
