﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloAdministradorDeCombate
    {
        //Id
        [Key]
        public int IdAdministradorDeCombate { get; set; }
        public int IndicePersonajeTurnoActual { get; set; }
        public uint TurnoActual { get; set; }

        public List<TIAdministradorDeCombateParticipante> Participantes { get; set; }
        public List<TIModeloAdministradorDeCombateMapa> Mapas { get; set; }
    }
}