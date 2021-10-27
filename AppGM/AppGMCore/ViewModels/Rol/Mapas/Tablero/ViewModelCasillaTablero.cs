using System.ComponentModel;


namespace AppGM.Core
{
    /// <summary>
    /// ViewModel que representa una casilla del tablero.
    /// </summary>
    public class ViewModelCasillaTablero : ViewModel
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// Indica si la casilla puede ser ocupada por una unidad o no.
        /// </summary>
        private bool puedeOcuparse = true;


        // Propiedades ---


        /// <summary>
        /// Color del borde de la casilla.
        /// </summary>
        public string ColorBordeCasilla { get; set; } = "000000";

        /// <summary>
        /// Color del fondo de la casilla.
        /// </summary>
        public string ColorFondoCasilla { get; set; } = "0000ffff";

        /// <summary>
        /// Indica si la casilla puede ser ocupada por una unidad dentro del mapa donde se encuentre.
        /// </summary>
        public bool PuedeOcuparse
        {
            get => puedeOcuparse;
            set
            {
                if (!value)
                {
                    puedeOcuparse = value;

                    ColorFondoCasilla = "7fe64e64";
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ColorFondoCasilla)));
                }
                else
                {
                    puedeOcuparse = value;

                    ColorFondoCasilla = "0000ffff";
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ColorFondoCasilla)));
                }
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewModelCasillaTablero()
        {

        }

        #endregion

        #region Funciones

        /// <summary>
        /// Dispara el evento property changed para las propiedades <see cref="ColorBordeIngresoPosicion" y <see cref="ColorFondoIngresoPosicion"/>/>
        /// </summary>
        public void DispararPropertyChangedColorBordeFondoCasilla()
        {
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ColorBordeCasilla)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ColorFondoCasilla)));
        }

        #endregion
    }
}
