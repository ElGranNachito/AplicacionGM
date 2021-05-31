using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// VM delmenu de seleccion de combates
    /// </summary>
    public class ViewModelMenuSeleccionCombate : BaseViewModel
    {
        #region Propiedades

        /// <summary>
        /// Lista de combates existentes
        /// </summary>
        public ViewModelListaCombates Combates { get; set; }

        /// <summary>
        /// Globo que muestra la informacion del combate que el usuario tiene actualmente seleccionado
        /// </summary>
        public ViewModelGlobo<ViewModelInfoCombateGlobo> GloboInfoCombate { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_combates">Lista que contiene controladores de los administradores de combate existentes</param>
        public ViewModelMenuSeleccionCombate(List<ControladorAdministradorDeCombate> _combates)
        {
            Combates = new ViewModelListaCombates(_combates);

            GloboInfoCombate = new ViewModelGlobo<ViewModelInfoCombateGlobo>
            {
                ViewModelContenido = new ViewModelInfoCombateGlobo(),
                GloboVisible = false
            };
        }
        #endregion
    }
}