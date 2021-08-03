using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloItem : ModeloUtilizable
    {
        /// <summary>
        /// Slots que ocupa el item
        /// </summary>
        public decimal SlotsQueOcupa { get; set; }
    }

    public class ModeloConsumible : ModeloItem
    {
        public ControladorConsumible controladorConsumible;

        /// <summary>
        /// Cantidad de usos que maximos que puede tener el consumible
        /// </summary>
        public ushort Usos { get; set; }
        ///Cantidad de usos que le quedan al consumible
        public ushort UsosRestantes { get; set; }
    }

    public class ModeloArmasDistancia : ModeloConsumible, IInfligeDaño
    {
        public ControladorArmaDistancia controladorArmaDistancia;

        /// <summary>
        /// Tirada para el daño que aplique el arma
        /// </summary>
        public virtual TIArmasDistanciaTiradaDeDaño TiradaDeDaño { get; set; }

        /// <summary>
        /// Tirada variable para su uso en rafaga
        /// </summary>
        public virtual TIArmasDistanciaTiradaVariable TiradaRafaga { get; set; }

        /// <summary>
        /// Tipo de daño que puede inflingir en su uso
        /// </summary>
        public ETipoDeDaño TipoDeDañoQueInflige { get; set; }

        /// <summary>
        /// Efecto infligido por el arma
        /// </summary>
        public virtual List<TIArmasDistanciaEfecto> EfectoQueInflige { get; set; } = new List<TIArmasDistanciaEfecto>();
    }
}
