using System;

namespace AppGMCore
{
    /// <summary>
    /// Modelo de datos para el personaje
    /// </summary>
    public class ModeloPersonaje
    {
        //ID
        public int IdPersonaje { get; set; }

        //Nombre del personaje
        public string Nombre { get; set; }
        //Stats del personaje
        public short Hp { get; set; }
        public ushort Str { get; set; }
        public ushort End { get; set; }
        public ushort Agi { get; set; }
        public ushort Intel { get; set; }
        public ushort Lck { get; set; }

        //Estado del personaje (en combate o no en combate)
        public bool EstaEnCombate { get; set; }

        //Posicion del personaje en el mapa
        public Vector2D Posicion { get; set; }

        //TODO: Terminar la db
        //public List<ModeloEfecto> Efectos
        //public List<ModeloUtilizable> Inventario
        //public List<ModeloDefensivo> Armadura
        //public List<ModeloPersonaje> Aliados
        //public List<ModeloPerk> Perks
        //public List<ModeloHabilidad> Skills
        //public List<ModeloMagia> Magias
        //public List<ModeloModificadorDeDefensa> ModificadoresDeDefensa
        //
        //private List<ControladorEfecto<ModeloEfecto>> controladorEfectos
        //private List<ControladorUtilizable<ModeloUtilizable>> controladorInventario
        //private List<ControladorDefensivo<ModeloDefensivo>> controladorArmadura
        //private List<ControladorPersonaje<ModeloPersonaje>> controladorAliados
        //private List<ControladorPerk<ModeloPerk>> controladorPerks
        //private List<ControladorHabilidad<ModeloHabilidad>> controladorSkills
        //private List<ControladorMagia<ModeloMagia>> controladorMagias
        //private List<ControladorModificadorDeDefensa<ModeloModificadorDeDefensa>> controladorModificadoresDeDefens
    }
}
