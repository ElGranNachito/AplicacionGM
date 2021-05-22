using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public class DatosRol
	{
		#region Miembros

		private RolContext mDBRol;
        private ModeloRol mRolSeleccionado;

		#endregion

		#region Propiedades

		public List<ControladorPersonaje> Servants { get; set; }                     = null;
		public List<ControladorPersonaje> Masters { get; set; }                      = null;
		public List<ControladorInvocacion> Invocaciones { get; set; }                = null;
		public List<ControladorUtilizable> Items { get; set; }                       = null;
		public List<ControladorPortable> Portables { get; set; }                     = null;
		public List<ControladorPortable> PortableOfensivo { get; set; }              = null;
		public List<ControladorDefensivo> Defensivos { get; set; }                   = null;
		public List<ControladorDefensivoAbsoluto> DefensivosAbsolutos { get; set; }  = null;
		public List<ControladorConsumible> Consumibles { get; set; }                 = null;
		public List<ControladorArmaDistancia> ArmasDistancia { get; set; }           = null;
		public List<ControladorSlot> Slots { get; set; }                             = null;
		public List<ControladorHabilidad> Perks { get; set; }                        = null;
		public List<ControladorHabilidad> Skills { get; set; }                       = null;
		public List<ControladorHabilidad> NoblePhantasms { get; set; }               = null;
		public List<ControladorMagia> Magias { get; set; }                           = null;
		public List<ControladorEfecto> Efectos { get; set; }                         = null;
		public List<ControladorCondicion> Condiciones { get; set; }                  = null;
		public List<ControladorAdministradorDeCombate> CombatesActivos { get; set; } = null;
		public List<ControladorLimitador> Limitadores { get; set; }                  = null;
		public List<ControladorCargasHabilidad> CargasHabilidades { get; set; }      = null;
		public List<ControladorParticipante> Participantes { get; set; }             = null;

		//El primer mapa siempre es el principal
		public List<ControladorMapa> Mapas { get; set; } = null; 
		#endregion

		/// <summary>
		/// Crea la clase y abre conexion con la base de datos
		/// </summary>
		/// <param name="_modeloRol"></param>
		public DatosRol(ModeloRol _rolSeleccionado)
        {
            mRolSeleccionado = _rolSeleccionado;

			mDBRol = new RolContext();
		}
		public async Task CargarDatos()
        {
            GCLatencyMode modoOriginal = GCSettings.LatencyMode;
            GCSettings.LatencyMode = GCLatencyMode.Batch;

			await Task.Run(() =>
			{
				//Si no hay datos creamos algunos para probar
				if (!mDBRol.Mapas.Any())
				{
					ModeloMaster master = new ModeloMaster
					{
						Nombre = "Un pibe",

						PesoMaximoCargable = 100,
						PesoCargado = 10,

						MaxHp = 20,
						Hp = 20,
						Str = 12,
						Agi = 13,
						End = 12,
						Int = 13,
						Lck = 13,
						Chr = 12,

						EClaseDeSuServant = EClaseServant.Berserker,
						TipoPersonaje = ETipoPersonaje.Master
					};

					ModeloMagia magia1 = new ModeloMagia
					{
						Nombre = "Chiquiti Pum pum",
						Descripcion = "Habilidad ultra potente que destruye todo a su paso",
						Nivel = 4,

						CostoDeMana = 50
					};

					TIPersonajeMagia masterMagia1 = new TIPersonajeMagia
					{
						Personaje = master,
						Magia = magia1
					};

					master.Magias.Add(masterMagia1);

					ModeloMapa mapa = new ModeloMapa
					{
						EFormatoImagen = EFormatoImagen.Png,
						NombreMapa = "Seoul"
					};

					ModeloUnidadMapa elemento1 = new ModeloUnidadMapaMasterServant
					{
						ETipoUnidad = ETipoUnidad.Servant,
						Nombre = "Saber",
						EClaseServant = EClaseServant.Saber
					};

					ModeloUnidadMapa elemento2 = new ModeloUnidadMapaMasterServant
					{
						ETipoUnidad = ETipoUnidad.Master,
						Nombre = "Master Saber",
						EClaseServant = EClaseServant.Saber

					};

					ModeloVector2 posSaber = new ModeloVector2
					{
						X = 150,
						Y = 100
					};

					ModeloVector2 posMaster = new ModeloVector2
					{
						X = 170,
						Y = 120
					};

					TIUnidadMapaVector2 tiPosSaber = new TIUnidadMapaVector2
					{
						Unidad = elemento1,
						Posicion = posSaber
					};

					TIUnidadMapaVector2 tiPosMaster = new TIUnidadMapaVector2
					{
						Unidad = elemento2,
						Posicion = posMaster
					};

					TIPersonajeUnidadMapa tiMasterUnidadMapa = new TIPersonajeUnidadMapa
					{
						Unidad = elemento2,
						Personaje = master
					};

					elemento1.Posicion = tiPosSaber;
					elemento2.Posicion = tiPosMaster;

					TIMapaUnidadMapa tiMapaSaber = new TIMapaUnidadMapa
					{
						Mapa = mapa,
						Unidad = elemento1
					};

					TIMapaUnidadMapa tiMapaMaster = new TIMapaUnidadMapa
					{
						Mapa = mapa,
						Unidad = elemento2
					};

					mapa.PosicionesUnidades.Add(tiMapaSaber);
					mapa.PosicionesUnidades.Add(tiMapaMaster);

					mDBRol.Add(new ModeloAdministradorDeCombate
					{
						IndicePersonajeTurnoActual = 0,
						Nombre = "SuperCombateFeroz",
						TurnoActual = 0,
						Participantes = new List<TIAdministradorDeCombateParticipante>()
					});

					mDBRol.Add(mapa);
					mDBRol.Add(elemento1);
					mDBRol.Add(elemento2);
					mDBRol.Add(posSaber);
					mDBRol.Add(posMaster);
					mDBRol.Add(tiMapaSaber);
					mDBRol.Add(tiMapaMaster);
					mDBRol.Add(tiPosSaber);
					mDBRol.Add(tiPosMaster);
					mDBRol.Add(tiMasterUnidadMapa);

					mDBRol.Add(master);
					mDBRol.Add(magia1);
					mDBRol.Add(masterMagia1);

					ModeloAdministradorDeCombate combate = new ModeloAdministradorDeCombate
					{
						IndicePersonajeTurnoActual = 0,
						TurnoActual = 0,
						Nombre = "最後の戦い"
					};

					TIAdministradorDeCombateMapa mapaCombate = new TIAdministradorDeCombateMapa
					{
						AdministradorDeCombate = combate,
						Mapa = mapa
					};

					ModeloParticipante participante = new ModeloParticipante
					{
						EsSuTurno = false
					};

					TIParticipantePersonaje personajeParticipante = new TIParticipantePersonaje
					{
						Participante = participante,
						Personaje = master
					};

					participante.Personaje = personajeParticipante;

					ModeloAccion accion = new ModeloAccion
					{
						Descripcion = "Se tiro un backflip y se ligo a una piba"
					};

					TIParticipanteAccion accionParticipante = new TIParticipanteAccion
					{
						Accion = accion,
						Participante = participante
					};

					participante.AccionesRealizadas.Add(accionParticipante);

					TIAdministradorDeCombateParticipante participantesCombate = new TIAdministradorDeCombateParticipante
					{
						AdministradorDeCombate = combate,
						Participante = participante
					};

					combate.Mapas.Add(mapaCombate);
					combate.Participantes.Add(participantesCombate);

					mDBRol.Add(combate);
					mDBRol.Add(mapaCombate);
					mDBRol.Add(participante);
					mDBRol.Add(personajeParticipante);
					mDBRol.Add(accion);
					mDBRol.Add(accionParticipante);
					mDBRol.Add(participantesCombate);
					
					mDBRol.SaveChanges();
				}

				//Cargamos los datos de una manera bastante primitiva :u

				var masters = 
					(from m in mDBRol.Masters
					select m).ToList();
				
				masters.TrimExcess();

				var mapas =
					(from m in mDBRol.Mapas
						select m).ToList();

				mapas.TrimExcess();

				var unidadesmapa =
					(from u in mDBRol.UnidadesMapa
						select u).ToList();

				unidadesmapa.TrimExcess();

				var tiunidadesmapa =
					(from u in mDBRol.MapasUnidadesMapa
						select u).ToList();

				tiunidadesmapa.TrimExcess();

				var tiunidadesposiciones =
					(from u in mDBRol.UnidadesMapaVectores2
						select u).ToList();

				tiunidadesposiciones.TrimExcess();

				var posiciones =
					(from p in mDBRol.Vectores2
						select p).ToList();

				posiciones.TrimExcess();

				var combates =
					(from c in mDBRol.Combates
						select c).ToList();

				combates.TrimExcess();

				var participantes =
					(from p in mDBRol.Participantes
						select p).ToList();

                participantes.TrimExcess();

				var acciones =
					(from a in mDBRol.Acciones
						select a).ToList();

				acciones.TrimExcess();

				var tiParticipanteAccion =
					(from ti in mDBRol.ParticipanteAccion
						select ti).ToList();

				tiParticipanteAccion.TrimExcess();

				var tiParticipantePersonaje =
					(from ti in mDBRol.ParticipantePersonaje
						select ti).ToList();

				tiParticipantePersonaje.TrimExcess();

				var tiParticipanteCombate =
					(from ti in mDBRol.CombateParticipantes
						select ti).ToList();

				tiParticipanteCombate.TrimExcess();

				var tiCombateMapa =
					(from ti in mDBRol.CombateMapas
						select ti).ToList();

				tiCombateMapa.TrimExcess();

                Masters = new List<ControladorPersonaje>(masters.Count);

				for (int i = 0; i < masters.Count; ++i)
				{
					ControladorPersonaje controladorActual = new ControladorPersonaje(masters[i]);

                    Masters.Add(controladorActual);
					masters[i].controlador = controladorActual;
				}

                Mapas = new List<ControladorMapa>(mapas.Count);

                for (int i = 0; i < mapas.Count; ++i)
                {
					ControladorMapa controladorActual = new ControladorMapa(mapas[i]);

                    Mapas.Add(controladorActual);
                    mapas[i].controladorMapa = controladorActual;
                }

                Participantes = new List<ControladorParticipante>(participantes.Count);

                for (int i = 0; i < participantes.Count; ++i)
                {
                    ControladorParticipante controladorActual = new ControladorParticipante(participantes[i]);

					Participantes.Add(controladorActual);
					participantes[i].controladorParticipante = controladorActual;
				}

                CombatesActivos = new List<ControladorAdministradorDeCombate>(combates.Count);

                for (int i = 0; i < combates.Count; ++i)
                {
                    ControladorAdministradorDeCombate controladorActual = new ControladorAdministradorDeCombate(combates[i]);

                    CombatesActivos.Add(controladorActual);
                    combates[i].controladorAdministradorDeCombate = controladorActual;
                }

				//TODO: Continuar cargando datos
			});

            GCSettings.LatencyMode = modoOriginal;
		}

		/// <summary>
		/// Funcion que cierra la conexion con la base de datos. Es necesario llamarla al terminar de utilizar la base de datos
		/// </summary>
		public void CerrarConexion()
		{
            mDBRol.Dispose();
		}

		/// <summary>
		/// Actualiza el estado de los modelos en la base de datos asincronicamente
		/// </summary>
		/// <returns></returns>
		public async Task GuardarDatosAsync()
		{
			await mDBRol.SaveChangesAsync();
		}

		/// <summary>
		/// Actualiza el estado de los modelos en la base de datos
		/// </summary>
		public void GuardarDatos()
		{
			mDBRol.SaveChanges();
		}

		/// <summary>
		/// Guarda un nuevo modelo en la base de datos
		/// </summary>
		/// <param name="modelo"></param>
		public void GuardarModelo(ModeloBaseSK modelo)
		{
			mDBRol.Add(modelo);
		}

		/// <summary>
		/// Elimina un modelo de la base de datos
		/// </summary>
		/// <param name="modelo"></param>
		public void EliminarModelo(ModeloBaseSK modelo)
		{
			mDBRol.Remove(modelo);
		}
	}
}