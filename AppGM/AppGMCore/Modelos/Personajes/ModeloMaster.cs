namespace AppGM.Core
{
    public class ModeloMaster : ModeloPersonajeJugable
    {
        //Clase de servant que posee el master
        public EClaseServant EClaseDeSuServant { get; set; }
        //Estado de bienestar del master
        public EBienestar EBienestar           { get; set; }

        //Energia magica del personaje
        public int Od         { get; set; }
        public int OdActual   { get; set; }
        public int Mana       { get; set; }
        public int ManaActual { get; set; }

        //Stat de carisma
        public ushort Chr           { get; set; }
        //Command spells disponibles
        public ushort CommandSpells { get; set; }

        //Lore del personaje
        public string Lore     { get; set; }
        //Condicion relacionada al lore del personaje
        public string Origen   { get; set; } 
        //Conceptos dominados por el master
        public string Afinidad { get; set; }
    }
}