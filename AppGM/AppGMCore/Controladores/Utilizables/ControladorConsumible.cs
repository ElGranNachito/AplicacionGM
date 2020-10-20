using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorConsumible: ControladorUtilizable
    {
        #region Constructor

        public ControladorConsumible()
        {
        }

        public ControladorConsumible(ModeloConsumible _modeloConsumible)
        {
            modelo = _modeloConsumible;
        }

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

        public ControladorArmaDistancia(ModeloArmasDistancia _modeloArmasDistancia)
        {
            modelo = _modeloArmasDistancia;
        }

        #endregion

        #region Funciones

        public override void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilización. Calcular la tirada mínima necesaria para impactar al enemigo.
        }

        #endregion
    }
}