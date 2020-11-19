﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloAdministradorDeCombate : ModeloBase
    {
        public ControladorAdministradorDeCombate controladorAdministradorDeCombate;

        public int IndicePersonajeTurnoActual { get; set; }
        public uint TurnoActual { get; set; }
        public string Nombre { get; set; }

        public List<TIAdministradorDeCombateParticipante> Participantes { get; set; } = new List<TIAdministradorDeCombateParticipante>();
        public List<TIAdministradorDeCombateMapa> Mapas { get; set; }                 = new List<TIAdministradorDeCombateMapa>();
    }
}