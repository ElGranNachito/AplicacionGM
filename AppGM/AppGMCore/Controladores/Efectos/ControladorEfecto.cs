using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorEfecto : Controlador<ModeloEfecto>
    {
        #region Propiedades & Campos

        private ControladorCondicion mCondicion;

        public List<IControladorModificadorDeStatBase> ControladoresModificaciones { get; set; }

        #endregion

        #region Constructor

        public ControladorEfecto(ModeloEfecto _modeloEfecto)
			:base(_modeloEfecto) {}

        #endregion

        #region Eventos

        /// <summary>
        /// Representa un metodo que lidia con eventos de aplicar efecto
        /// </summary>
        /// <param name="instigador">Quien quiere aplicar el efecto</param>
        /// <param name="objetivos">A quienes se le aplicara el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser aplicado</param>
        public delegate void dAplicarEfecto(ControladorPersonaje instigador, ControladorPersonaje[] objetivos,
            ControladorEfecto efectoAplicado);

        /// <summary>
        /// Representa un metodo que lidia con eventos de reducir efecto
        /// </summary>
        /// <param name="instigador">Quien quiere reducir el efecto</param>
        /// <param name="objetivos">A quienes se les reduce el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser reducido</param>
        public delegate void dReducirTurno(ControladorPersonaje instigador,
            ControladorPersonaje[] objetivos, ControladorEfecto efectoAplicado);

        /// <summary>
        /// Representa un metodo que lidia con eventos de quitar efecto
        /// </summary>
        /// <param name="instigador">Quien quiere quitar el efecto</param>
        /// <param name="objetivos">A quienes se les quita el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser quitado</param>
        public delegate void dQuitarEfecto(ControladorPersonaje instigador, ControladorPersonaje[] objetivos,
            ControladorEfecto efectoAplicado);

        /// <summary>
        /// Evento que se dispara cuando se aplica el efecto
        /// </summary>
        public event dAplicarEfecto OnAplicarEfecto  = delegate { };

        /// <summary>
        /// Evento que se dispara cuando se reduce el efecto
        /// </summary>
        public event dReducirTurno  OnReducirEfecto  = delegate { };

        /// <summary>
        /// Evento que se dispara cuando se quita el efecto
        /// </summary>
        public event dQuitarEfecto  OnQuitarEfecto   = delegate { };

        #endregion

        #region Funciones

        /// <summary>
        /// Chequea que el efecto pueda ser aplicado 
        /// </summary>
        /// <param name="personaje">Personaje quien se intenta aplicar el efecto</param>
        /// <returns></returns>
        public virtual bool PuedeAplicarEfecto(ControladorPersonaje personaje)
        {
            //TODO: Revisar que las condiciones para aplicar el efecto sean cumplidas

            return false;
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="personaje">Al que se aplicara el efecto</param>
        public virtual void AplicarEfecto(ControladorPersonaje personaje)
        {
            //TODO: Separamos el string con los valores correspondientes para cada stat y sumamos, disminuimos un turno si la duracion es superior a 0
        }

        #endregion
    }
}
