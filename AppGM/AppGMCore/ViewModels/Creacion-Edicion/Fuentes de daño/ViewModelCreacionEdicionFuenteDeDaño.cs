using System;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel para la creacion/edicion de un <see cref="ModeloFuenteDeDaño"/>
	/// </summary>
	public sealed class ViewModelCreacionEdicionFuenteDeDaño : ViewModelCreacionEdicionDeModelo<ModeloFuenteDeDaño, Controlador<ModeloFuenteDeDaño>, ViewModelCreacionEdicionFuenteDeDaño>
	{
		#region Metodos

		/// <summary>
		/// Nombre de la fuente de daño
		/// </summary>
		public string Nombre
		{
			get => ModeloCreado.NombreFuente;
			set => ModeloCreado.NombreFuente = value;
		}

		/// <summary>
		/// Tipos de daños abarcados por esta fuente
		/// </summary>
		public ViewModelMultiselectComboBox<ETipoDeDaño> ViewModelTiposDeDañoAbarcados { get; set; } 

		#endregion

		#region Constructor

		public ViewModelCreacionEdicionFuenteDeDaño(
			Action<ViewModelCreacionEdicionFuenteDeDaño> _accionSalir,
			ModeloFuenteDeDaño _modeloEditar)

			: base(_accionSalir, null, true, true)
		{
			ModeloSiendoEditado = _modeloEditar;

			CrearComandoEliminar();

			ViewModelTiposDeDañoAbarcados = new ViewModelMultiselectComboBox<ETipoDeDaño>(EnumHelpers.TiposDeDañoDisponibles.Select(t => new ViewModelMultiselectComboBoxItem<ETipoDeDaño>(t, t.ToString(), ViewModelTiposDeDañoAbarcados, EstaEditando && ModeloSiendoEditado.TiposDeDaño.HasFlag(t))).ToList());

			if(EstaEditando)
				ViewModelTiposDeDañoAbarcados.ModificarEstadoSeleccionItem(ModeloSiendoEditado.TiposDeDaño, true);

			ViewModelTiposDeDañoAbarcados.OnEstadoSeleccionItemCambio += item =>
			{
				if (item.EstaSeleccionado)
					ModeloCreado.TiposDeDaño |= item.Valor;
				else
					ModeloCreado.TiposDeDaño ^= item.Valor;

				DispararPropertyChanged(string.Empty);
			};
		} 

		#endregion

		#region Metodos

		public override ModeloFuenteDeDaño CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			return ModeloCreado;
		}

		public override Controlador<ModeloFuenteDeDaño> CrearControlador()
		{
			SistemaPrincipal.LoggerGlobal.Log($"{nameof(ModeloFuenteDeDaño)} no tiene un controlador", ESeveridad.Error);

			return null;
		}

		public override void ActualizarValidez()
		{
			EsValido = false;

			if (Nombre.IsNullOrWhiteSpace())
				return;

			if (ViewModelTiposDeDañoAbarcados.ItemsSeleccionados.Count == 0)
				return;

			EsValido = true;
		} 

		#endregion
	}
}