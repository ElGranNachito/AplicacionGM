using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorHabilidad : Controlador<ModeloHabilidad>, IUtilizable
    {
        #region Campos & Propiedades

        //-------------------------CAMPOS---------------------------

        /// <summary>
        /// Funcion que toma un control de personaje y devuelve un booleano indicando si el personaje puede utilizar la habilidad
        /// </summary>
        private Func<ControladorPersonaje, ControladorPersonaje[], bool> mPuedeSerUtilizada;

        /// <summary>
        /// Funcion que realiza la habilidad
        /// </summary>
        private Action<ControladorPersonaje, ControladorPersonaje[], object, object> mUtilizarHabilidad;

        /// <summary>
        /// Funcion que se encarga de realizar acciones necesaria por cada paso de turno
        /// </summary>
        private Action<ControladorPersonaje> mAvanzarTurno;

        /// <summary>
        /// Funcion que se encarga de realizar las acciones necesarias cuando pasa un dia
        /// </summary>
        private Action<ControladorPersonaje> mAvanzarDia;

        //-----------------------PROPIEDADES-----------------------

        /// <summary>
        /// Controlador del limitador de usos de la habilidad.
        /// Null si la habilidad no tiene un limite de usos
        /// </summary>
        public ControladorLimitador ControladorLimiteDeUsos { get; set; }

        /// <summary>
        /// Controlador de cargas de la habilidad.
        /// Null si la habilidad no tiene cargas
        /// </summary>
        public ControladorCargasHabilidad ControladorCargasHabilidad { get; set; }

        /// <summary>
        /// Lista de las tiradas que tienen que realizarse al utilizar la habilidad
        /// </summary>
        public List<IControladorTiradaBase> ControladorTiradasDeUso { get; set; }

        /// <summary>
        /// Tirada de daño de la habilidad
        /// </summary>
        [AccesibleEnGuraScratch("TiradaDeDaño")]
        public ControladorTiradaDaño ControladorTiradaDeDaño { get; set; }

        /// <summary>
        /// Items que invoca
        /// </summary>
        public List<ControladorUtilizable> ControladorItemInvocacion { get; set; }

        /// <summary>
        /// Items que cuesta
        /// </summary>
        public List<ControladorUtilizable> ControladorItemsQueCuesta { get; set; }

        /// <summary>
        /// Invocacion que crea
        /// </summary>
        public List<ControladorInvocacion> ControladorInvocacion { get; set; }

        /// <summary>
        /// Efectos sobre su usuario
        /// </summary>
        public List<ControladorEfecto> ControladorEfectosSobreUsuario { get; set; }

        /// <summary>
        /// Efectos sobre su objetivo
        /// </summary>
        public List<ControladorEfecto> ControladorEfectoSobreObjetivo { get; set; }

        #endregion

        #region Constructor

        public ControladorHabilidad(ModeloHabilidad _modeloHabilidad)
            :base(_modeloHabilidad){}

        #endregion

        #region Eventos

        public delegate void dUtilizarHabilidad(ControladorHabilidad habilidad, ControladorPersonaje usuario, ControladorPersonaje[] objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        #endregion

        #region Funciones

        [AccesibleEnGuraScratch(nombreQueMostrar = "Utilizar")]
        public void Utilizar(
	        ControladorPersonaje usuario, ControladorPersonaje[] objetivos,
	        object parametroExtra, object segundoParametroExtra)
        {
	        mUtilizarHabilidad(usuario, objetivos, parametroExtra, segundoParametroExtra);
        }

        public void Utilizar(
	        ControladorPersonaje usuario,
	        object parametroExtra, object segundoParametroExtra)
        {
	        mUtilizarHabilidad(usuario, null, parametroExtra, segundoParametroExtra);
        }

        public virtual bool PuedeUtilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos)
        {
	        return mPuedeSerUtilizada(usuario, objetivos);
        }
        protected virtual void AlAvanzarTurno(ControladorPersonaje usuario)
        {
	        mAvanzarTurno(usuario);
        }
        protected virtual void AlCambiarDeDia(ControladorPersonaje usuario)
        {
	        mAvanzarDia(usuario);
        }

        #endregion
    }

    public class ControladorMagia : ControladorHabilidad
    {
        #region Constructor

        public ControladorMagia(ModeloMagia _modeloMagia)
            :base(_modeloMagia){}

        #endregion

        #region Funciones

        public virtual void CancelarCasteo(ControladorPersonaje usuario)
        {
            //TODO: Devolver el mana al usuario
        }

        #endregion
    }

    /// <summary>
    /// Una clase para asegurarnos de que los <see cref="ModeloHabilidad"/> sean de cierto tipo, asi podemos evitar errores en los casteos
    /// </summary>
    /// <typeparam name="TipoHabilidad">Tipo del modelo</typeparam>
    public class ControladorHabilidadG<TipoHabilidad> : ControladorHabilidad
        where TipoHabilidad: ModeloHabilidad, new()
    {
        public ControladorHabilidadG(TipoHabilidad _habilidad) : base(_habilidad){}

        public static Type ObtenerTipo() => typeof(TipoHabilidad);
    }
}