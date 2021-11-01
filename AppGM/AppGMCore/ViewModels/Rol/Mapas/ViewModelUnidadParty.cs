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
                StringBuilder stringBuilder = new StringBuilder("../../../../Media/Imagenes/Posiciones/");

                switch (NumeroParty)
                {
                    case ENumeroParty.Party_0: stringBuilder.Append("Party_0");
                        break;
                    case ENumeroParty.Party_Saber: stringBuilder.Append("Party_Saber_Localization");
                        break;
                    case ENumeroParty.Party_Lancer: stringBuilder.Append("Party_Lancer_Localization");
                        break;
                    case ENumeroParty.Party_Archer: stringBuilder.Append("Party_Archer_Localization");
                        break;
                    case ENumeroParty.Party_Rider: stringBuilder.Append("Party_Rider_Localization");
                        break;
                    case ENumeroParty.Party_Berserker: stringBuilder.Append("Party_Berserker_Localization");
                        break;
                    case ENumeroParty.Party_Assassin: stringBuilder.Append("Party_Assassin_Localization");
                        break;
                    case ENumeroParty.Party_Caster: stringBuilder.Append("Party_Caster_Localization");
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
            ComandoEliminarUnidad     = new Comando(EliminarUnidad);
            ComandoUnidadSeleccionada = new Comando(AgregarUnidadSeleccionada);
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

            SistemaPrincipal.GuardarDatosAsync();
        }

        #endregion

    }
}
