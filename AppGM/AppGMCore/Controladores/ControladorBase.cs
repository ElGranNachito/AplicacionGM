﻿namespace AppGM.Core
{
    public class ControladorBase<TipoModelo>
        where TipoModelo: new()
    {
        public TipoModelo modelo;
    }
}