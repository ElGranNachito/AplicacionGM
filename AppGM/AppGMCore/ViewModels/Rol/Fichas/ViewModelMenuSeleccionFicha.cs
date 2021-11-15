using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using AppGM.Core;
using Castle.Core.Internal;

namespace AppGM
{
    /// <summary>
    /// VM que representa un control que permite seleccionar el tipo de fichas que explorar
    /// </summary>
    public class ViewModelMenuSeleccionFicha : ViewModel
    {
        #region Miembros

        // Campos ---



        // Propiedades ---


        /// <summary>
        /// Comando que se ejecuta cuando se presiona el boton 'Añadir'.
        /// </summary>
        public ICommand ComandoAñadirPersonaje { get; set; }

        /// <summary>
        /// VMs de fichas masters.
        /// </summary>
        public ObservableCollection<ViewModelFichaPersonaje> Masters { get; set; } = new ObservableCollection<ViewModelFichaPersonaje>();

        /// <summary>
        /// VMs de fichas servants.
        /// </summary>
        public ObservableCollection<ViewModelFichaPersonaje> Servants { get; set; } = new ObservableCollection<ViewModelFichaPersonaje>();

        /// <summary>
        /// VMs de fichas invocaciones.
        /// </summary>
        public ObservableCollection<ViewModelFichaPersonaje> Invocaciones { get; set; } = new ObservableCollection<ViewModelFichaPersonaje>();
        
        /// <summary>
        /// VMs de fichas NPCs.
        /// </summary>
        public ObservableCollection<ViewModelFichaPersonaje> NPCs { get; set; } = new ObservableCollection<ViewModelFichaPersonaje>();

        #endregion

        #region Constructores

        /// <summary>
        /// Controlador
        /// </summary>
        public ViewModelMenuSeleccionFicha()
        {
            if (!SistemaPrincipal.DatosRolSeleccionado.Masters.IsNullOrEmpty())
            {
                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.Masters.Count; ++i)
                    Masters.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.Masters[i]));
            }

            if (!SistemaPrincipal.DatosRolSeleccionado.Servants.IsNullOrEmpty())
            {
                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.Servants.Count; ++i)
                    Servants.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.Servants[i]));
            }
            
            if (!SistemaPrincipal.DatosRolSeleccionado.Invocaciones.IsNullOrEmpty())
            {
                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Count; ++i)
                    Invocaciones.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.Invocaciones[i]));
            }

            if (!SistemaPrincipal.DatosRolSeleccionado.NPCs.IsNullOrEmpty())
            {
                for (int i = 0; i < SistemaPrincipal.DatosRolSeleccionado.NPCs.Count; ++i)
                    NPCs.Add(new ViewModelFichaPersonaje(SistemaPrincipal.DatosRolSeleccionado.NPCs[i]));
            }

            ComandoAñadirPersonaje = new Comando(AñadirPersonaje);
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion llamada para añadir un nuevo personaje.
        /// </summary>
        public async void AñadirPersonaje()
        {
            SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionEdicionPersonaje, ModeloPersonaje, ControladorPersonaje>(
                await new ViewModelCreacionEdicionPersonaje(async vm =>
                {
                    if (vm.Resultado.EsAceptarOFinalizar())
                    {
                        var nuevoPersonaje = vm.CrearControlador();

                        if (vm.EstaEditando)
                        {
                            var resultado = await nuevoPersonaje.modelo.CrearCopiaProfundaEnSubtipoAsync(vm.ModeloSiendoEditado.GetType(), vm.ModeloSiendoEditado);

                            await resultado.modelosCreadosEliminados.GuardarYEliminarModelosAsync();
                        }
                        else
                        {
                            await SistemaPrincipal.GuardarDatosAsync();
                        }

                        switch (nuevoPersonaje.modelo.TipoPersonaje)
                        {
                            case ETipoPersonaje.Master:
                                Masters.Add(new ViewModelFichaPersonaje(nuevoPersonaje));
                                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Masters)));
                                break;
                            case ETipoPersonaje.Servant:
                                Servants.Add(new ViewModelFichaPersonaje(nuevoPersonaje));
                                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Servants)));
                                break;
                            case ETipoPersonaje.Invocacion:
                                Invocaciones.Add(new ViewModelFichaPersonaje(nuevoPersonaje));
                                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Invocaciones)));
                                break;
                            case ETipoPersonaje.NPC:
                                NPCs.Add(new ViewModelFichaPersonaje(nuevoPersonaje));
                                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(NPCs)));
                                break;
                        }
                    }

                }).Inicializar());
        }

        #endregion
    }
}
