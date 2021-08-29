using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AppGM.Core
{
    public class ViewModelUnidadParty : ViewModelIngresoPosicion
    {
        #region Campos & Propiedades

        // Campos ---


        /// <summary>
        /// Indica si se debe mostrar la imagen de la unidad sobre el mapa.
        /// </summary>
        private bool imagenPosicionEsVisible = false;


        // Propiedades ---


        /// <summary>
        /// Unidades de personajes que pertenecen a esta party.
        /// </summary>
        public ViewModelListaDeElementos<ViewModelIngresoPosicion> PersonajesParty { get; set; } = new ViewModelListaDeElementos<ViewModelIngresoPosicion>();

        /// <summary>
        /// Numero con el que se identifica al equipo de persoanjes.
        /// </summary>
        public ENumeroParty NumeroParty { get; set; }

        /// <summary>
        /// Nombre de la unidad party
        /// </summary>
        public string Nombre => NumeroParty.ToStringNumeroParty();

        /// <summary>
        /// Ruta de la imagen de la unidad
        /// </summary>
        public string PathImagen
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder("../../../../Media/Imagenes/Iconos/Parties/");

                switch (NumeroParty)
                {
                    case ENumeroParty.Party_0: stringBuilder.Append("Party_0");
                        break;
                    case ENumeroParty.Party_1: stringBuilder.Append("Party_1");
                        break;
                    case ENumeroParty.Party_2: stringBuilder.Append("Party_2");
                        break;
                    case ENumeroParty.Party_3: stringBuilder.Append("Party_3");
                        break;
                    case ENumeroParty.Party_4: stringBuilder.Append("Party_4");
                        break;
                    case ENumeroParty.Party_5: stringBuilder.Append("Party_5");
                        break;
                    case ENumeroParty.Party_6: stringBuilder.Append("Party_6");
                        break;
                    case ENumeroParty.Party_7: stringBuilder.Append("Party_7");
                        break;
                }

                stringBuilder.Append(".png");

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Indica si el indicador en el mapa debe ser visible o no
        /// </summary>
        public bool ImagenPosicionEsVisible
        {
            get => imagenPosicionEsVisible;
            set
            {
                imagenPosicionEsVisible = value;

                mapa.UnidadesPartiesVisibles.Add(this);

                if (PersonajesParty.All(a => !a.ModoPartyHabilitado))
                {
                    for (int i = 0; i < PersonajesParty.Count; ++i) 
                        PersonajesParty[i].ModoPartyHabilitado = value;
                }
                else if (PersonajesParty.All(a => a.ModoPartyHabilitado))
                {
                    for (int i = 0; i < PersonajesParty.Count; ++i) 
                        PersonajesParty[i].ModoPartyHabilitado = value;                    
                }
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
