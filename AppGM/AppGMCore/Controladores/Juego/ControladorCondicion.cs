using System.Linq.Expressions;

namespace AppGM.Core
{
    public class ControladorCondicion : ControladorBase<ModeloCondicion>
    {
        #region Constructor

        public ControladorCondicion(ModeloCondicion _modeloCondicion)
        {
            modelo = _modeloCondicion;
        }

        #endregion

        #region Funciones

        //LambdaExpression mLambda

        public ControladorCondicion()
        {
            //TODO: Generar expresión
        }

        public bool SeCumpleLaCondicion(ControladorPersonaje<ModeloPersonaje> pj)
        {
            return false;
        }

        #endregion
    }
}