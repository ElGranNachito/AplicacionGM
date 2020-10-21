using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorHabilidad : Controlador<ModeloHabilidad>
    {
        #region Controladores

        public ControladorLimitador ControladorLimiteDeUsos { get; set; }
        public ControladorCargasHabilidad ControladorCargasHabilidad { get; set; }

        public List<IControladorTiradaBase> ControladorTiradasDeUso { get; set; }
        public ControladorTiradaVariable ControladorTiradaDeDaño { get; set; }

        public List<ControladorUtilizable> ControladorItemInvocacion { get; set; }
        public List<ControladorUtilizable> ControladorItemsQueCuesta { get; set; }

        public List<ControladorInvocacion> ControladorInvocacion { get; set; }

        public List<ControladorEfecto> ControladorEfectosSobreUsuario { get; set; }
        public List<ControladorEfecto> ControladorEfectoSobreObjetivo { get; set; }

        #endregion

        #region Miembros

        private ushort TurnosRestantes;
        private bool EstaActiva;

        private Func<ControladorPersonaje, bool> mPuedeSerUtilizada;
        private Func<ControladorPersonaje, ControladorPersonaje[], bool> mPuedeSerUtilizadaConObjetivos;

        #endregion

        #region Constructor

        public ControladorHabilidad()
        {
        }

        public ControladorHabilidad(ModeloHabilidad _modeloHabilidad)
        {
            modelo = _modeloHabilidad;
        }

        #endregion

        #region Eventos

        public delegate void dUtilizarHabilidad(ControladorHabilidad habilidad, ControladorPersonaje usuario, ControladorPersonaje[] objetivos);

        public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

        #endregion

        #region Funciones

        public virtual void IntentarUtilizar(ControladorPersonaje usuario)
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

    public class ControladorMagia : ControladorHabilidad
    {
        #region Constructor

        public ControladorMagia(ModeloMagia _modeloMagia)
        {
            modelo = _modeloMagia;
        }

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