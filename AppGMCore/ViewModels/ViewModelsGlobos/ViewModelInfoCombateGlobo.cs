using System.Text;

namespace AppGM.Core
{
    public class ViewModelInfoCombateGlobo : BaseViewModel
    {
        public ModeloAdministradorDeCombate Combate { get; set; }

        public string Participantes => ObtenerParticipantes();

        #region Funciones
        public string ObtenerParticipantes()
        {
            if (Combate == null)
                return string.Empty;

            StringBuilder listaPartcipantes = new StringBuilder();

            for (int i = 0; i < Combate.Participantes.Count; ++i)
            {
                ModeloPersonaje pj = Combate.Participantes[i].Participante.Personaje.Personaje;

                listaPartcipantes.Append(i == Combate.Participantes.Count - 1 ? $"{pj.Nombre}" : $"{pj.Nombre}, ");
            }

            return listaPartcipantes.ToString();
        }
        #endregion
    }
}
