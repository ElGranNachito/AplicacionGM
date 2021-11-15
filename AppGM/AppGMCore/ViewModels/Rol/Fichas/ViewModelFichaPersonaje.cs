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
        /// Fuerza del personaje.
        /// </summary>
        public int Str => personaje.Str;

        /// <summary>
        /// Resistencia del personaje.
        /// </summary>
        public int End => personaje.End;

        /// <summary>
        /// Agilidad del personaje.
        /// </summary>
        public int Agi => personaje.Agi;

        /// <summary>
        /// Inteligencia del personaje.
        /// </summary>
        public int Int => personaje.Int;

        /// <summary>
        /// Suerte del personaje.
        /// </summary>
        public int Lck => personaje.Lck;

        /// <summary>
        /// Stat de carisma
        /// </summary>
        public int Chr => ((ModeloMaster) personaje.modelo).Chr;

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
        public int VentajaChr => ((ModeloMaster) personaje.modelo).VentajaChr;

        /// <summary>
        /// Energia vital total del personaje
        /// </summary>
        public int Od => ((ModeloMaster) personaje.modelo).Od;
        
        /// <summary>
        /// Energia vital actual del personaje
        /// </summary>
        public int OdActual => ((ModeloMaster) personaje.modelo).OdActual;
        
        /// <summary>
        /// Energia magica concentrada total del personaje.
        /// </summary>
        public int Mana => ((ModeloMaster) personaje.modelo).Mana;

        /// <summary>
        /// Energia magica concentrada actual del personaje.
        /// </summary>
        public int ManaActual => ((ModeloMaster) personaje.modelo).ManaActual;

        /// <summary>
        /// Energia magica total del personaje.
        /// </summary>
        public int Prana => ((ModeloServant) personaje.modelo).Prana;

        /// <summary>
        /// Energia magica actual del personaje.
        /// </summary>
        public int PranaActual => ((ModeloServant) personaje.modelo).PranaActual;

        /// <summary>
        /// Command spells disponibles del personaje.
        /// </summary>
        public ushort CommandSpells => ((ModeloMaster) personaje.modelo).CommandSpells;

        /// <summary>
        /// Edad del personaje.
        /// </summary>
        public int Edad => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Edad;

        /// <summary>
        /// Estatura del personaje.
        /// </summary>
        public int Estatura => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Estatura;

        /// <summary>
        /// Peso del personaje.
        /// </summary>
        public int Peso => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Peso;

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
        public string Nacionalidad => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Nacionalidad;

        /// <summary>
        /// Origen del personaje.
        /// </summary>
        public string Origen => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Origen;

        /// <summary>
        /// Afinidad del personaje.
        /// </summary>
        public string Afinidad => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Afinidad;

        /// <summary>
        /// Fisico del personaje.
        /// </summary>
        public string Fisico => ((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Fisico;

        /// <summary>
        /// Arquetipo del personaje.
        /// </summary>
        public string Arquetipo => Enum.GetName(((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Arquetipo);

        /// <summary>
        /// Mano dominante del personaje.
        /// </summary>
        public string ManoDominante => Enum.GetName(((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.ManoDominante);

        /// <summary>
        /// Sexo del personaje.
        /// </summary>
        public string Sexo => Enum.GetName(((ModeloPersonajeJugable) personaje.modelo).Caracteristicas.Sexo);

        /// <summary>
        /// Bienestar del personaje.
        /// </summary>
        public string Bienestar => Enum.GetName(((ModeloMaster) personaje.modelo).EBienestar);

        /// <summary>
        /// Rango del Noble Phantasm del personaje si es servant.
        /// </summary>
        public string RangoNP => Enum.GetName(((ModeloServant)personaje.modelo).RangoNP);

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

            ModificadorStr = Helpers.Juego.ObtenerModificadorStat(Str);
            ModificadorAgi = Helpers.Juego.ObtenerModificadorStat(Agi);
            ModificadorEnd = Helpers.Juego.ObtenerModificadorStat(End);
            ModificadorInt = Helpers.Juego.ObtenerModificadorStat(Int);
            ModificadorLck = Helpers.Juego.ObtenerModificadorStat(Lck);
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
