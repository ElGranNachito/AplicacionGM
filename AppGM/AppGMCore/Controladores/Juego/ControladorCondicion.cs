﻿using System.Linq.Expressions;

namespace AppGM.Core
{
    public class ControladorCondicion : ControladorBase<ModeloCondicion>
    {
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