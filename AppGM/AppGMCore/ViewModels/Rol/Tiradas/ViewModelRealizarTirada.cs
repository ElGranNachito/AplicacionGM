namespace AppGM.Core
{
	public class ViewModelRealizarTirada : ViewModelConResultado<ViewModelRealizarTirada>
	{
		public string NumeroDeTiradas { get; set; }

		public string Tirada { get; set; }

		public string Modificador { get; set; }

		public string VariableExtra { get; set; }

		public string TextoTipoTirada { get; set; }

		public bool UtilizaMultiplicadorDelPuntoVital { get; set; }

		public ViewModelComboBox<EStat> ViewModelComboBoxStat { get; set; }

		public ViewModelComboBox<EManoUtilizada> ViewModelComboBoxManoUtilizada { get; set; }

		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada {get; set; }

		public ViewModelMultiselectComboBox<ETipoDeDaño> ViewModelMultiselectComboBoxTipoDeDaño { get; set; }

		public ViewModelMultiselectComboBox<ModeloFuenteDeDaño> ViewModelMultiselectComboBoxFuentesDeDañoAbarcadas { get; set; }

		public ViewModelSeleccionDeControlador ViewModelSeleccionTirada { get; set; }

		public ViewModelSeleccionDeControlador ViewModelSeleccionUsuario { get; set; }

		public ViewModelSeleccionDeControlador ViewModelSeleccionObjetivo { get; set; }

		public ViewModelSeleccionDeControlador ViewModelSeleccionHabilidad { get; set; }

		public ViewModelSeleccionDeControlador ViewModelSeleccionItem { get; set; }

		public ModeloPresetTirada PresetTirada { get; set; } = new ModeloPresetTirada();

		public ViewModelBoton ViewModelBotonRealizarTirada { get; set; }

		public ViewModelBoton ViewModelBotonAplicarDaño { get; set; }

		public ViewModelBoton ViewModelBotonSalir { get; set; }

		public ViewModelBoton ViewModelBotonAñadirPreset { get; set; }

		public ViewModelRealizarTirada(ControladorPersonaje usuario, ControladorHabilidad habilidadContenedoraDeTirada, ControladorItem itemContenedorDeTirada)
		{
			
		}
	}
}
