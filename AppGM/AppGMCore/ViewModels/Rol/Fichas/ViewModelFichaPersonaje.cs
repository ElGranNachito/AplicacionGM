using System;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{

    /// <summary>
    /// View model para un item en un ItemControl que contiene datos basicos de una ficha de un personaje
    /// </summary>
    public class ViewModelFichaPersonaje : ViewModelConResultado<ViewModelFichaPersonaje>
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// Controlador del personaje.
        /// </summary>
        private ControladorPersonaje personaje;


        // Propiedades ---


        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el menu item 'Ver ficha'
        /// </summary>
        public ICommand ComandoVerFicha { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el menu item 'Editar'
        /// </summary>
        public ICommand ComandoEditar { get; set; }

        /// <summary>
        /// Comando que se ejecuta cuando el usuario presiona el menu item 'Eliminar'
        /// </summary>
        public ICommand ComandoEliminar { get; set; }

        /// <summary>
        /// Vida maxima del personaje.
        /// </summary>
        public int MaxHp => personaje.MaxHp;

        /// <summary>
        /// Vida actual del personaje.
        /// </summary>
        public int Hp => personaje.Hp;

        /// <summary>
        /// Stat de carisma
        /// </summary>
        public int Chr => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).Chr : 0;

        /// <summary>
        /// Modificador en fuerza del personaje.
        /// </summary>
        public int ModificadorStr { get; set; }

        /// <summary>
        /// Modificador en resistencia del personaje.
        /// </summary>
        public int ModificadorEnd { get; set; }

        /// <summary>
        /// Modificador en agilidad del personaje.
        /// </summary>
        public int ModificadorAgi { get; set; }
        
        /// <summary>
        /// Modificador en inteligencia del personaje.
        /// </summary>
        public int ModificadorInt { get; set; }

        /// <summary>
        /// Modificador en suerte del personaje.
        /// </summary>
        public int ModificadorLck { get; set; }

        /// <summary>
        /// Modificador en stat de carisma
        /// </summary>
        public int ModificadorChr { get; set; }

        /// <summary>
        /// Ventaja en fuerza del personaje.
        /// </summary>
        public int VentajaStr => personaje.VentajaStr;

        /// <summary>
        /// Ventaja en resistencia del personaje.
        /// </summary>
        public int VentajaEnd => personaje.VentajaEnd;

        /// <summary>
        /// Ventaja en agilidad del personaje.
        /// </summary>
        public int VentajaAgi => personaje.VentajaAgi;
        
        /// <summary>
        /// Ventaja en inteligencia del personaje.
        /// </summary>
        public int VentajaInt => personaje.VentajaInt;

        /// <summary>
        /// Ventaja en suerte del personaje.
        /// </summary>
        public int VentajaLck => personaje.VentajaLck;

        /// <summary>
        /// Ventaja en stat de carisma
        /// </summary>
        public int VentajaChr => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).VentajaChr : 0;

        /// <summary>
        /// Energia vital total del personaje
        /// </summary>
        public int Od => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).OdTotal : 0;
        
        /// <summary>
        /// Energia vital actual del personaje
        /// </summary>
        public int OdActual => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).OdActual : 0;
        
        /// <summary>
        /// Energia magica concentrada total del personaje.
        /// </summary>
        public int Mana => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).ManaTotal : 0;

        /// <summary>
        /// Energia magica concentrada actual del personaje.
        /// </summary>
        public int ManaActual => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).ManaActual : 0;

        /// <summary>
        /// Energia magica total del personaje.
        /// </summary>
        public int Prana => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? ((ModeloServant) personaje.modelo).Prana : 0;

        /// <summary>
        /// Energia magica actual del personaje.
        /// </summary>
        public int PranaActual => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? ((ModeloServant) personaje.modelo).PranaActual : 0;

        /// <summary>
        /// Command spells disponibles del personaje.
        /// </summary>
        public ushort CommandSpells => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? ((ModeloMaster) personaje.modelo).CommandSpells : ushort.MinValue;

        /// <summary>
        /// Edad del personaje.
        /// </summary>
        public int Edad => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Edad : 0;

        /// <summary>
        /// Estatura del personaje.
        /// </summary>
        public int Estatura => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Estatura : 0;

        /// <summary>
        /// Peso del personaje.
        /// </summary>
        public int Peso => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Peso : 0;

        /// <summary>
        /// Imagen del personaje.
        /// </summary>
        public byte[] Imagen => personaje.modelo.Imagen;

        /// <summary>
        /// Peso maximo que puede cargar el personaje.
        /// </summary>
        public decimal PesoMaximoCargable => personaje.modelo.PesoMaximoCargable;
        
        /// <summary>
        /// Peso siendo cargado por el personaje.
        /// </summary>
        public decimal PesoCargado => personaje.modelo.PesoCargado;

        /// <summary>
        /// Indica si la ficha fue seleccionada.
        /// </summary>
        public bool EstaSeleccionada { get; set; }

        /// <summary>
        /// Nombre del personaje.
        /// </summary>
        public string Nombre => personaje.modelo.Nombre;

        /// <summary>
        /// Puede ser un personaje Master, Servant, Invocacion, o NPC
        /// </summary>
        public string TipoDelPersonaje => Enum.GetName(personaje.modelo.TipoPersonaje);

        /// <summary>
        /// Party a la que pertenece el personaje.
        /// </summary>
        public string NumeroDeParty => EnumHelpers.ToStringNumeroParty(personaje.modelo.NumeroParty);

        /// <summary>
        /// Nacionalidad del personaje.
        /// </summary>
        public string Nacionalidad => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Nacionalidad : string.Empty;

        /// <summary>
        /// Origen del personaje.
        /// </summary>
        public string Origen => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Origen : string.Empty;

        /// <summary>
        /// Afinidad del personaje.
        /// </summary>
        public string Afinidad => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Afinidad : string.Empty;

        /// <summary>
        /// Fisico del personaje.
        /// </summary>
        public string Fisico => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Fisico : string.Empty;

        /// <summary>
        /// Arquetipo del personaje.
        /// </summary>
        public string Arquetipo => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? Enum.GetName(((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Arquetipo) : string.Empty;

        /// <summary>
        /// Mano dominante del personaje.
        /// </summary>
        public string ManoDominante => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? Enum.GetName(((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.ManoDominante) : string.Empty;

        /// <summary>
        /// Sexo del personaje.
        /// </summary>
        public string Sexo => (personaje.modelo.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0 ? Enum.GetName(((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Sexo) : string.Empty;

        /// <summary>
        /// Bienestar del personaje.
        /// </summary>
        public string Bienestar => personaje.modelo.TipoPersonaje == ETipoPersonaje.Master ? Enum.GetName(((ModeloMaster) personaje.modelo).EBienestar) : string.Empty;

        /// <summary>
        /// Fuerza del personaje.
        /// </summary>
        public string Str => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? Enum.GetName(EnumHelpers.ARango(personaje.Str)) : personaje.Str.ToString();

        /// <summary>
        /// Resistencia del personaje.
        /// </summary>
        public string End => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? Enum.GetName(EnumHelpers.ARango(personaje.End)) : personaje.End.ToString();

        /// <summary>
        /// Agilidad del personaje.
        /// </summary>
        public string Agi => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? Enum.GetName(EnumHelpers.ARango(personaje.Agi)) : personaje.Agi.ToString();

        /// <summary>
        /// Inteligencia del personaje.
        /// </summary>
        public string Int => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? Enum.GetName(EnumHelpers.ARango(personaje.Int)) : personaje.Int.ToString();

        /// <summary>
        /// Suerte del personaje.
        /// </summary>
        public string Lck => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? Enum.GetName(EnumHelpers.ARango(personaje.Lck)) : personaje.Lck.ToString();

        /// <summary>
        /// Rango del Noble Phantasm del personaje si es servant.
        /// </summary>
        public string RangoNP => personaje.modelo.TipoPersonaje == ETipoPersonaje.Servant ? Enum.GetName(((ModeloServant)personaje.modelo).RangoNP) : string.Empty;

        /// <summary>
        /// VM del inventario del personaje.
        /// </summary>
        public ViewModelInventario Inventario { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_personaje">Personaje que representa esta ficha</param>
        public ViewModelFichaPersonaje(ControladorPersonaje _personaje)
        {
            personaje = _personaje;

            Inventario = new ViewModelInventario(personaje.modelo, personaje);

            ModificadorStr = Helpers.Juego.ObtenerModificadorStat(personaje.Str);
            ModificadorEnd = Helpers.Juego.ObtenerModificadorStat(personaje.End);
            ModificadorAgi = Helpers.Juego.ObtenerModificadorStat(personaje.Agi);
            ModificadorInt = Helpers.Juego.ObtenerModificadorStat(personaje.Int);
            ModificadorLck = Helpers.Juego.ObtenerModificadorStat(personaje.Lck);

            ModificadorChr = Helpers.Juego.ObtenerModificadorStat(Chr);

            ComandoVerFicha = new Comando(VerFicha);
            ComandoEditar = new Comando(EditarPersonaje);
            ComandoEliminar = new Comando(EliminarPersonaje);
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion llamada para ver la ficha completa de un personaje.
        /// </summary>
        public async void VerFicha()
        {
            await SistemaPrincipal.MostrarMensajeAsync(this, $"Ficha {Nombre}", false, 450, 800);
        }

        /// <summary>
        /// Funcion llamada para editar a un personaje.
        /// </summary>
        public async void EditarPersonaje()
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
                    }

                }, personaje).Inicializar());
        }

        /// <summary>
        /// Funcion llamada para eliminar a un personaje.
        /// </summary>
        public void EliminarPersonaje()
        {
            switch (personaje.modelo.TipoPersonaje)
            {
                case ETipoPersonaje.Master:
                    SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionFicha>().Masters.Remove(this);
                    break;
                case ETipoPersonaje.Servant:
                    SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionFicha>().Servants.Remove(this);
                    break;
                case ETipoPersonaje.Invocacion:
                    SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionFicha>().Invocaciones.Remove(this);
                    break;
                case ETipoPersonaje.NPC:
                    SistemaPrincipal.ObtenerInstancia<ViewModelMenuSeleccionFicha>().NPCs.Remove(this);
                    break;
            }

            personaje.Eliminar();

            SistemaPrincipal.GuardarDatosAsync();
        }

        #endregion
    }
}
