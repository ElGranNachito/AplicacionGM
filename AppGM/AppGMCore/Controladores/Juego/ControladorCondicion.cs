using System.Linq.Expressions;

namespace AppGM.Core
{
    public class ControladorCondicion : Controlador<ModeloCondicion>
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

        public bool SeCumpleLaCondicion(ControladorPersonaje pj)
        {
            return false;
        }

        #endregion
    }
}