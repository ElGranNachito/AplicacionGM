using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a un control para el ingreso de los datos de un consumible
	/// </summary>
	public sealed class ViewModelIngresoDatosConsumible : ViewModelCreacionEdicionDeModelo<ModeloDatosConsumible, Controlador<ModeloDatosConsumible>, ViewModelIngresoDatosConsumible>
	{
		#region Campos & Propiedades
		/// <summary>
		/// Viewmodel de creacion/edicion item que contiene a este objeto
		/// </summary>
		public readonly ViewModelCreacionEdicionItem viewModelCreacionEdicionItemContenedor;

		/// <summary>
		/// Usos totales del item
		/// </summary>
		public string UsosTotales
		{
			get => ModeloCreado.UsosTotales.ToString();
			set => ModeloCreado.UsosTotales = value.ParseToIntIfValid();
		}

		/// <summary>
		/// Usos restantes del item
		/// </summary>
		public string UsosRestantes
		{
			get => ModeloCreado.UsosRestantes.ToString();
			set => ModeloCreado.UsosRestantes = value.ParseToIntIfValid();
		} 
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmContenedor">Viewmodel de creacion/edicion item que contiene a este objeto</param>
		/// <param name="_modeloEditar">Modelo que editaremos</param>
		public ViewModelIngresoDatosConsumible(ModeloDatosConsumible _modeloEditar, ViewModelCreacionEdicionItem _vmContenedor)

			: base(null, true, false)
		{
			viewModelCreacionEdicionItemContenedor = _vmContenedor;
			ModeloSiendoEditado = _modeloEditar;

			ModeloCreado = ModeloSiendoEditado ?? new ModeloDatosConsumible();
		} 
		#endregion

		#region Metodos
		public override void ActualizarValidez()
		{
			EsValido = false;

			//Nos aseguramos de que tenga al menos un uso total
			if (ModeloCreado.UsosTotales <= 0)
				return;

			//Nos aseguramos de que el numero de usos restantes no sea negativo
			if (ModeloCreado.UsosRestantes < 0)
				return;

			EsValido = true;
		}

		public override ModeloDatosConsumible CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			return ModeloCreado;
		}

		public override Controlador<ModeloDatosConsumible> CrearControlador()
		{
			SistemaPrincipal.LoggerGlobal.Log($"{nameof(ModeloDatosConsumible)} no requiere un controlador", ESeveridad.Error);

			return null;
		} 
		#endregion
	}
}
