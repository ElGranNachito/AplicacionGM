using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene todos los datos necesarios para la creacion de un rol
	/// </summary>
    public class DatosCreacionRol
    {
        public ModeloRol modeloRol = new ModeloRol();

		public List<ModeloPersonaje> personajes                   = new List<ModeloPersonaje>();
		public List<ModeloPersonaje> servants                     = new List<ModeloPersonaje>();
		public List<ModeloPersonaje> masters                      = new List<ModeloPersonaje>();
		public List<ModeloPersonaje> invocaciones                 = new List<ModeloPersonaje>();
        public List<ModeloPersonaje> npcs                         = new List<ModeloPersonaje>();
		public List<ModeloMapa> mapas                             = new List<ModeloMapa>();
		public List<ModeloUtilizable> items                       = new List<ModeloUtilizable>();
		public List<ModeloPortable> portables                     = new List<ModeloPortable>();
		public List<ModeloPortable> portableOfensivo              = new List<ModeloPortable>();
		public List<ModeloDefensivo> defensivos                   = new List<ModeloDefensivo>();
		public List<ModeloDefensivoAbsoluto> defensivosAbsolutos  = new List<ModeloDefensivoAbsoluto>();
		public List<ModeloCondicion> consumibles                  = new List<ModeloCondicion>();
		public List<ModeloArmasDistancia> armasDistancia          = new List<ModeloArmasDistancia>();
		public List<ModeloSlot> slots                             = new List<ModeloSlot>();
		public List<ModeloPerk> perks                             = new List<ModeloPerk>();
		public List<ModeloHabilidad> skills                       = new List<ModeloHabilidad>();
		public List<ModeloHabilidad> noblePhantasms               = new List<ModeloHabilidad>();
		public List<ModeloMagia> magias                           = new List<ModeloMagia>();
		public List<ModeloEfecto> efectos                         = new List<ModeloEfecto>();
		public List<ModeloCondicion> condiciones                  = new List<ModeloCondicion>();
		public List<ModeloAdministradorDeCombate> combatesActivos = new List<ModeloAdministradorDeCombate>();
		public List<ModeloLimitador> limitadores                  = new List<ModeloLimitador>();
		public List<ModeloCargas> cargasHabilidades      = new List<ModeloCargas>();
		public List<ModeloParticipante> participantes             = new List<ModeloParticipante>();
	}
}
