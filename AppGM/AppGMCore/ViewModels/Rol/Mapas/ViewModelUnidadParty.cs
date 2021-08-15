using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelUnidadParty : ViewModelIngresoPosicion
    {
        #region Campos & Propiedades

        /// <summary>
        /// Unidades de personajes que pertenecen a esta party.
        /// </summary>
        public ViewModelListaDeElementos<ViewModelIngresoPosicion> PersonajesParty { get; set; } = new ViewModelListaDeElementos<ViewModelIngresoPosicion>();

        /// <summary>
        /// Numero con el que se identifica al equipo de persoanjes.
        /// </summary>
        public ENumeroParty NumeroParty
        {
            get
            {
                if (PersonajesParty.Elementos.All(a => a == PersonajesParty.Elementos[0]))
                {
                    return PersonajesParty.Elementos[0].unidad.personaje.modelo.NumeroParty;
                }
                else
                    return ENumeroParty.NINGUNO;
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="_mapa">Viewmodel del mapa</param>
        /// <param name="_unidad">Contralador de la unidad</param>
        public ViewModelUnidadParty(ViewModelMapa _mapa, ControladorUnidadMapa _unidad) : 
            base(_mapa, _unidad)
        {
            ComandoEliminarUnidad = new Comando(EliminarUnidad);
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Elimina esta unidad del mapa y de la base de datos
        /// </summary>
        private void EliminarUnidad()
        {
            mapa.PosicionesParties.Remove(this);

            unidad.Eliminar();

            SistemaPrincipal.GuardarDatosRolAsincronicamente();
        }

        #endregion

    }
}
