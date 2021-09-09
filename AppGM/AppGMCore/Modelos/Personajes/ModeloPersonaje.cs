using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje : ModeloConVariablesYTiradas<TIVariablePersonaje, TITiradaPersonaje>
    {
        public ControladorPersonaje controlador;

        /// <summary>
        /// Relacion rol
        /// </summary>
        public virtual TIRolPersonaje RolPersonaje { get; set; }

        /// <summary>
        /// Nombre del personaje
        /// </summary>
        [StringLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Puede ser un personaje Master, Servant, Invocacion, o NPC
        /// </summary>
        public ETipoPersonaje TipoPersonaje { get; set; }

        /// <summary>
        /// Numero de party (equipo) a la que el personaje pertenece.
        /// </summary>
        public ENumeroParty NumeroParty { get; set; }

        //Stats del personaje
        public int MaxHp { get; set; }
        public int Hp    { get; set; }
        public int Str   { get; set; }
        public int End   { get; set; }
        public int Agi   { get; set; }
        public int Int   { get; set; }
        public int Lck   { get; set; }
               
        //Ventajas stats del personaje
        public int VentajaStr   { get; set; }
        public int VentajaEnd   { get; set; }
        public int VentajaAgi   { get; set; }
        public int VentajaInt   { get; set; }
        public int VentajaLck   { get; set; }

        /// <summary>
        /// Maxima cantidad de pesos que puede cargar el personaje
        /// </summary>
        public decimal PesoMaximoCargable   { get; set; }
        /// <summary>
        /// Cantidad de peso siendo cargado actualmente
        /// </summary>
        public decimal PesoCargado { get; set; }

        /// <summary>
        /// Ruta relativa a la imagen del personaje
        /// </summary>
        public string PathImagenRelativo { get; set; }

        /// <summary>
        /// Ruta completa a la imagen del personaje
        /// </summary>
        public string PathImagenAbsoluto { get; set; }

        /// <summary>
        /// Estado del personaje (en combate o no en combate)
        /// </summary>
        public bool EstaEnCombate { get; set; }

        /// <summary>
        /// Formato de la imagen asignada al personaje
        /// </summary>
        public EFormatoImagen FormatoImagen { get; set; }

        /// <summary>
        /// Posicion del personaje en el mapa
        /// </summary>
        public virtual ModeloVector2 Posicion { get; set; }

        /// <summary>
        /// Efectos aplicados sobre el personaje
        /// </summary>
        public virtual List<TIPersonajeEfectoSiendoAplicado> EfectosAplicandose { get; set; } = new List<TIPersonajeEfectoSiendoAplicado>();
        
        //Equipamiento del personaje
        public virtual List<TIPersonajeUtilizable>           Inventario { get; set; } = new List<TIPersonajeUtilizable>();
        public virtual List<TIPersonajeDefensivo>            Armadura   { get; set; } = new List<TIPersonajeDefensivo>();
        public virtual List<TIPersonajeArmaDistancia>        ArmasDistancia         { get; set; } = new List<TIPersonajeArmaDistancia>();

        /// <summary>
        /// Contratos realizados con tros personajes
        /// </summary>
        public virtual List<TIPersonajeContrato>      Contratos  { get; set; } = new List<TIPersonajeContrato>();

        /// <summary>
        /// Alianzas con otros personajes
        /// </summary>
        public virtual List<TIPersonajeAlianza>       Alianzas   { get; set; } = new List<TIPersonajeAlianza>();
                                              
        //Habilidades del personaje
        public virtual List<TIPersonajePerk>          Perks      { get; set; } = new List<TIPersonajePerk>();
        public virtual List<TIPersonajeHabilidad>     Skills     { get; set; } = new List<TIPersonajeHabilidad>();
        public virtual List<TIPersonajeMagia>         Magias     { get; set; } = new List<TIPersonajeMagia>();
        
        /// <summary>
        /// Modificadores de defensa del personaje.
        /// </summary>
        public virtual List<TIPersonajeModificadorDeDefensa> ModificadoresDeDefensa { get; set; } = new List<TIPersonajeModificadorDeDefensa>();
        
        /// <summary>
        /// Es participante en algun combate.
        /// TODO: Solo puede participar de un combate activo a la vez.
        /// </summary>
        public virtual List<TIParticipantePersonaje> ParticipacionEnCombates { get; set; } = new List<TIParticipantePersonaje>();

        /// <summary>
        /// Especialidades de este personaje
        /// </summary>
        public virtual List<TIEspecialidadPersonaje> Especialidades { get; set; } = new List<TIEspecialidadPersonaje>();
    }
}