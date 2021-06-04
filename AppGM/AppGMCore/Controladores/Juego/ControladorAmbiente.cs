using System;

namespace AppGM.Core
{
    public class ControladorAmbiente : Controlador<ModeloAmbiente>
    {
        #region Propiedades

        /// <summary>
        /// Rol en el que el ambiente se encuentra
        /// </summary>
        public ControladorRol Rol { get; set; }


        /// <summary>
        /// Devuelve verdadero si este ambiente toma sus valores del ambiente global del rol
        /// </summary>
        public bool EsInfluenciadoPorAmbienteGlobal => modelo.EsInfluidoPorAmbienteGlobal;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloAmbiente">Modelo del ambiente que representa este controlador</param>
        public ControladorAmbiente(ModeloAmbiente _modeloAmbiente)
            : base(_modeloAmbiente)
        {
            if (modelo.EsInfluidoPorAmbienteGlobal)
            {
                modelo.TemperaturaActual = Rol.modelo.AmbienteGlobal.Ambiente.TemperaturaActual;

                modelo.HumedadActual = Rol.modelo.AmbienteGlobal.Ambiente.HumedadActual;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Representa un metodo que lidie con eventos de modificacion de temperatura en el ambiente
        /// </summary>
        /// <param name="nuevoDia"></param>
        public delegate void dModificarTemperatura(ref int nuevaTemperatura);

        /// <summary>
        /// Evento que se dispara cuando la temperatura del ambiente cambia
        /// </summary>
        public event dModificarTemperatura OnModificarTemperatura = delegate { };

        /// <summary>
        /// Representa un metodo que lidie con eventos de modificacion de humedad en el ambiente
        /// </summary>
        /// <param name="nuevoDia"></param>
        public delegate void dModificarHumedad(ref int nuevaTemperatura);

        /// <summary>
        /// Evento que se dispara cuando la humedad relativa del ambiente cambia
        /// </summary>
        public event dModificarHumedad OnModificarHumedad = delegate { };

        /// <summary>
        /// Representa un metodo que lidie con eventos de modificacion de la influencia global en el ambiente
        /// </summary>
        /// <param name="nuevoDia"></param>
        public delegate void dModificarInfluenciaGloabl(ref bool nuevaInfluencia);

        /// <summary>
        /// Evento que se dispara cuando la influencia global sobre el ambiente cambia
        /// </summary>
        public event dModificarInfluenciaGloabl OnModificarInfluenciaGloabl = delegate { };

        #endregion

        #region Funciones

        public void ModificarTemperatura(int grados)
        {
            int temperatura;

            if (!EsInfluenciadoPorAmbienteGlobal)
                temperatura = modelo.TemperaturaActual + grados;
            else
                temperatura = Rol.modelo.AmbienteGlobal.Ambiente.TemperaturaActual;
            
            OnModificarTemperatura(ref temperatura);

            modelo.TemperaturaActual = temperatura;
        }

        public void ModificarHumedad(int humedadRelativa)
        {
            int humedad;

            if (!EsInfluenciadoPorAmbienteGlobal)
                humedad = modelo.HumedadActual + humedadRelativa;
            else
                humedad = Rol.modelo.AmbienteGlobal.Ambiente.HumedadActual;

            OnModificarHumedad(ref humedad);

            modelo.HumedadActual = humedad;
        }

        public void ModificarInfluenciaSobreAmbiente(bool influencia)
        {
            OnModificarInfluenciaGloabl(ref influencia);

            modelo.EsInfluidoPorAmbienteGlobal = influencia;
        }

        #endregion

    }
}
