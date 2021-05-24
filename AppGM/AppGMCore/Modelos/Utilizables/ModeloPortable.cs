using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloPortable : ModeloUtilizable
    {
        public ControladorPortable controladorPortable;

        /// <summary>
        /// Condicion porcentual del portable
        /// </summary>
        public int Estado { get; set; }

        /// <summary>
        /// Slots que aporta el portable
        /// </summary>
        public List<TIPortableSlots> Slots { get; set; }

        /// <summary>
        /// Ventajas y desventajas de tener equipado el portable
        /// Primer indice son las ventajas, Segundo indice son las desventajas
        /// </summary>
        public List<TIPortableModificadorDeStatBase> VentajasYDesventajasDeEquiparlo { get; set; } = new List<TIPortableModificadorDeStatBase>();
    }

    public class ModeloDefensivo : ModeloPortable
    {
        public ControladorDefensivo controladorDefensivo;
    }

    public class ModeloDefensivoAbsoluto : ModeloDefensivo
    {
        public ControladorDefensivoAbsoluto controladorDefensivoAbsoluto;

        /// <summary>
        /// Cantidad de usos maximos del defensivo absoluto
        /// </summary>
        public short Usos { get; set; }
        /// <summary>
        /// Usos que le quedan al defensivo absoluto
        /// </summary>
        public short UsosRestantes { get; set; }
    }

    public class ModeloOfensivo : ModeloPortable, IInfligeDaño
    {
        /// <summary>
        /// Tipos de daño que puede inflingir en su uso
        /// </summary>
        public ETipoDeDaño DañosQuePuedeInfligir { get; set; }
        
        /// <summary>
        /// Efectos que inflinge al ser utilizado
        /// </summary>
        public TIOfensivoEfecto EfectoQueInflige { get; set; }

        /// <summary>
        /// Tiradas de daño que puede realizar el portable ofensivo
        /// </summary>
        public List<TIOfensivoTiradaDeDaño> TiradasDeDaño { get; set; } = new List<TIOfensivoTiradaDeDaño>();
    }
}