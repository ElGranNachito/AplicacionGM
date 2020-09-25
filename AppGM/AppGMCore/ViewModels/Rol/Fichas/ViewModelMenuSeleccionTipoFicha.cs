using System.Collections.Generic;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelMenuSeleccionTipoFicha : BaseViewModel
    {
        #region Propiedades
        public ICommand ComandoBotonFichasServants { get; set; }
        public ICommand ComandoBotonFichasMasters { get; set; }
        public ICommand ComandoBotonFichasInvocaciones { get; set; }
        public ICommand ComandoBotonFichasNPCs { get; set; }

        public ETipoPersonaje ETipoPersonajeSeleccionado { get; set; }

        public List<ModeloServant> Servants { get; set; }
        public List<ModeloMaster> Masters { get; set; }
        public List<ModeloInvocacion> Invocaciones { get; set; }
        public List<ModeloPersonaje> NPCs { get; set; }

        #endregion

        public ViewModelMenuSeleccionTipoFicha()
        {
            Servants = new List<ModeloServant>
            {
                new ModeloServant
                {
                    Nombre = "Stephen Hawking",
                    mEClaseDeServant = EClaseServant.Caster
                },

                new ModeloServant
                {
                    Nombre = "Krampus",
                    mEClaseDeServant = EClaseServant.Rider
                },

                new ModeloServant
                {
                    Nombre = "Pelinore",
                    mEClaseDeServant = EClaseServant.Saber
                }
            };

            ComandoBotonFichasServants = new Comando(() =>
            {
                //Creamos una variable tempoal para almacenar los view models para cada item
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                //Creamos los view models
                for (int i = 0; i < Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Servants[i]));

                //Cambiamos las fichas actuales por la variable temporal que creamos
                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Establecemos que el tipo de personaje seleccionado fue Servant
                ETipoPersonajeSeleccionado = ETipoPersonaje.Servant;

                //Cambiamos la pagina actual
                SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipalRol>().EMenuActual =
                    EMenuActualRol.VistaFichas;
            });
        }
    }
}
