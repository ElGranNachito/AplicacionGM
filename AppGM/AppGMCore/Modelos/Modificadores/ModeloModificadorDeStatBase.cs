﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    public class ModeloModificadorDeStatBase : ModeloBase
    {
        public ControladorModificadorDeStat controladorModificadorDeStat;

        //Valor requerido para realizar tirada
        public int ValorRequeridoTirada { get; set; }

        //Valor de la stat en las tiradas
        public virtual TIModificadorDeStatBaseTiradaBase ValorTirada { get; set; }
    }

    public class ModeloModificadorDeStatPrimitivos : ModeloModificadorDeStatBase
    {
        //Stats afectadas por el modificador. Terner en cuenta los valores de EStat
        public int StatsQueAfecta { get; set; }

        //Valor aplicado a las stats
        public byte Valor { get; set; }
    }

    public class ModeloModificadorDeStatClase : ModeloModificadorDeStatBase
    {
        public ControladorModificadorDeStatClase controladorModificadorDeStatClase;

        //Nombre de la clase a modificar
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string NombreClase { get; set; }

        //Id de la clase
        public int IdObjeto { get; set; }
    }

    public class ModeloModificadorDeDefensa : ModeloModificadorDeStatBase
    {
        //Tener en cuenta los valores de ETipoDeDaño
        public int TiposDeDaño { get; set; }

        public byte ModificacionPorcentual { get; set; }
        public byte ModificacionFija { get; set; }
    }
}