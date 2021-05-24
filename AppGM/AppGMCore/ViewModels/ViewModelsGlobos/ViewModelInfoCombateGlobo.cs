using System.Text;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel para el contenido de un globo que muestra la informacion de un combate
    /// </summary>
    public class ViewModelInfoCombateGlobo : BaseViewModel
    {
		#region Propiedades

		/// <summary>
		/// Modelo del combate
		/// </summary>
		public ModeloAdministradorDeCombate Combate { get; set; }

		/// <summary>
		/// Obtiene una lista de los participantes
		/// </summary>
		public string Participantes => ObtenerParticipantes(); 

		#endregion

		#region Funciones

		public string ObtenerParticipantes()
        {
            if (Combate == null)
                return string.Empty;

            StringBuilder listaPartcipantes = new StringBuilder();

            for (int i = 0; i < Combate.Participantes.Count; ++i)
            {
                ModeloPersonaje pj = Combate.Participantes[i].Participante.Personaje.Personaje;

                listaPartcipantes.Append(i == Combate.Participantes.Count - 1 
					//Si este es el ultimo personaje de la lista...
	                ? $"{pj.Nombre}" 
                    //Sino...
	                : $"{pj.Nombre}, ");
            }

            return listaPartcipantes.ToString();
        }

        #endregion
    }
}
