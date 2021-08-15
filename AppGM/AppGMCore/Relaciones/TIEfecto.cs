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
        public virtual ModeloEfecto Efecto { get; set; }

        [ForeignKey(nameof(Modificador))]
        public int IdModificadorDeStat { get; set; }
        public virtual ModeloModificadorDeStatBase Modificador { get; set; }
    }

    /// <summary>
    /// Representa una realcion entre un <see cref="ModeloEfecto"/> y un <see cref="ModeloFuncion"/>
    /// </summary>
    public class TIEfectoFuncion : ModeloBaseSK
    {
	    [ForeignKey(nameof(Efecto))]
	    public int IdEfecto { get; set; }
	    public virtual ModeloEfecto Efecto { get; set; }

        public int IdFuncion { get; set; }
        public virtual ModeloFuncion Funcion { get; set; }

        public ETipoFuncionEfecto TipoFuncion { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloBase"/>
    /// </summary>
    public class TIEfectoSiendoAplicado : ModeloBaseSK
    {
	    [ForeignKey(nameof(EfectoAplicandose))] 
	    public int IdEfectoSiendoAplicado { get; set; }
        public virtual ModeloEfectoSiendoAplicado EfectoAplicandose { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloEfecto"/>
    /// </summary>
    public class TIEfectoSiendoAplicadoEfecto : TIEfectoSiendoAplicado
    {
        [ForeignKey(nameof(Efecto))]
        public int IdEfecto { get; set; }
        public virtual ModeloEfecto Efecto { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloPersonaje"/> como instigador
    /// </summary>
    public class TIEfectoSiendoAplicadoPersonajeInstigador : TIEfectoSiendoAplicado
    {
        [ForeignKey(nameof(PersonajeInstigador))]
        public int IdPersonajeInstigador { get; set; }
        public virtual ModeloPersonaje PersonajeInstigador { get; set; }
    }

    /// <summary>
    /// Relacion entre un <see cref="ModeloEfectoSiendoAplicado"/> y un <see cref="ModeloPersonaje"/> como objetivo
    /// </summary>
    public class TIEfectoSiendoAplicadoPersonajeObjetivo : TIEfectoSiendoAplicado
    {
        [ForeignKey(nameof(PersonajeObjetivo))]
        public int IdPersonajeObjetivo { get; set; }
        public virtual ModeloPersonaje PersonajeObjetivo { get; set; }
    }

    public class TIEfectoSiendoAplicadoFuncion : TIEfectoSiendoAplicado
    {
        [ForeignKey(nameof(ModeloFuncion))]
        public int IdFuncion { get; set; }
        public virtual ModeloFuncion ModeloFuncion { get; set; }

        public ETipoFuncionEfecto TipoFuncion { get; set; }
    }
}