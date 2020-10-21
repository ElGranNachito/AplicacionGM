namespace AppGM.Core
{
    public class ModeloMaster : ModeloPersonajeJugable
    {
        //Clase de servant que posee el master
        public EClaseServant EClaseDeSuServant { get; set; }

        //Stat carisma
        public ushort Chr { get; set; }
        //Command spells disponibles
        public ushort CommandSpells { get; set; }
        
        //Lore del master
        public string Lore { get; set; }
    }
}