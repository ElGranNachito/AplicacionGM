using System;
using AppGM.Core;

namespace AppGM
{

    /// <summary>
    /// View model para un item en un ItemControl que contiene datos basicos de una ficha de un personaje
    /// </summary>
    public class ViewModelFichaPersonaje : ViewModel
    {
        #region Miembros

        // Campos ---


        private ControladorPersonaje personaje;


        // Propiedades ---


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
        public ushort VentajaChr => ((ModeloMaster) personaje.modelo).VentajaChr;

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
        /// Peso maximo que puede cargar el personaje.
        /// </summary>
        public decimal PesoMaximoCargable => personaje.modelo.PesoMaximoCargable;
        
        /// <summary>
        /// Peso siendo cargado por el personaje.
        /// </summary>
        public decimal PesoCargado => personaje.modelo.PesoCargado;

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
        public string Bienestar => Enum.GetName(((ModeloMaster) personaje.modelo).Bienestar);

        public string RangoNP => Enum.GetName(((ModeloServant)personaje.modelo).RangoNP);

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_personaje">Personaje que representa esta ficha</param>
        public ViewModelFichaPersonaje(ControladorPersonaje _personaje)
        {
            personaje = _personaje;
        }

        #endregion

        #region Funciones

        

        #endregion
    }
}
