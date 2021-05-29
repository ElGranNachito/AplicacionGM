using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje : ModeloBase
    {
        /// <summary>
        /// Relacion rol
        /// </summary>
        public TIRolPersonaje RolPersonaje { get; set; }

        public ControladorPersonaje controlador;

        /// <summary>
        /// Nombre del personaje
        /// </summary>
        [StringLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Puede ser un personaje Master, Servant, Invocacion, o NPC
        /// </summary>
        public ETipoPersonaje TipoPersonaje{ get; set; }

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

        //Pesos que maneja el personaje
        public decimal PesoMaximoCargable   { get; set; }
        public decimal PesoCargado { get; set; }

        /// <summary>
        /// Imagencita del personaje
        /// </summary>
        public string PathImagen { get; set; }

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
        public ModeloVector2 Posicion { get; set; }

        /// <summary>
        /// Efectos sobre el personaje
        /// </summary>
        public List<TIPersonajeEfecto>        Efectos    { get; set; } = new List<TIPersonajeEfecto>();
        
        //Equipamiento del personaje
        public List<TIPersonajeUtilizable>    Inventario { get; set; } = new List<TIPersonajeUtilizable>();
        public List<TIPersonajeDefensivo>     Armadura   { get; set; } = new List<TIPersonajeDefensivo>();
        public List<TIPersonajeArmaDistancia> ArmasDistancia         { get; set; } = new List<TIPersonajeArmaDistancia>();

        //Contratos realizados con tros personajes
        public List<TIPersonajeContrato>      Contratos  { get; set; } = new List<TIPersonajeContrato>();
        //Alianzas con otros personajes
        public List<TIPersonajeAlianza>       Alianzas   { get; set; } = new List<TIPersonajeAlianza>();
                                              
        //Habilidades del personaje
        public List<TIPersonajePerk>          Perks      { get; set; } = new List<TIPersonajePerk>();
        public List<TIPersonajeHabilidad>     Skills     { get; set; } = new List<TIPersonajeHabilidad>();
        public List<TIPersonajeMagia>         Magias     { get; set; } = new List<TIPersonajeMagia>();
        
        //Modificadores de defensa del personaje
        public List<TIPersonajeModificadorDeDefensa> ModificadoresDeDefensa { get; set; } = new List<TIPersonajeModificadorDeDefensa>();
        
        //Es participante en algun combate:
        public List<TIParticipantePersonaje> ParticipacionEnCombates { get; set; } = new List<TIParticipantePersonaje>();

    }
}
