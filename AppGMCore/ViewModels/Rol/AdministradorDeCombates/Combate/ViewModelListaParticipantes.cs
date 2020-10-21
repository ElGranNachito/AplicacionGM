using System.Collections.Generic;

namespace AppGM.Core
{
    public class ViewModelListaParticipantes : BaseViewModel
    {
        public List<ViewModelParticipante> Participantes { get; set; }

        public ViewModelListaParticipantes(List<ControladorParticipante> _participantes)
        {
            for (int i = 0; i < _participantes.Count; ++i)
                Participantes.Add(new ViewModelParticipante(_participantes[i]));
        }
    }
}
