using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelCombateItem : BaseViewModel
    {
        #region Propiedades
        public ModeloAdministradorDeCombate ModeloAdministradorDeCombate { get; set; }
        public ICommand ComandoClickeado { get; set; }
        public ICommand ComandoMouseEnter { get; set; }
        public ICommand ComandoMouseLeave { get; set; }

        #endregion

        #region Constructores
        public ViewModelCombateItem()
        {
            ComandoMouseEnter = new Comando(
                () =>
                {

                });
            ComandoMouseLeave = new Comando(
                () =>
                {

                }
            );

            ComandoClickeado = new Comando(
                () =>
                {

                });
        } 
        #endregion
    }

}
