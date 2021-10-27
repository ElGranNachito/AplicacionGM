using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
    /// <summary>
    /// ViewModel que representa una fila de casillas del tablero.
    /// </summary>
    public class ViewModelFilaCasillas : ViewModel
    {
        #region Miembros

        // Campos ---


        // Propiedades ---


        /// <summary>
        /// Fila de casillas de tableros.
        /// </summary>
        public ObservableCollection<ViewModelCasillaTablero> CasillasTablero { get; set; } = new ObservableCollection<ViewModelCasillaTablero>();

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewModelFilaCasillas()
        {
            for (int i = 0; i < 50; ++i)
                CasillasTablero.Add(new ViewModelCasillaTablero());
        }

        #endregion

        #region Funciones



        #endregion
    }
}
