using System.IO;

namespace AppGM.Core
{
    public class ViewModelHabilidadItem : BaseViewModel
    {
        #region Propiedades

        public ModeloHabilidad Habilidad { get; set; }

        public bool CuestaMana => Habilidad.CostoDeMana != 0;
        public bool EsPerk     => Habilidad.TipoDeHabilidad == ETipoHabilidad.Perk;
        public bool EsSkill    => Habilidad.TipoDeHabilidad == ETipoHabilidad.Skill;
        public bool EsMagia    => Habilidad.TipoDeHabilidad == ETipoHabilidad.Magia;
        public bool EsNP       => Habilidad.TipoDeHabilidad == ETipoHabilidad.NoblePhantasm;

        /// <summary>
        /// Consiste del nombre y el nivel o rango
        /// </summary>
        public string TituloHabilidad     => Habilidad.Nombre + $".{(EsMagia ? (Habilidad as ModeloMagia)?.Nivel.ToString() : Habilidad.Rango.ToString())}";

        public string PathImagenHabilidad => Path.Combine(Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "Habilidades" + SistemaPrincipal.ControladorDeArchivos.CaracterSeparadorDeCarpetas), Habilidad.TipoDeHabilidad + ".png");

        #endregion

        #region Constructor

        public ViewModelHabilidadItem(ModeloHabilidad _habilidad)
        {
            Habilidad = _habilidad;
        } 

        #endregion
    }
}