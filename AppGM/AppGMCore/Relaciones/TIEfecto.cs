using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una relacion de un <see cref="ModeloEfecto"/> con el <see cref="ModeloModificadorDeStatBase"/> que tenga
    /// TODO: Evaluar eliminar esto a la mierda
    /// </summary>
    public class TIEfectoModificadorDeStatBase : ModeloBaseSK
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }

        [ForeignKey(nameof(Modificador))]
        public int IdModificadorDeStat { get; set; }
        public ModeloModificadorDeStatBase Modificador { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloBase"/>
    /// </summary>
    public class TIEfectoSiendoAplicado : ModeloBaseSK
    {
	    [ForeignKey(nameof(EfectoAplicandose))] 
	    public int IdEfectoSiendoAplicado { get; set; }

	    public ModeloEfectoSiendoAplicado EfectoAplicandose { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloEfecto"/>
    /// </summary>
    public class TIEfectoSiendoAplicadoEfecto : TIEfectoSiendoAplicado
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public ModeloEfecto Efecto { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloPersonaje"/>
    /// </summary>
    public class TIEfectoSiendoAplicadoPersonaje : TIEfectoSiendoAplicado
    {
        [ForeignKey(nameof(Personaje))]
        public int IdPersonaje { get; set; }
        public ModeloPersonaje Personaje { get; set; }
    }
}