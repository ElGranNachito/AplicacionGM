using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorConsumible<TipoConsumible> : ControladorUtilizable<ModeloConsumible>
    {
        #region Funciones

        public void Recargar(ushort cant)
        {
            //TODO: Recargar la cantidad pasada en el argumento.
        }

        #endregion
    }

    public class ControladorArmaDistancia : ControladorConsumible<ModeloArmasDistancia>
    {
        #region Controladores

        public ControladorTiradaVariable<ModeloTiradaDeDaño> ControladorTiradaDeDaño { get; set; }
        public ControladorTiradaVariable<ModeloTiradaVariable> ControladorTiradaVariable { get; set; }

        public List<ControladorEfecto<ModeloEfecto>> ControladorEfectoQueInflige { get; set; }

        #endregion

        #region Funciones

        public override void Utilizar(ControladorPersonaje<ModeloPersonaje> usuario, ControladorPersonaje<ModeloPersonaje>[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilización. Calcular la tirada mínima necesaria para impactar al enemigo.
        }

        #endregion
    }
}