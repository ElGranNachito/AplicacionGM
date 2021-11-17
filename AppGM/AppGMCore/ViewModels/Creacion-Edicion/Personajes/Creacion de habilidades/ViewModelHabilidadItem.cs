using System;
using System.Collections.ObjectModel;
using System.IO;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una habilidad en una lista
    /// </summary>
    public class ViewModelHabilidadItem : ViewModelItemListaControlador<ViewModelHabilidadItem, ControladorHabilidad>
    {
        #region Propiedades

        public bool CuestaMana => ControladorGenerico.CostoMana != 0;
        public bool EsPerk     => ControladorGenerico.TipoHabilidad == ETipoHabilidad.Perk;
        public bool EsSkill    => ControladorGenerico.TipoHabilidad == ETipoHabilidad.Skill;
        public bool EsHechizo    => ControladorGenerico.TipoHabilidad == ETipoHabilidad.Hechizo;
        public bool EsNP       => ControladorGenerico.TipoHabilidad == ETipoHabilidad.NoblePhantasm;

        #endregion

        #region Constructor

        public ViewModelHabilidadItem(ControladorHabilidad _habilidad)
	        :base(_habilidad)
        {
	        Imagen = Path.Combine(
							Path.Combine(
								SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "Habilidades" + Path.DirectorySeparatorChar),
								ControladorGenerico.TipoHabilidad + ".png");
        }

		#endregion

		#region Metodos

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>
			{
				//Nombre de la habilidad
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = ControladorGenerico.Nombre + " - " + (EsHechizo ? "Lv." : "Rango:") + (EsHechizo ? (ControladorGenerico.modelo as ModeloMagia)?.Nivel.ToString() : ControladorGenerico.Rango.ToString())
				},

				//Tipo de la habilidad
				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo Habilidad",
					Valor = ControladorGenerico.TipoHabilidad.ToString()
				}
			};
        }

		protected override void ActualizarGruposDeBotones()
		{
			Action accionEditar = async () =>
			{
				var vmActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCrearHabilidad(
					vm =>
					{
						SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = vmActual;
					}, ControladorGenerico.modelo.Dueño, ControladorGenerico).Inicializar();
			};

			CrearBotonesParaEditarYEliminar(accionEditar, ()=>{});
		}

		#endregion
	}
}