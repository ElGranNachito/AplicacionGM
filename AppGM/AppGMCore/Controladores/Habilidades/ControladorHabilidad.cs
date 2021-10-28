using System;
using System.Collections.Generic;
using CoolLogs;

namespace AppGM.Core
{
    public class ControladorHabilidad : Controlador<ModeloHabilidad>
    {
        #region Campos & Propiedades

        //-------------------------CAMPOS---------------------------

        /// <summary>
        /// Funcion que toma un control de personaje y devuelve un booleano indicando si el personaje puede utilizar la habilidad
        /// </summary>
        private ControladorFuncion_PredicadoHabilidad mPuedeSerUtilizada;
        
        /// <summary>
        /// Funcion que realiza la habilidad
        /// </summary>
        private ControladorFuncion_Habilidad mUtilizarHabilidad;

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
        /// Tirada de daño de la habilidad
        /// </summary>
        [AccesibleEnGuraScratch("TiradaDeDaño")]
        public ControladorTiradaDaño ControladorTiradaDeDaño { get; set; }

        /// <summary>
        /// Efectos sobre su usuario
        /// </summary>
        public List<ControladorEfecto> ControladorEfectosSobreUsuario { get; set; }

        /// <summary>
        /// Efectos sobre su objetivo
        /// </summary>
        public List<ControladorEfecto> ControladorEfectoSobreObjetivo { get; set; }

        /// <summary>
        /// Devuelve el controlador del personaja a quien pertenece esta habilidad
        /// </summary>
        public ControladorPersonaje DueñoHabilidad => SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.Dueño);

        public ETipoHabilidad TipoHabilidad
        {
	        get => modelo.TipoDeHabilidad;
	        set => modelo.TipoDeHabilidad = value;
        }

        public ERango Rango
        {
	        get => modelo.Rango;
	        set => modelo.Rango = value;
        }

        public int CostoMana
        {
	        get => modelo.CostoDeMana;
	        set => modelo.CostoDeMana = value;
        }

        public string Nombre
        {
	        get => modelo.Nombre;
	        set => modelo.Nombre = value;
        }

        #endregion

        #region Constructor

        public ControladorHabilidad(ModeloHabilidad _modeloHabilidad)
	        : base(_modeloHabilidad)
        {
            CargarVariablesYTiradas();
        }

        #endregion

        #region Eventos

        public delegate void dUtilizarHabilidad(ControladorHabilidad habilidad, ControladorPersonaje usuario, ControladorPersonaje[] objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        #endregion

        #region Funciones

        //TODO: Para todas estas funciones de utilizar, puede utilizar vamos a tener que implementar una manera de obtener los parametros extra que puedan requerir
        //TODO: asi el GM puede establecer sus valores antes de llamar Utilizar

        [AccesibleEnGuraScratch(nameof(Utilizar))]
        public void Utilizar(
	        ControladorPersonaje usuario, ControladorPersonaje[] objetivos,
	        object parametroExtra, object segundoParametroExtra)
        {
	        foreach (var objetivo in objetivos)
		        mUtilizarHabilidad.EjecutarFuncion(this, usuario, objetivo, new[] {parametroExtra, segundoParametroExtra});
        }

        public void Utilizar(
	        ControladorPersonaje usuario,
	        object parametroExtra, object segundoParametroExtra)
        {
	        mUtilizarHabilidad.EjecutarFuncion(this, usuario, null, new[]{parametroExtra, segundoParametroExtra});
        }

        public virtual bool PuedeUtilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos)
        {
	        foreach (var objetivo in objetivos)
	        {
		        if (!mPuedeSerUtilizada.EjecutarFuncion(this, usuario, objetivo).resultadoFuncion)
			        return false;
	        }

	        return true;
        }

        protected virtual void AlAvanzarTurno(ControladorPersonaje usuario)
        {
	        mAvanzarTurno(usuario);
        }
        protected virtual void AlCambiarDeDia(ControladorPersonaje usuario)
        {
	        mAvanzarDia(usuario);
        }

        public override ControladorVariableBase ObtenerControladorVariable(int idVariable)
        {
	        if (mVariablesPersistenes.ContainsKey(idVariable))
		        return mVariablesPersistenes[idVariable];

	        //Si la variable no se encuentra en esta habilidad entonces intentamos obtenerla a traves de su dueño
            if (DueñoHabilidad.ObtenerControladorVariable(idVariable) is { } controladorVariable)
		        return controladorVariable;

	        SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener una variable con id: {idVariable}, pero no se encuentra en {nameof(mVariablesPersistenes)}", ESeveridad.Error);

	        return null;
        }

        #endregion
    }

    public class ControladorMagia : ControladorHabilidad
    {
	    #region Campos & Propiedades

        

	    #endregion
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