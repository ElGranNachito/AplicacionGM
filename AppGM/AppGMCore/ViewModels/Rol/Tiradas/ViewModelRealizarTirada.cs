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

		public string NumeroDeTiradas { get; set; }

		public string Tirada { get; set; }

		public string Modificador { get; set; }

		public string VariableExtra { get; set; }

		public string TextoTipoTirada { get; set; }

		public string MultiplicadorEspecialidad { get; set; }

		public string TextoCroto { get; set; }

		public string RutaCompletaImagenCroto { get; set; }

		public string RutaCompletaImagenMarco { get; set; }

		public string ResultadoTirada { get; set; }

		public string ResultadoDetalladoTirada { get; set; }

		public string ModStat { get; set; }

		public bool UtilizaMultiplicadorDelPuntoVital { get; set; }

		public bool EsTiradaDeDaño => ObtenerEsTiradaDeTipo(ETipoTirada.Daño);

		public bool EsTiradaPersonalizada => ObtenerEsTiradaDeTipo(ETipoTirada.Personalizada);

		public bool EsTiradaStat => ObtenerEsTiradaDeTipo(ETipoTirada.Stat);

		public string RutaCompletaImagenTipoTirada => ObtenerPathImagenTipoTirada(ViewModelComboBoxTipoTirada.Valor);

		public ViewModelComboBox<EStat> ViewModelComboBoxStat { get; set; }

		public ViewModelComboBox<EManoUtilizada> ViewModelComboBoxManoUtilizada { get; set; }

		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada {get; set; }

		public ViewModelComboBox<ModeloPresetTirada> ViewModelComboBoxPresetsDisponibles { get; set; }

		public ViewModelMultiselectComboBox<ETipoDeDaño> ViewModelMultiselectComboBoxTipoDeDaño { get; set; }

		public ViewModelMultiselectComboBox<ControladorBase> ViewModelMultiselectComboBoxContenedorTirada { get; set; }

		public ViewModelSeleccionDeControlador<ControladorTiradaBase> ViewModelSeleccionTirada { get; set; }

		public ViewModelSeleccionDeControlador<ControladorPersonaje> ViewModelSeleccionUsuario { get; set; }

		public ViewModelSeleccionDeControlador<ControladorBase> ViewModelSeleccionObjetivo { get; set; }

		public ViewModelSeleccionDeControlador<ControladorBase> ViewModelSeleccionContenedor { get; set; }

		public ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot> ViewModelInventario { get; set; }

		public ModeloPresetTirada PresetTirada { get; set; } = new ModeloPresetTirada();

		public ControladorTiradaBase ControladorTirada { get; set; }

		public ViewModelItemListaBase UsuarioSeleccionado => ViewModelSeleccionUsuario.ItemSeleccionado;

		public ICommand ComandoRealizarTirada { get; private set; }

		public ICommand ComandoAplicarDaño { get; private set; }

		public ICommand ComandoCrearPreset { get; private set; }

		public ICommand ComandoSalir { get; private set; }

		public ICommand ComandoToggleExistenciaTirada { get; private set; }

		public ViewModelRealizarTirada(ControladorPersonaje usuario, ControladorHabilidad habilidadContenedoraDeTirada, ControladorItem itemContenedorDeTirada)
		{
			ActualizarCroto(EImagenCroto.ImagenCrotoNormal);
			ActualizarMarco(EMarcoGlobo.MarcoTiradaNueva);

			//Seleccion stat
			ViewModelComboBoxStat = new ViewModelComboBox<EStat>(EnumHelpers.StatsDisponibles);

			ViewModelComboBoxStat.OnValorSeleccionadoCambio += StatSeleccionadaCambioHandler;

			ViewModelComboBoxManoUtilizada = new ViewModelComboBox<EManoUtilizada>(Enum.GetValues<EManoUtilizada>().ToList());

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

			ViewModelSeleccionContenedor?.SeleccionarControlador(habilidadContenedoraDeTirada != null ? habilidadContenedoraDeTirada : itemContenedorDeTirada);

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

			ViewModelSeleccionContenedor = new ViewModelSeleccionDeControlador<ControladorBase>(new List<ControladorBase>
			{
				new ControladorHabilidad(new ModeloHabilidad
				{
					Nombre = "Explosion",
					Descripcion = "Piola descripcion",
					CostoDeMana = 120,
					TipoDeHabilidad = ETipoHabilidad.Hechizo
				}),

				new ControladorHabilidad(new ModeloHabilidad
				{
					Nombre = "Baile cautivador",
					Descripcion = "Piola descripcion",
					CostoDeMana = 0,
					TipoDeHabilidad = ETipoHabilidad.Skill
				}),
			});

			if (EsTiradaDeDaño)
			{
				ViewModelSeleccionObjetivo = new ViewModelSeleccionDeControlador<ControladorBase>(
					new List<ControladorBase>(
						SistemaPrincipal.DatosRolSeleccionado.Personajes.Where(p => p != nuevoUsuario)));

				ViewModelInventario = new ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot>(null);

				var itemsInventario = nuevoUsuario.modelo.SlotsBase.Select(s =>
				{
					var controladorSlot = SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(s, true);

					return new ViewModelElementoArbolItemInventario(ViewModelInventario, null, controladorSlot, ETipoItem.TODOS);
				}).ToList();

				ViewModelInventario.Hijos.AddRange(itemsInventario);
			}
		}

		private void TipoDeTiradaCambioHandler(ViewModelItemComboBoxBase<ETipoTirada> valoranterior, ViewModelItemComboBoxBase<ETipoTirada> valoractual)
		{
			ViewModelSeleccionObjetivo = null;

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