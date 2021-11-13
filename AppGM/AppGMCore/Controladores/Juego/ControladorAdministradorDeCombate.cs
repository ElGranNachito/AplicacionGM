using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloAdministradorDeCombate"/>
    /// </summary>
    public class ControladorAdministradorDeCombate : Controlador<ModeloAdministradorDeCombate>
    {
        #region Propiedades

        /// <summary>
        /// Controlador del ambiente en el que se encuentra el combate
        /// </summary>
        public ControladorAmbiente ControladorAmbiente { get; set; }

        /// <summary>
        /// Controladores de todos los participantes del combate
        /// </summary>
        public List<ControladorParticipante> ControladoresParticipantes { get; set; } = new List<ControladorParticipante>();

        /// <summary>
        /// Controladores de todos los mapas
        /// </summary>
        public List<ControladorMapa> ControladoresMapas { get; set; } = new List<ControladorMapa>();

        #endregion

        #region Eventos

        /// <summary>
        /// Representa un metodo que lidia con el cambio de turnos durante el combate
        /// </summary>
        /// <param name="TurnoActual"></param>
        public delegate void dTurnoCambio(ref int turnoAnterior, ref int turnoActual);

        /// <summary>
        /// Evento que se dispara cuando se cambia de turno
        /// </summary>
        public event dTurnoCambio OnTurnoCambio = delegate { };

        /// <summary>
        /// Representa un metodo que lidia con el avance de turno del personaje
        /// </summary>
        /// <param name="modeloAdministradorDeCombate"></param>
        public delegate void dAvanzarTurnoPersonaje(ModeloAdministradorDeCombate modeloAdministradorDeCombate);

        /// <summary>
        /// Evento que se dispara cuando se avanza con el turno de un personaje
        /// </summary>
        public event dAvanzarTurnoPersonaje OnAvanzarTurnoPersonaje = delegate { };
        
        /// <summary>
        /// Representa un metodo que lidia con la activacion y desactivacion del combate
        /// </summary>
        /// <param name="actividadCombate">Verdadero si el combate esta activo. Falso si se desactiva</param>
        public delegate void dActividadModificada(ref bool actividadCombate);

        /// <summary>
        /// Evento que se dispara cuando se modifica la actividad del combate
        /// </summary>
        public event dActividadModificada OnActividadModificada = delegate { };

        #endregion

        #region Constructores
        public ControladorAdministradorDeCombate(ModeloAdministradorDeCombate _modeloAdministradorCombate)
			:base(_modeloAdministradorCombate)
        {
            for (int i = 0; i < modelo.Participantes.Count; ++i) 
                ControladoresParticipantes.Add(SistemaPrincipal.ObtenerControlador<ControladorParticipante, ModeloParticipante>(modelo.Participantes[i]));

            for (int i = 0; i < modelo.Mapas.Count; ++i)
                ControladoresMapas.Add(SistemaPrincipal.ObtenerControlador<ControladorMapa, ModeloMapa>(modelo.Mapas[i]));

            ControladoresParticipantes[0].modelo.EsSuTurno = true;
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Agrega un participante al combate
        /// </summary>
        /// <param name="participante">Participante que agregar</param>
        public void AgregarParticipante(ModeloParticipante participante)
        {
            modelo.Participantes.Add(participante);

            ControladoresParticipantes.Add(SistemaPrincipal.ObtenerControlador<ControladorParticipante, ModeloParticipante>(participante, true));

            SistemaPrincipal.GuardarModelo(participante);
        }

        /// <summary>
        /// Avanza el turno actual del combate si todos los personajes ya tuvieron su turnos,
        /// si no es asi entonces avanza al turno del proximo personaje
        /// </summary>
        public void AvanzarTurno()
        {
            int turnoAnteriorTmp = modelo.IndicePersonajeTurnoActual;
            
            ControladoresParticipantes[modelo.IndicePersonajeTurnoActual].modelo.EsSuTurno = false;

            ++modelo.IndicePersonajeTurnoActual;
            ++modelo.TurnoActual;

            if (modelo.IndicePersonajeTurnoActual >= modelo.Participantes.Count)
                modelo.IndicePersonajeTurnoActual = 0;

            int turnoActualTmp = modelo.IndicePersonajeTurnoActual;

            ControladoresParticipantes[modelo.IndicePersonajeTurnoActual].modelo.EsSuTurno = true;

            OnTurnoCambio(ref turnoAnteriorTmp, ref turnoActualTmp);
            
            modelo.IndicePersonajeTurnoActual = turnoActualTmp;
        }

        /// <summary>
        /// Retrocede el turno actual si este es el primer personaje en la lista,
        /// si no es asi entonces retrocedemos al personaje anterior
        /// </summary>
        public void RetrocederTurno()
        {
            int turnoAnteriorTmp = modelo.IndicePersonajeTurnoActual;

            ControladoresParticipantes[modelo.IndicePersonajeTurnoActual].modelo.EsSuTurno = false;

            --modelo.IndicePersonajeTurnoActual;

            if (modelo.IndicePersonajeTurnoActual < 0)
                modelo.IndicePersonajeTurnoActual = modelo.Participantes.Count - 1;

            int turnoActualTmp = modelo.IndicePersonajeTurnoActual;

            ControladoresParticipantes[modelo.IndicePersonajeTurnoActual].modelo.EsSuTurno = true;

            OnTurnoCambio(ref turnoAnteriorTmp, ref turnoActualTmp);

            modelo.IndicePersonajeTurnoActual = turnoActualTmp;
        }

        public void ModificarActividadCombate(bool _actividadCombate)
        {
            OnActividadModificada(ref _actividadCombate);

            modelo.EstaActivo = _actividadCombate;
        }

        #endregion
    }
}