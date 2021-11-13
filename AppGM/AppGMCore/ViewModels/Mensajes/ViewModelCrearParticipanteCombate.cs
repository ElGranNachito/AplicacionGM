﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;


namespace AppGM.Core
{
    /// <summary>
    /// VM para la creacion de la representacion de un participante en un <see cref="ViewModelCombate"/>.
    /// </summary>
    public class ViewModelCrearParticipanteCombate : ViewModelConResultado<ViewModelCrearParticipanteCombate>
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// VM del combate en el que se agregara el participante
        /// </summary>
        private ViewModelCombate combate;

        /// <summary>
        /// VM de participante resultante de la creacion
        /// </summary>
        public ViewModelParticipante vmResultado;


        // Propiedades ---


        /// <summary>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Finalizar'
        /// </summary>
        public ICommand ComandoFinalizar { get; set; }

        /// <summary>
        /// Valores del enum <see cref="ETipoUnidad"/>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Finalizar'
        /// </summary>
        /// TODO: Remover
        public List<ETipoPersonaje> TiposPersonajes => Enum.GetValues(typeof(ETipoPersonaje)).Cast<ETipoPersonaje>().ToList();

        /// <summary>
        /// Valores del enum <see cref="EClaseServant"/>
        /// Indica si la creacion puede finalizarse.
        /// </summary>
        /// TODO: Remover
        public List<EClaseServant> ClasesServants => Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();

        /// <summary>
        /// Clase de servant seleccionada.
        /// Solamente aplica si el <see cref="TipoPersonajeSeleccionado"/> no es <see cref="ETipoPersonaje.NPC"/>
        /// </summary>
        public EClaseServant ClaseSeleccionada { get; set; }

        /// <summary>
        /// Tipo del personaje que sera agregado al combate
        /// </summary>
        public ETipoPersonaje TipoPersonajeSeleccionado { get; set; }

        /// <summary>
        /// Nombre del participante.
        /// </summary>
        public string Nombre { get; set; } = "Participante X";

        /// <summary>
        /// Nombre del personaje actualmente seleccionado
        /// </summary>
        public string PersonajeSeleccionado { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad inicial de acciones que puede realizar por turno.
        /// </summary>
        public int CantidadInicialDeAcciones { get; set; } = 2;

        /// <summary>
        /// Indica si es necesario seleccionar una <see cref="EClaseServant"/>
        /// </summary>
        public bool DebeSeleccionarClaseServant   => TipoPersonajeSeleccionado != ETipoPersonaje.NPC;

        /// <summary>
        /// Indica si la creacion puede finalizarse.
        /// </summary>
        public bool PuedeFinalizarCreacion
        {
            get
            {
                if (Nombre == string.Empty)
                    return false;

                if (DebeSeleccionarClaseServant && ClaseSeleccionada == EClaseServant.NINGUNO)
                    return false;

                if (PersonajeSeleccionado == string.Empty)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Lista de personajes disponibles para agregar.
        /// </summary>
        public List<string> PersonajesDisponibles
        {
            get
            {
                switch (TipoPersonajeSeleccionado)
                {
                    case ETipoPersonaje.Servant:
                        return SistemaPrincipal.DatosRolSeleccionado.Servants.Select(s => s.ToString()).ToList();
                    case ETipoPersonaje.Master:
                        return SistemaPrincipal.DatosRolSeleccionado.Masters.Select(m => m.ToString()).ToList();
                    case ETipoPersonaje.Invocacion:
                        return SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Select(i => i.ToString()).ToList();
                    case ETipoPersonaje.NPC:
                        return SistemaPrincipal.DatosRolSeleccionado.NPCs.Select(i => i.ToString()).ToList();
                    default:
                        return new List<string>(0);
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_combate">Combate al que se agregara al participante</param>
        public ViewModelCrearParticipanteCombate(ViewModelCombate _combate)
        {
            combate = _combate;

            ComandoFinalizar = new Comando(GenerarViewModel);

            PropertyChanged += (obj, e) =>
            {
                //Si el tipo de personaje que estamos creando cambio...
                if (e.PropertyName == nameof(TipoPersonajeSeleccionado))
                {
                    //Disparamos property changed en estas propiedades para que se actualicen los campos a completar en la UI
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarClaseServant)));
                }
                //Si la propiedad no es el tipo sileccionado ni si podemos finalizar la creacion...
                else if(e.PropertyName != nameof(PuedeFinalizarCreacion))
                    //Disparamos el evento property changed en PuedeFinalizarCreacion
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizarCreacion)));
            };
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Crea el VM
        /// </summary>
        private void GenerarViewModel()
        {
            ModeloParticipante modeloParticipante = null;

            switch (TipoPersonajeSeleccionado)
            {
                case ETipoPersonaje.Master:
                {
                    modeloParticipante = new ModeloParticipante
                    {
                        EsSuTurno = false,
                        TotalAccionesPorTurno = CantidadInicialDeAcciones
                    };

                    modeloParticipante.Personaje = SistemaPrincipal.DatosRolSeleccionado.Masters.Find(m => m.ToString() == PersonajeSeleccionado).modelo;
                    break;
                }
                case ETipoPersonaje.Servant:
                {
                    modeloParticipante = new ModeloParticipante
                    {
                        EsSuTurno = false,
                        TotalAccionesPorTurno = CantidadInicialDeAcciones
                    };

                    modeloParticipante.Personaje = SistemaPrincipal.DatosRolSeleccionado.Servants.Find(s => s.ToString() == PersonajeSeleccionado).modelo;
                    break;
                }
                case ETipoPersonaje.Invocacion:
                {
                    modeloParticipante = new ModeloParticipante
                    {
                        EsSuTurno = false,
                        TotalAccionesPorTurno = CantidadInicialDeAcciones
                    };
                    modeloParticipante.Personaje = SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Find(s => s.ToString() == PersonajeSeleccionado).modelo;
                    break;
                }
                case ETipoPersonaje.NPC:
                {
                    modeloParticipante = new ModeloParticipante
                    {
                        EsSuTurno = false,
                        TotalAccionesPorTurno = CantidadInicialDeAcciones
                    };

                    modeloParticipante.Personaje = SistemaPrincipal.DatosRolSeleccionado.NPCs.Find(i => i.ToString() == PersonajeSeleccionado).modelo;
                    break;
                }
            }

            combate.administradorDeCombate.AgregarParticipante(modeloParticipante);

            ControladorParticipante controlador = new ControladorParticipante(modeloParticipante);
            
            vmResultado = new ViewModelParticipante(controlador, combate);

            SistemaPrincipal.GuardarModelo(modeloParticipante);

            SistemaPrincipal.GuardarDatosAsync();

            Resultado = EResultadoViewModel.Aceptar;
        }

        #endregion
    }
}
