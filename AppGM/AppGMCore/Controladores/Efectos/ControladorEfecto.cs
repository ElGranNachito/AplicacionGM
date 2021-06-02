using System;
using System.Collections.Generic;
using AppGM.Core.Controladores.Efectos;

namespace AppGM.Core
{
    /// <summary>
    /// Constrolador de un efecto
    /// </summary>
    //TODO: Añadir una funcion para sacar este efecto a todos sus afectados
    public class ControladorEfecto : Controlador<ModeloEfecto>
    {
        #region Propiedades & Campos

        /// <summary>
        /// Funcion que nos permite saber si el <see cref="ModeloEfecto"/> puede aplicarse desde determinado <see cref="ControladorPersonaje"/> a determinados <see cref="ControladorPersonaje"/>
        /// </summary>
        private Func<ControladorEfecto, ControladorPersonaje, ControladorPersonaje, bool> mPuedeAplicarse;

        /// <summary>
        /// Funcion que aplica el <see cref="ModeloEfecto"/> desde cierto <see cref="ControladorPersonaje"/> a otros <see cref="ControladorPersonaje"/>
        /// </summary>
        private Action<ControladorEfecto, ControladorPersonaje, ControladorPersonaje> mAplicarEfecto;

        /// <summary>
        /// Funcion que quita el <see cref="ModeloEfecto"/> desde cierto <see cref="ControladorPersonaje"/> a otros <see cref="ControladorPersonaje"/>
        /// </summary>
        private Action<ControladorEfecto, ControladorPersonaje, ControladorPersonaje> mQuitarEfecto;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloEfecto"><see cref="ModeloEfecto"/> que representa</param>
        public ControladorEfecto(ModeloEfecto _modeloEfecto)
	        : base(_modeloEfecto)
        {
            //TODO: Pasar el ModeloEfecto al compilador y obtener las funciones.
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Representa un metodo que lidia con eventos de aplicar efecto
        /// </summary>
        /// <param name="instigador">Quien quiere aplicar el efecto</param>
        /// <param name="objetivos">A quienes se le aplicara el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser aplicado</param>
        public delegate void dAplicarEfecto(ControladorPersonaje instigador, ControladorPersonaje objetivos,
            ControladorEfecto efectoAplicado);

        /// <summary>
        /// Representa un metodo que lidia con eventos de reducir efecto
        /// </summary>
        /// <param name="instigador">Quien quiere reducir el efecto</param>
        /// <param name="objetivos">A quienes se les reduce el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser reducido</param>
        public delegate void dReducirTurno(ControladorPersonaje instigador,
            ControladorPersonaje objetivos, ControladorEfecto efectoAplicado);

        /// <summary>
        /// Representa un metodo que lidia con eventos de quitar efecto
        /// </summary>
        /// <param name="instigador">Quien quiere quitar el efecto</param>
        /// <param name="objetivos">A quienes se les quita el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser quitado</param>
        public delegate void dQuitarEfecto(ControladorPersonaje instigador, ControladorPersonaje objetivos,
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
        /// Crea una instancia de un <see cref="ControladorEfectoSiendoAplicado"/> para aplicar este efecto a ciertos <paramref name="objetivos"/>
        /// </summary>
        /// <param name="instigador"><see cref="ControladorPersonaje"/> que aplica este efecto</param>
        /// <param name="objetivos"><see cref="ControladorPersonaje"/> que recibiran el efecto</param>
        /// <param name="añadirEfectoAObjetivos">Indica si añadir el <see cref="ControladorEfectoSiendoAplicado"/> creado a los <paramref name="objetivos"/></param>
        /// <returns></returns>
        public ControladorEfectoSiendoAplicado CrearInstanciaEfecto(ControladorPersonaje instigador, List<ControladorPersonaje> objetivos, bool añadirEfectoAObjetivos = true)
        {
	        ControladorEfectoSiendoAplicado controladorCreado = new ControladorEfectoSiendoAplicado(this, instigador, objetivos);

            modelo.Aplicaciones.Add(controladorCreado.modelo.Efecto);

	        //Si debemos añadir los efectos a los objetivos...
	        if (añadirEfectoAObjetivos)
	        {
                //Añadimos el efecto a cada uno
		        foreach (var obj in objetivos)
                    obj.AñadirEfecto(controladorCreado);
	        }

	        return controladorCreado;
        }

        /// <summary>
        /// Chequea que el efecto pueda ser aplicado 
        /// </summary>
        /// <param name="usuario">Personaje que intenta aplicar el efecto</param>
        /// <param name="objetivos">Personaje/s a quienes se intenta aplicar el efecto</param>
        /// <returns></returns>
        public virtual bool PuedeAplicarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivos)
        {
	        return mPuedeAplicarse(this, usuario, objetivos);
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="usuario">Personaje que aplicara el efecto</param>
        /// <param name="objetivos">Personaje/s a quienes se les aplicara el efecto</param>
        public virtual void AplicarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivos)
        {
            mAplicarEfecto(this, usuario, objetivos);

            OnAplicarEfecto(usuario, objetivos, this);
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="usuario">Personaje que origino el efecto</param>
        /// <param name="objetivos">Personaje/s a quienes se les quitara el efecto</param>
        public virtual void QuitarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivos)
        {
	        mQuitarEfecto(this, usuario, objetivos);

	        OnQuitarEfecto(usuario, objetivos, this);
        }
        #endregion
    }
}