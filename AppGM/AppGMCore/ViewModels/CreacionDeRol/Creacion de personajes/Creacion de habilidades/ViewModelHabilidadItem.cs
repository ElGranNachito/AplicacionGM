using System.IO;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelHabilidadItem : ViewModel
    {
        #region Propiedades

        public ModeloHabilidad Habilidad { get; set; }

        public bool CuestaMana => Habilidad.CostoDeMana != 0;
        public bool EsPerk     => Habilidad.TipoDeHabilidad == ETipoHabilidad.Perk;
        public bool EsSkill    => Habilidad.TipoDeHabilidad == ETipoHabilidad.Skill;
        public bool EsMagia    => Habilidad.TipoDeHabilidad == ETipoHabilidad.Hechizo;
        public bool EsNP       => Habilidad.TipoDeHabilidad == ETipoHabilidad.NoblePhantasm;

        /// <summary>
        /// Consiste del nombre y el nivel o rango
        /// </summary>
        public string TituloHabilidad     => Habilidad.Nombre + $".{(EsMagia ? (Habilidad as ModeloMagia)?.Nivel.ToString() : Habilidad.Rango.ToString())}";

        public string PathImagenHabilidad => Path.Combine(Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "Habilidades" + Path.DirectorySeparatorChar), Habilidad.TipoDeHabilidad + ".png");


        public ICommand ComandoEditar { get; private set; }
        #endregion

        #region Constructor

        public ViewModelHabilidadItem(ModeloHabilidad _habilidad)
        {
            Habilidad = _habilidad;

            ComandoEditar = new Comando(() =>
            {
	            var vmActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

	            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCrearHabilidad(_habilidad.Dueño.Personaje,
		            () =>
		            {
			            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = vmActual;
		            }, Habilidad);
            });
        } 

        #endregion
    }
}