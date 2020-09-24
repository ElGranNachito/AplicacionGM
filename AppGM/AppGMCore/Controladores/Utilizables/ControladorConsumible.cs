namespace AppGMCore
{
    class ControladorConsumible<TipoConsumible> : ControladorUtilizable<ModeloConsumible>
    {
        #region Funciones

        public void Recargar(ushort cant)
        {
            //TODO: Recargar la cantidad pasada en el argumento.
        }

        #endregion
    }

    class ControladorArmaDistancia : ControladorConsumible<ModeloArmasDistancia>
    {
        #region Funciones

        public override void Utilizar(ControladorPersonaje<ModeloPersonaje> usuario, ControladorPersonaje<ModeloPersonaje>[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            //TODO: Realizar la tirada de utilización. Calcular la tirada mínima necesaria para impactar al enemigo.
        }

        #endregion
    }
}