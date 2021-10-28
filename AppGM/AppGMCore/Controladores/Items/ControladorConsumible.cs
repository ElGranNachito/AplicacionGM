using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorConsumible: ControladorItem
    {
        #region Constructor

        public ControladorConsumible(ModeloConsumible _modeloConsumible)
			:base(_modeloConsumible) {}

        #endregion

        #region Funciones

        public void Recargar(ushort cant)
        {
            //TODO: Recargar la cantidad pasada en el argumento.
        }

        #endregion
    }

    public class ControladorArmaDistancia : ControladorConsumible
    {
        #region Controladores

        public ControladorTiradaVariable ControladorTiradaDeDaño { get; set; }
        public ControladorTiradaVariable ControladorTiradaVariable { get; set; }

        public List<ControladorEfecto> ControladorEfectoQueInflige { get; set; }

        #endregion

        #region Constructor

        public ControladorArmaDistancia(ModeloArmaDistancia _modeloArmaDistancia)
			:base(_modeloArmaDistancia) {}

        #endregion

        #region Funciones

        public override void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilización. Calcular la tirada mínima necesaria para impactar al enemigo.
        }

        #endregion
    }
}