using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelMenuSeleccionTipoFicha : BaseViewModel
    {
        #region Propiedades

        public Grosor AnchoMargenCartas => SistemaPrincipal.ObtenerInstancia<ViewModelAplicacion>().VentanaMaximizada ? new Grosor(20) : new Grosor(5, 20);
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
            SistemaPrincipal.ObtenerInstancia<ViewModelAplicacion>().PropertyChanged += (o, a) =>
            {
                if (a.PropertyName == nameof(ViewModelAplicacion.VentanaMaximizada))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(AnchoMargenCartas)));
            };

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
                    mEClaseDeServant = EClaseServant.Saber,

                    Caracteristicas = new TIPersonajeJugableCaracteristicas()
                    {
                        Caracteristicas = new ModeloCaracteristicas
                        {
                        Contextura = "Super en forma",
                        EAlineamiento = EAlineamiento.LawfulGood,
                        Edad = 50,
                        EManoDominante = EManoDominante.Izquierda,
                        ESexo = ESexo.Masculino,
                        Estatura = 190,
                        Nacionalidad = "Inglesa"
                        }
                    }
                }
            };

            Masters = new List<ModeloMaster>
            {
                new ModeloMaster
                {
                    Nombre = "Lepibe",
                    EClaseDeSuServant = EClaseServant.Berserker, 
                    Str = 10,
                    Agi = 11,
                    End = 10,
                    Intel = 13,
                    Lck = 16,
                    Chr = 15,

                    Caracteristicas = new TIPersonajeJugableCaracteristicas
                    {
                        Caracteristicas = new ModeloCaracteristicas
                        {
                        Contextura = "Fuera de forma",
                        EAlineamiento = EAlineamiento.NeutralGood,
                        Edad = 19,
                        EManoDominante = EManoDominante.Derecha,
                        ESexo = ESexo.Masculino,
                        Estatura = 170,
                        Nacionalidad = "Estadounidense"
                        }
                    }
                },

                new ModeloMaster
                {
                    Nombre = "Charles",
                    EClaseDeSuServant = EClaseServant.Saber,

                    Caracteristicas = new TIPersonajeJugableCaracteristicas()
                    {
                        Caracteristicas = new ModeloCaracteristicas
                        {
                        Contextura = "Super entrenado",
                        EAlineamiento = EAlineamiento.Neutral,
                        Edad = 30,
                        EManoDominante = EManoDominante.Derecha,
                        ESexo = ESexo.Masculino,
                        Estatura = 190,
                        Nacionalidad = "Ingles"
                        }
                    }
                },

                new ModeloMaster
                {
                    Nombre = "Ricky Millones",
                    EClaseDeSuServant = EClaseServant.Archer,

                    Caracteristicas = new TIPersonajeJugableCaracteristicas()
                    {
                        Caracteristicas = new ModeloCaracteristicas
                        {
                        Contextura = "En forma",
                        EAlineamiento = EAlineamiento.Neutral,
                        Edad = 25,
                        EManoDominante = EManoDominante.Derecha,
                        ESexo = ESexo.Masculino,
                        Estatura = 200,
                        Nacionalidad = "No se la verdad"
                        }
                    }
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

            ComandoBotonFichasMasters = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                for (int i = 0; i < Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Masters[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Establecemos que el tipo de personaje seleccionado fue Master
                ETipoPersonajeSeleccionado = ETipoPersonaje.Master;

                SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipalRol>().EMenuActual =
                    EMenuActualRol.VistaFichas;
            });

            ComandoBotonFichasInvocaciones = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                for (int i = 0; i < Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(Invocaciones[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Establecemos que el tipo de personaje seleccionado fue Invocacion
                ETipoPersonajeSeleccionado = ETipoPersonaje.Invocacion;

                SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipalRol>().EMenuActual =
                    EMenuActualRol.VistaFichas;
            });

            ComandoBotonFichasNPCs = new Comando(() =>
            {
                List<ViewModelFichaItem> fichasTemp = new List<ViewModelFichaItem>();

                for (int i = 0; i < Servants.Count; ++i)
                    fichasTemp.Add(new ViewModelFichaItem(NPCs[i]));

                SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>().ViewModelListaFichas.FichaItems = fichasTemp;

                //Establecemos que el tipo de personaje seleccionado fue NPC
                ETipoPersonajeSeleccionado = ETipoPersonaje.NPC;

                SistemaPrincipal.ObtenerInstancia<ViewModelPaginaPrincipalRol>().EMenuActual =
                    EMenuActualRol.VistaFichas;
            });
        }
    }
}
