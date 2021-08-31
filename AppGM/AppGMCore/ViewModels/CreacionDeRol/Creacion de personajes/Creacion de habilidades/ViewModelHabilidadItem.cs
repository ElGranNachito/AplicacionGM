using System.Collections.ObjectModel;
using System.IO;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una habilidad en una lista
    /// </summary>
    public class ViewModelHabilidadItem : ViewModelItemLista
    {
        #region Propiedades

        public ModeloHabilidad Habilidad { get; set; }

        public bool CuestaMana => Habilidad.CostoDeMana != 0;
        public bool EsPerk     => Habilidad.TipoDeHabilidad == ETipoHabilidad.Perk;
        public bool EsSkill    => Habilidad.TipoDeHabilidad == ETipoHabilidad.Skill;
        public bool EsMagia    => Habilidad.TipoDeHabilidad == ETipoHabilidad.Hechizo;
        public bool EsNP       => Habilidad.TipoDeHabilidad == ETipoHabilidad.NoblePhantasm;

        #endregion

        #region Constructor

        public ViewModelHabilidadItem(ModeloHabilidad _habilidad)
        {
            Habilidad = _habilidad;

            PathImagen = Path.Combine(
							Path.Combine(
								SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "Habilidades" + Path.DirectorySeparatorChar), 
								Habilidad.TipoDeHabilidad + ".png");

            CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>
            {
                //Nombre de la habilidad
	            new ViewModelCaracteristicaItem
	            {
		            Titulo = "Nombre",
		            Valor = Habilidad.Nombre + $".{(EsMagia ? (Habilidad as ModeloMagia)?.Nivel.ToString() : Habilidad.Rango.ToString())}"
	            },

                //Tipo de la habilidad
                new ViewModelCaracteristicaItem
                {
                    Titulo = "Tipo Habilidad",
                    Valor = Habilidad.TipoDeHabilidad.ToString()
                }
            };

            ComandoBotonSuperior = new Comando(() =>
            {
	            var vmActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

	            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCrearHabilidad(_habilidad.Dueño.Personaje,
		            vm =>
		            {
			            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = vmActual;
		            }, Habilidad);
            });
        } 

        #endregion
    }
}