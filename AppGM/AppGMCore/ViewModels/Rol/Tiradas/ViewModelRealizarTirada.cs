using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppGM.Core
{
	public class ViewModelRealizarTirada : ViewModelConResultado<ViewModelRealizarTirada>
	{
		public enum EImagenCroto
		{
			ImagenCrotoNormal,
			ImagenCrotoArmado
		}

		public enum EMarcoGlobo
		{
			MarcoTiradaExistente,
			MarcoTiradaNueva
		}

		/// <summary>
		/// Numero de tiradas a realizar
		/// </summary>
		public string NumeroDeTiradas
		{
			get => PresetTirada.NumeroDeTiradas;
			set => PresetTirada.NumeroDeTiradas = value;
		}

		/// <summary>
		/// Tirada que realizar (en caso de que no sea una tirada preexistente)
		/// </summary>
		public string Tirada { get; set; }

		/// <summary>
		/// Modificador que aplicar al resultado
		/// </summary>
		public string Modificador
		{
			get => PresetTirada.Modificador.ToString();
			set => PresetTirada.Modificador = int.Parse(value);
		}

		/// <summary>
		/// Valor de la variable extra
		/// </summary>
		public string VariableExtra
		{
			get => PresetTirada.VariableExtra;
			set => PresetTirada.VariableExtra = value;
		}

		public string TextoTipoTirada { get; set; }

		/// <summary>
		/// Multiplicador de especialidad
		/// </summary>
		public string MultiplicadorEspecialidad
		{
			get => PresetTirada.MultiplicadorDeEspecialidad.ToString();
			set => PresetTirada.MultiplicadorDeEspecialidad = int.Parse(value);
		}

		/// <summary>
		/// Texto de la burbuja de texto del croto	
		/// </summary>
		public string TextoCroto { get; set; }

		/// <summary>
		/// Ruta completa a la imagen actual del croto
		/// </summary>
		public string RutaCompletaImagenCroto { get; set; }

		/// <summary>
		/// Ruta completa a la imagen del marco
		/// </summary>
		public string RutaCompletaImagenMarco { get; set; }

		/// <summary>
		/// Resultado de la ultima tirada
		/// </summary>
		public string ResultadoTirada { get; set; }

		/// <summary>
		/// Resultado detallado de la ultima tirada
		/// </summary>
		public string ResultadoDetalladoTirada { get; set; }

		/// <summary>
		/// Modificador de la stat actualmente seleccionada
		/// </summary>
		public string ModStat { get; set; }

		/// <summary>
		/// Ruta completa a la imagen del tipo de tirada actualmente seleccionado
		/// </summary>
		public string RutaCompletaImagenTipoTirada => ObtenerPathImagenTipoTirada(ViewModelComboBoxTipoTirada.Valor);

		/// <summary>
		/// Indica si se debe utilizar el multiplicador del punto vital
		/// </summary>
		public bool UtilizaMultiplicadorDelPuntoVital { get; set; }

		public bool EsTiradaPreexistente { get; private set; }

		/// <summary>
		/// Indica si esta es una tirada de daño
		/// </summary>
		public bool EsTiradaDeDaño => ObtenerEsTiradaDeTipo(ETipoTirada.Daño);

		/// <summary>
		/// Indica si esta es una tirada personalizada
		/// </summary>
		public bool EsTiradaPersonalizada => ObtenerEsTiradaDeTipo(ETipoTirada.Personalizada);

		/// <summary>
		/// Indica si esta es una tirada de stat
		/// </summary>
		public bool EsTiradaStat => ObtenerEsTiradaDeTipo(ETipoTirada.Stat);

		/// <summary>
		/// Viewmodel del combobox para seleccionar la stat de la tirada
		/// </summary>
		public ViewModelComboBox<EStat> ViewModelComboBoxStat { get; set; }

		/// <summary>
		/// Viewmodel del combobox para seleccionar la mano utilizada
		/// </summary>
		public ViewModelComboBox<EManoUtilizada> ViewModelComboBoxManoUtilizada { get; set; }

		/// <summary>
		/// Viewmodel del combobox para seleccionar el tipo de la tirada
		/// </summary>
		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada {get; set; }

		/// <summary>
		/// Viewmodel del combobox para seleccionar el preset
		/// </summary>
		public ViewModelComboBox<ModeloPresetTirada> ViewModelComboBoxPresetsDisponibles { get; set; }

		/// <summary>
		/// Viewmodel del combobox para seleccionar los tipos de daño aplicados por la tirada
		/// </summary>
		public ViewModelMultiselectComboBox<ETipoDeDaño> ViewModelMultiselectComboBoxTipoDeDaño { get; set; }

		/// <summary>
		/// Viewmodel para la seleccion del controlador de la tirada
		/// </summary>
		public ViewModelSeleccionDeControlador<ControladorTiradaBase> ViewModelSeleccionTirada { get; set; }

		/// <summary>
		/// Viewmodel para la seleccion del controlador del personaje que realiza la tirada
		/// </summary>
		public ViewModelSeleccionDeControlador<ControladorPersonaje> ViewModelSeleccionUsuario { get; set; }

		/// <summary>
		/// Viewmodel para la seleccion del objetivo de la tirada
		/// </summary>
		public ViewModelSeleccionDeControlador<ControladorBase> ViewModelSeleccionObjetivo { get; set; }

		/// <summary>
		/// Viewmodel para la seleccion del contenedor de la tirada
		/// </summary>
		public ViewModelSeleccionDeControlador<ControladorBase> ViewModelSeleccionContenedor { get; set; }

		/// <summary>
		/// Viewmodel para la vista en arbol del inventario del objetivo
		/// </summary>
		public ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot> ViewModelInventarioObjetivo { get; set; }

		public ModeloPresetTirada PresetTirada { get; set; } = new ModeloPresetTirada();

		public ControladorTiradaBase ControladorTirada { get; set; }

		public ViewModelItemListaBase UsuarioSeleccionado => ViewModelSeleccionUsuario.ItemSeleccionado;

		public ICommand ComandoRealizarTirada { get; private set; }

		public ICommand ComandoAplicarDaño { get; private set; }

		public ICommand ComandoCrearPreset { get; private set; }

		public ICommand ComandoSalir { get; private set; }

		public ICommand ComandoToggleEsTiradaExistente { get; private set; }

		public ViewModelRealizarTirada(ControladorPersonaje usuario, ControladorHabilidad habilidadContenedoraDeTirada, ControladorItem itemContenedorDeTirada)
		{
			ActualizarCroto(EImagenCroto.ImagenCrotoNormal);
			ActualizarMarco(EMarcoGlobo.MarcoTiradaNueva);

			//Seleccion stat
			ViewModelComboBoxStat = new ViewModelComboBox<EStat>(EnumHelpers.StatsDisponibles);

			ViewModelComboBoxStat.OnValorSeleccionadoCambio += StatSeleccionadaCambioHandler;

			//Mano utilizada
			ViewModelComboBoxManoUtilizada = new ViewModelComboBox<EManoUtilizada>(Enum.GetValues<EManoUtilizada>().ToList());

			ViewModelComboBoxManoUtilizada.OnValorSeleccionadoCambio += ManoSeleccionadaCambioHandler;

			//Tipo tirada
			ViewModelComboBoxTipoTirada = new ViewModelComboBox<ETipoTirada>(EnumHelpers.TiposDeTiradasDisponibles);

			ViewModelComboBoxTipoTirada.OnValorSeleccionadoCambio += TipoDeTiradaCambioHandler;

			ViewModelComboBoxTipoTirada.SeleccionarValor(ETipoTirada.Stat);

			ViewModelMultiselectComboBoxTipoDeDaño = new ViewModelMultiselectComboBox<ETipoDeDaño>(EnumHelpers
				.TiposDeDañoDisponibles.Select(t => new ViewModelMultiselectComboBoxItem<ETipoDeDaño>(t, t.ToString(), ViewModelMultiselectComboBoxTipoDeDaño)).ToList());

			//Seleccion usuario
			ViewModelSeleccionUsuario = new ViewModelSeleccionDeControlador<ControladorPersonaje>(SistemaPrincipal.DatosRolSeleccionado.Personajes);

			ViewModelSeleccionUsuario.OnControladorSeleccionado += UsuarioSeleccionadoHandler;

			ViewModelSeleccionUsuario.SeleccionarControlador(usuario);

			//Seleccion contenedor
			ViewModelSeleccionContenedor = new ViewModelSeleccionDeControlador<ControladorBase>(new List<ControladorBase>());

			ViewModelSeleccionContenedor.SeleccionarControlador(habilidadContenedoraDeTirada != null ? habilidadContenedoraDeTirada : itemContenedorDeTirada);

			ViewModelSeleccionContenedor.OnControladorSeleccionado += ContenedorTiradaCambioHandler;

			//Seleccion tirada
			ViewModelSeleccionTirada = new ViewModelSeleccionDeControlador<ControladorTiradaBase>(
				new List<ControladorTiradaBase>
				{
					new ControladorTiradaDaño(new ModeloTiradaDeDaño
					{
						Nombre = "Tirada habilidad piola",
						Descripcion = "Mete mucho daño",
						Tirada = "6d21+9",
						EsValido = true
					})
				});

			ViewModelSeleccionTirada.OnControladorSeleccionado += TiradaSeleccionadaCambioHandler;

			ComandoRealizarTirada = new Comando(() =>
			{
				var resultadoTirada = ParserTiradas.RealizarTiradaStat(new ArgumentosTirada
				{
					multiplicadorEspecialidad = int.Parse(MultiplicadorEspecialidad),
					modificador = int.Parse(Modificador) + int.Parse(ModStat),
					stat = ViewModelComboBoxStat.Valor
				});

				ResultadoTirada          = resultadoTirada.resultado.ToString();
				ResultadoDetalladoTirada = resultadoTirada.resultadoDetallado;
			});

			ComandoToggleEsTiradaExistente = new Comando(() =>
			{
				EsTiradaPreexistente = !EsTiradaPreexistente;
			});

			ComandoSalir = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Aceptar;
			});
		}

		public void ActualizarCroto(EImagenCroto nuevaImagen)
		{
			RutaCompletaImagenCroto = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesTiradas, nuevaImagen == EImagenCroto.ImagenCrotoNormal ? "CrotoPromedio.png" : "CrotoArmado.png");
		}

		public void ActualizarMarco(EMarcoGlobo nuevoMarco)
		{
			RutaCompletaImagenMarco = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesTiradas, nuevoMarco == EMarcoGlobo.MarcoTiradaExistente ? "Marco1.png" : "Marco2.png");
		}

		private void UsuarioSeleccionadoHandler(ViewModelItemListaBase itemLista, ControladorPersonaje nuevoUsuario)
		{
			DispararPropertyChanged(nameof(UsuarioSeleccionado));

			ViewModelSeleccionContenedor.ActualizarControladorDisponibles(new List<ControladorBase>
			{
				new ControladorHabilidad(new ModeloMagia()
				{
					Nombre = "Explosion",
					Descripcion = "Piola descripcion",
					CostoDeMana = 120,
					TipoDeHabilidad = ETipoHabilidad.Hechizo,
					Nivel = 2
				}),

				new ControladorHabilidad(new ModeloHabilidad
				{
					Nombre = "Baile cautivador",
					Descripcion = "Piola descripcion",
					CostoDeMana = 0,
					TipoDeHabilidad = ETipoHabilidad.Skill,
					Rango = ERango.E
				}),
			});

			if (EsTiradaDeDaño)
			{
				ViewModelSeleccionObjetivo = new ViewModelSeleccionDeControlador<ControladorBase>(
					new List<ControladorBase>(
						SistemaPrincipal.DatosRolSeleccionado.Personajes.Where(p => p != nuevoUsuario)));

				ViewModelSeleccionObjetivo.OnControladorSeleccionado += ObjetivoSeleccionadoCambioHandler;
			}
		}

		private void ContenedorTiradaCambioHandler(ViewModelItemListaBase itemSeleccionado, ControladorBase nuevoContenedor)
		{
			ViewModelSeleccionTirada.ActualizarControladorDisponibles(ViewModelSeleccionContenedor?.ControladorSeleccionado.ObtenerTiradas()
				.Select(t => (ControladorTiradaBase)t).ToList());
		}

		private void TiradaSeleccionadaCambioHandler(ViewModelItemListaBase itemSeleccionado, ControladorTiradaBase nuevaTirada)
		{
			PresetTirada.TiradaALaQuePertenece = nuevaTirada.modelo;
		}

		private void ObjetivoSeleccionadoCambioHandler(ViewModelItemListaBase itemLista, ControladorBase nuevoObjetivo)
		{
			if (nuevoObjetivo is ControladorPersonaje pj)
			{
				ViewModelInventarioObjetivo = new ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot>(null);

				var itemsInventario = pj.modelo.SlotsBase.Select(s =>
				{
					var controladorSlot = SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(s, true);

					return new ViewModelElementoArbolItemInventario(ViewModelInventarioObjetivo, null, controladorSlot,
						ETipoItem.TODOS);
				}).ToList();

				ViewModelInventarioObjetivo.Hijos.AddRange(itemsInventario);
			}
			else
			{
				ViewModelInventarioObjetivo = null;
			}
		}

		private void TipoDeTiradaCambioHandler(ViewModelItemComboBoxBase<ETipoTirada> valoranterior, ViewModelItemComboBoxBase<ETipoTirada> valoractual)
		{
			if (valoractual.valor != ETipoTirada.Daño)
			{
				ViewModelSeleccionObjetivo  = null;
				ViewModelInventarioObjetivo = null;
			}

			switch (valoractual.valor)
			{
				case ETipoTirada.Daño:
				{
					ViewModelSeleccionObjetivo = new ViewModelSeleccionDeControlador<ControladorBase>(
						new List<ControladorBase>(
							SistemaPrincipal.DatosRolSeleccionado.Personajes.Where(p => p != UsuarioSeleccionado?.Controlador)));

					ActualizarCroto(EImagenCroto.ImagenCrotoArmado);

					break;
				}
				case ETipoTirada.Personalizada:
				{
					ActualizarCroto(EImagenCroto.ImagenCrotoNormal);

					break;
				}
				case ETipoTirada.Stat:
				{
					ActualizarCroto(EImagenCroto.ImagenCrotoNormal);

					break;
				}
			}

			TextoCroto = valoractual.Texto;

			DispararPropertyChanged(nameof(EsTiradaStat));
			DispararPropertyChanged(nameof(EsTiradaDeDaño));
			DispararPropertyChanged(nameof(EsTiradaPersonalizada));
			DispararPropertyChanged(nameof(RutaCompletaImagenTipoTirada));
		}

		private void StatSeleccionadaCambioHandler(ViewModelItemComboBoxBase<EStat> valoranterior, ViewModelItemComboBoxBase<EStat> valoractual)
		{
			ModStat = Helpers.Juego.ObtenerModificadorStat(((ModeloPersonaje) UsuarioSeleccionado.Controlador.Modelo).ObtenerValorStat(valoractual.valor)).ToString();

			PresetTirada.Stat = valoractual.valor;
		}

		private void ManoSeleccionadaCambioHandler(ViewModelItemComboBoxBase<EManoUtilizada> valoranterior, ViewModelItemComboBoxBase<EManoUtilizada> valoractual)
		{
			PresetTirada.ManoUtilizada = valoractual.valor;
		}

		private bool ObtenerEsTiradaDeTipo(ETipoTirada tipoTirada)
		{
			return ViewModelComboBoxTipoTirada.ValorSeleccionado?.valor == tipoTirada || ViewModelSeleccionTirada.ControladorSeleccionado?.modelo?.TipoTirada == tipoTirada;
		}

		private string ObtenerPathImagenTipoTirada(ETipoTirada tipoTirada)
		{
			switch (tipoTirada)
			{
				case ETipoTirada.Daño:
					return Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesTiradas, "Iconos/TiradaDaño.png");
					break;
				case ETipoTirada.Personalizada:
					return Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesTiradas, "Iconos/TiradaPersonalizada.png");
					break;
				case ETipoTirada.Stat:
					return Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenesTiradas, "Iconos/TiradaStat.png");
					break;
			}

			return string.Empty;
		}
	}
}