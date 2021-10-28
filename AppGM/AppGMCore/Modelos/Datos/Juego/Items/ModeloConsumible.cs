namespace AppGM.Core
{
	public class ModeloConsumible : ModeloItem
    {
        /// <summary>
        /// Cantidad de usos que maximos que puede tener el consumible
        /// </summary>
        public ushort Usos { get; set; }
       
        /// <summary>
        /// Cantidad de usos que le quedan al consumible
        /// </summary>
        public ushort UsosRestantes { get; set; }
    }
}
