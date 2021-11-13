using System.Collections.Generic;
using CoolLogs;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloParticipante"/>
    /// </summary>
    public class ControladorParticipante : Controlador<ModeloParticipante>
    {
        #region Controladores

        /// <summary>
        /// Controlador del personaje que representa este <see cref="ModeloParticipante"/>
        /// </summary>
        public ControladorPersonaje ControladorPersonaje { get; private set; }

        public List<ControladorAccion> ControladoresAcciones { get; set; } = new List<ControladorAccion>();

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloParticipante">Modelo del participante</param>
        public ControladorParticipante(ModeloParticipante _modeloParticipante)
			:base(_modeloParticipante)
        {
	        ControladorPersonaje = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.Personaje, true);

            for (int i = 0; i < modelo.AccionesRealizadas.Count; ++i)
                ControladoresAcciones.Add(SistemaPrincipal.ObtenerControlador<ControladorAccion, ModeloAccion>(modelo.AccionesRealizadas[i]));
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Añade una accion al combate
        /// </summary>
        /// <param name="participante">Accion para añadir</param>
        public void AñadirAccion(ModeloAccion modeloAccion)
        {
            modelo.AccionesRealizadas.Add(modeloAccion);
            
            ++modelo.AccionesRealizadasEnTurno;
            ++modelo.AccionesRestantes;

            SistemaPrincipal.GuardarModelo(modeloAccion);
        }

        #endregion
    }
}
