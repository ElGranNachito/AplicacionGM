using System;

namespace AppGM.Core
{
    public class ControladorAmbiente : Controlador<ModeloAmbiente>
    {
        #region Propiedades

        /// <summary>
        /// Si el valor de Caracteristicas en el<see cref="ModeloAmbiente"/> es el default, devuelve el valor que tiene el ambiente global para Caracteristicas,
        /// de no ser default, devuelve el valor del ambiente local.
        /// </summary>
        public ECaracteristicasAmbiente Caracteristicas
        {
            get
            {
                if (modelo.CaracteristicasAmbiente == Constantes.CaracteristicasAmbiente)
                    return SistemaPrincipal.ModeloRolActual.AmbienteGlobal.Ambiente.CaracteristicasAmbiente;

                return modelo.CaracteristicasAmbiente;
            }
        }

        /// <summary>
        /// Si el valor de Casillas en el <see cref="ModeloAmbiente"/> es el default, devuelve el valor que tiene el ambiente global para Casillas,
        /// de no ser default, devuelve el valor del ambiente local.
        /// </summary>
        public int Casillas
        {
            get
            {
                if (modelo.CantidadCasillas == Constantes.CantidadCasillas)
                    return SistemaPrincipal.ModeloRolActual.AmbienteGlobal.Ambiente.CantidadCasillas;

                return modelo.CantidadCasillas;
            }
        }

        /// <summary>
        /// Si el valor de Temperatura en el <see cref="ModeloAmbiente"/> es el default, devuelve el valor que tiene el ambiente global para Temperatura,
        /// de no ser default, devuelve el valor del ambiente local.
        /// </summary>
        public int Temperatura
        {
            get
            {
                if (modelo.TemperaturaActual == Constantes.TemperaturaActual)
                    return SistemaPrincipal.ModeloRolActual.AmbienteGlobal.Ambiente.TemperaturaActual;

                return modelo.TemperaturaActual;
            }
        }

        /// <summary>
        /// Si el valor de Humedad en el <see cref="ModeloAmbiente"/> es el default, devuelve el valor que tiene el ambiente global para Humedad,
        /// de no ser default, devuelve el valor del ambiente local.
        /// </summary>
        public int Humedad
        {
            get
            {
                if (modelo.HumedadActual == Constantes.HumedadActual)
                    return SistemaPrincipal.ModeloRolActual.AmbienteGlobal.Ambiente.HumedadActual;

                return modelo.HumedadActual;
            }
        }

        /// <summary>
        /// Si el Mapa en el <see cref="ModeloAmbiente"/> es el default (null), devuelve el valor que tiene el ambiente global en Mapa,
        /// de no ser default, devuelve el valor del ambiente local.
        /// </summary>
        public TIMapaAmbiente Mapa
        {
            get
            {
                if (modelo.MapaDelAmbiente == Constantes.MapaDelAmbiente)
                    return SistemaPrincipal.ModeloRolActual.AmbienteGlobal.Ambiente.MapaDelAmbiente;

                return modelo.MapaDelAmbiente;
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloAmbiente">Modelo del ambiente que representa este controlador</param>
        public ControladorAmbiente(ModeloAmbiente _modeloAmbiente)
            : base(_modeloAmbiente) {}

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

        #endregion

        #region Funciones

        /// <summary>
        /// Modifica la temperatura del ambiente.
        /// </summary>
        /// <param name="_grados"></param>
        public void ModificarTemperatura(int _grados)
        {
            int temperatura;

            temperatura = modelo.TemperaturaActual + _grados;

            OnModificarTemperatura(ref temperatura);

            modelo.TemperaturaActual = temperatura;
        }

        /// <summary>
        /// Modifica la humedad del ambiente.
        /// </summary>
        /// <param name="_humedadRelativa"></param>
        public void ModificarHumedad(int _humedadRelativa)
        {
            int humedad;
            
            humedad = modelo.HumedadActual + _humedadRelativa;

            OnModificarHumedad(ref humedad);

            modelo.HumedadActual = humedad;
        }

        #endregion

    }
}
