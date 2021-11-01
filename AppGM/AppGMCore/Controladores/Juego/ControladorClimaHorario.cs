using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloClimaHorario"/>
    /// </summary>
    public class ControladorClimaHorario : Controlador<ModeloClimaHorario>
    {
        #region Propiedades

        

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloClimaHorario">Modelo del clima que representa este controlador</param>
        public ControladorClimaHorario(ModeloClimaHorario _modeloClimaHorario)
            : base(_modeloClimaHorario) {}

        #endregion

        #region Funciones

        /// <summary>
        /// Actualiza las condiciones climaticas de forma aleatoria.
        /// </summary>
        public void ActualizarClima()
        {
            modelo.Clima       = ((EClima)(new Random().Next(1, Enum.GetValues(typeof(EClima)).Length - 1)));
            modelo.Viento      = ((EViento)(new Random().Next(1, Enum.GetValues(typeof(EViento)).Length - 1)));
            modelo.Humedad     = ((EHumedad)(new Random().Next(1, Enum.GetValues(typeof(EHumedad)).Length - 1)));
            modelo.Temperatura = ((ETemperatura)(new Random().Next(1, Enum.GetValues(typeof(ETemperatura)).Length - 1)));
        }

        /// <summary>
        /// Avanza un dia de la semana.
        /// </summary>
        public void AvanzarDiaSemana()
        {
            modelo.DiaSemana.Siguiente();
        }

        /// <summary>
        /// Retrocede un dia de la semana.
        /// </summary>
        public void RetrocederDiaSemana()
        {
            modelo.DiaSemana.Anterior();
        }

        #endregion
    }
}
