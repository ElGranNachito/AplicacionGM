using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// View model que almacena los participantes de un <see cref="ViewModelCombate"/>
    /// </summary>
    public class ViewModelListaParticipantes : BaseViewModel
    {
        /// <summary>
        /// Participantes
        /// </summary>
        public List<ViewModelParticipante> Participantes { get; set; } = new List<ViewModelParticipante>();

        /// <summary>
        /// Combate al que pertenece
        /// </summary>
        public ViewModelCombate Combate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_participantes">Controladores de los participantes del combate</param>
        /// <param name="_combate">Combate al que pertenecen los participantes</param>
        public ViewModelListaParticipantes(List<ControladorParticipante> _participantes, ViewModelCombate _combate)
        {
            Combate = _combate;

            for (int i = 0; i < _participantes.Count; ++i)
                Participantes.Add(new ViewModelParticipante(_participantes[i], _combate));
        }
    }
}
