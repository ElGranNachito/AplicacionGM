﻿using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorMapa : Controlador<ModeloMapa>
    {
        #region Miembros

        public List<ControladorUnidadMapa> controladoresUnidadesMapa = new List<ControladorUnidadMapa>(); 

        #endregion

        #region Constructores
        //Constructor temporal, despues se eliminara
        public ControladorMapa()
        {

        }
        public ControladorMapa(ModeloMapa _modeloMapa)
        {
            modelo = _modeloMapa;

            for (int i = 0; i < controladoresUnidadesMapa.Count; ++i)
                controladoresUnidadesMapa.Add(new ControladorUnidadMapa(_modeloMapa.PosicionesUnidades[i].Unidad));
        }

        #endregion

        #region Funciones
        public string ObtenerExtension() => string.Format($".{modelo.EFormatoImagen.ToString().ToLower()}");

        #endregion

        #region Propiedades
        public string NombreMapa => modelo.NombreMapa; 

        #endregion
    }
}
