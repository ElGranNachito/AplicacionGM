﻿using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloItem : ModeloUtilizable
    {
        public decimal StatsQueOcupa { get; set; }
    }

    public class ModeloConsumible : ModeloItem
    {
        public ControladorConsumible controladorConsumible;

        public ushort Usos { get; set; }
        public ushort UsosRestantes { get; set; }
    }

    public class ModeloArmasDistancia : ModeloConsumible, IInfligeDaño
    {
        public ControladorArmaDistancia controladorArmaDistancia;

        public TIArmasDistanciaTiradaDeDaño TiradaDeDaño { get; set; }

        public TIArmasDistanciaTiradaVariable TiradaRafaga { get; set; }

        public ETipoDeDaño TipoDeDañoQueInflige { get; set; }

        public List<TIArmasDistanciaEfecto> EfectoQueInflige { get; set; } = new List<TIArmasDistanciaEfecto>();
    }
}
