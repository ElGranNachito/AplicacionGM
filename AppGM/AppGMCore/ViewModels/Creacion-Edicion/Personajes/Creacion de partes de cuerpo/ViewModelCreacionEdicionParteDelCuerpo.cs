using System;
using System.Windows.Input;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel utilizado para crear o editar un <see cref="ModeloParteDelCuerpo"/>
	/// </summary>
	public class ViewModelCreacionEdicionParteDelCuerpo : ViewModelCreacionEdicionDeModelo<ModeloParteDelCuerpo, ControladorParteDelCuerpo, ViewModelCreacionEdicionParteDelCuerpo>
	{
		#region Propiedades

		/// <summary>
		/// Nombre de esta parte del cuerpo
		/// </summary>
		public string Nombre
		{
			get => ModeloCreado.Nombre;
			set => ModeloCreado.Nombre = value;
		}

		/// <summary>
		/// Multiplicador de daño de esta parte del cuerpo
		/// </summary>
		public string Multiplicador
		{
			get => ModeloCreado.MultiplicadorDeEstaParte.ToString("##.###");
			set => ModeloCreado.MultiplicadorDeEstaParte = float.Parse(value);
		}

		/// <summary>
		/// Cantidad de slots que contiene esta parte del cuerpo
		/// </summary>
		public string CantidadDeSlots
		{
			get => ModeloCreado.Slots.Count.ToString();
			set
			{
				if (int.TryParse(value, out int nuevaCantidadDeSlots) && nuevaCantidadDeSlots >= 0)
				{
					SetCantidadDeSlots(nuevaCantidadDeSlots);
				}
				else
				{
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(value)} debe ser un int y mayor o igual a 0", ESeveridad.Error);
				}
			}
		}

		#endregion

		#region Contructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		/// <param name="_controladorParaEditar">Controlador del <see cref="ModeloParteDelCuerpo"/> que se editara</param>
		public ViewModelCreacionEdicionParteDelCuerpo(
			Action<ViewModelCreacionEdicionParteDelCuerpo> _accionSalir,
			ControladorParteDelCuerpo _controladorParaEditar)

			: base(_accionSalir, _controladorParaEditar, true, true)
		{
			CrearComandoEliminar();
		}

		#endregion

		#region Metodos

		public override ModeloParteDelCuerpo CrearModelo()
		{
			ActualizarValidez();

			return EsValido ? ModeloCreado : null;
		}

		public override ControladorParteDelCuerpo CrearControlador()
		{
			var modeloCreado = CrearModelo();

			if (modeloCreado == null)
				return null;

			return EstaEditando ? ControladorSiendoEditado : SistemaPrincipal.ObtenerControlador<ControladorParteDelCuerpo, ModeloParteDelCuerpo>(ModeloCreado, true);
		}

		protected override void ActualizarValidez()
		{
			if (ModeloCreado.Nombre.IsNullOrWhiteSpace())
			{
				EsValido = false;

				return;
			}

			if (ModeloCreado.MultiplicadorDeEstaParte <= 0)
			{
				EsValido = false;

				return;
			}

			EsValido = true;
		}

		/// <summary>
		/// Actualiza la cantidad de slots que contiene el <see cref="ModeloParteDelCuerpo"/> siendo creado o editado
		/// </summary>
		/// <param name="nuevaCantidad">Nueva cantidad de slots</param>
		private async void SetCantidadDeSlots(int nuevaCantidad)
		{
			//Obtenemos la diferencia con la cantidad anterior
			var diferenciaDeCantidad = nuevaCantidad - ModeloCreado.Slots.Count;

			//Si se añadieron slots...
			if (diferenciaDeCantidad > 0)
			{
				//Creamos los nuevos slots y los añadimos
				for (int i = 0; i < diferenciaDeCantidad; ++i)
				{
					var nuevoSlot = new ModeloSlot
					{
						NombreSlot = IModeloConSlots.ObtenerNombreParaNuevoSlot(ModeloCreado),
						EspacioTotal = Constantes.EspacioPorDefectoNuevoSlot,
						ParteDelCuerpoDueña = ModeloCreado
					};

					ModeloCreado.Slots.Add(nuevoSlot);
				}
			}
			//Si se quitaron...
			else if (diferenciaDeCantidad < 0)
			{
				//Obtenemos el valor absoluto de la diferencia
				diferenciaDeCantidad = Math.Abs(diferenciaDeCantidad);

				//Mostramos un mensaje de confirmacion y si el usuario acepta quitamos los slots del modelo
				var resultadoConfirmacion = await MensajeHelpers.MostrarMensajeConfirmacionAsync("¿Proseguir?", $"Si continua se eliminaran {diferenciaDeCantidad} slots y los items que contengan");

				if (resultadoConfirmacion != EResultadoViewModel.Aceptar)
					return;

				ModeloCreado.Slots.RemoveRange(ModeloCreado.Slots.Count - diferenciaDeCantidad, diferenciaDeCantidad);
			}
		}

		#endregion
	}
}