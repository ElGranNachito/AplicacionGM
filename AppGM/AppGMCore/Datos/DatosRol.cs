using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using CoolLogs;

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
		
		//TODO: Ver
        public List<ControladorAmbiente> Ambientes { get; set; } = null;

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

		#region Metodos

		public async Task CargarDatos()
		{
			GCLatencyMode modoOriginal = GCSettings.LatencyMode;
			GCSettings.LatencyMode = GCLatencyMode.Batch;

			await Task.Run(() =>
			{
				//Si no hay datos creamos algunos para probar
				if (!mDBRol.Mapas.Any())
				{
					ModeloMaster master1 = new ModeloMaster
					{
						Nombre = "Master Bahsakah",

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
						TipoPersonaje     = ETipoPersonaje.Master,
						NumeroParty       = ENumeroParty.Party_1
                    };

					ModeloServant servant1 = new ModeloServant
					{
						Nombre = "Servant Bahsakah",

						PesoMaximoCargable = 100,
						PesoCargado = 10,

						MaxHp = 20,
						Hp = 20,
						Str = 12,
						Agi = 13,
						End = 12,
						Int = 13,
						Lck = 13,

						EClaseServant = EClaseServant.Berserker,
						TipoPersonaje = ETipoPersonaje.Servant,
						NumeroParty   = ENumeroParty.Party_1
					};

					ModeloMaster master2 = new ModeloMaster
					{
						Nombre = "Master Raidah",

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

						EClaseDeSuServant = EClaseServant.Rider,
						TipoPersonaje     = ETipoPersonaje.Master,
                        NumeroParty       = ENumeroParty.Party_2
					};

					ModeloServant servant2 = new ModeloServant
					{
						Nombre = "Servant Raidah",

						PesoMaximoCargable = 100,
						PesoCargado = 10,

						MaxHp = 20,
						Hp = 20,
						Str = 12,
						Agi = 13,
						End = 12,
						Int = 13,
						Lck = 13,

						EClaseServant = EClaseServant.Rider,
						TipoPersonaje = ETipoPersonaje.Servant,
                        NumeroParty   = ENumeroParty.Party_2
					};

					ModeloContrato contratoPiola = new ModeloContrato
					{
						Nombre = "PiolaContrato",
						Descripcion = "Contrato entre gente de contratos",

						EsVigente = true
					};

					TIPersonajeContrato master1Contrato = new TIPersonajeContrato
					{
						Contrato = contratoPiola,
						Personaje = master1
					};

					TIPersonajeContrato master2Contrato = new TIPersonajeContrato
					{
						Contrato = contratoPiola,
						Personaje = master2
					};

					contratoPiola.PersonajesAfectados.Add(master1Contrato);
					contratoPiola.PersonajesAfectados.Add(master2Contrato);

					ModeloAlianza alianzaPiola = new ModeloAlianza
					{
						EIconoAlianza = EIconoAlianza.Team_UwU,
						FormatoImagen = EFormatoImagen.Png,

						PathImagenIcono = "Team_UwU",
						Nombre = "TeamUwU",
						Descripcion = "Alianza entre magos para ganar el grial mas facil.",

						EsVigente = true,
					};

					TIAlianzaContrato alianzaPiolaContrato = new TIAlianzaContrato
					{
						Contrato = contratoPiola,
						Alianza = alianzaPiola
					};

					alianzaPiola.ContratoDeAlianza = alianzaPiolaContrato;

					TIPersonajeAlianza master1Alianza = new TIPersonajeAlianza
					{
						Personaje = master1,
						Alianza = alianzaPiola
					};

					TIPersonajeAlianza master2Alianza = new TIPersonajeAlianza
					{
						Personaje = master2,
						Alianza = alianzaPiola
					};

					alianzaPiola.PersonajesAfectados.Add(master1Alianza);
					alianzaPiola.PersonajesAfectados.Add(master2Alianza);

					master1.Alianzas.Add(master1Alianza);
					master2.Alianzas.Add(master2Alianza);

					ModeloMagia magia1 = new ModeloMagia
					{
						Nombre = "Chiquiti Pum pum",
						Descripcion = "Habilidad ultra potente que destruye todo a su paso",
						Nivel = 4,

						CostoDeMana = 50
					};

					ModeloMagia magia2 = new ModeloMagia
					{
						Nombre = "Bibidi badibi bu",
						Descripcion = "Magia absoluta que le mermite recrear cualquir cosa que se le venga a la mente",
						Nivel = 4,

						CostoDeMana = 70
					};

					ModeloNoblePhantasm noblePhantasm1 = new ModeloNoblePhantasm()
					{
						Nombre = "Bomb-Voyage",
						Descripcion = "Tremenda NP que le permite explotar todo en su rango de hechiceria",

						CostoDeMana = 10
					};

					ModeloNoblePhantasm noblePhantasm2 = new ModeloNoblePhantasm()
					{
						Nombre = "Excalibur-Breaker",
						Descripcion = "Tremendo NP que le permite romper cualquier otro NP a todo momento",

						CostoDeMana = 5
					};

					TIPersonajeMagia masterMagia1 = new TIPersonajeMagia
					{
						Personaje = master1,
						Magia = magia1
					};

					TIPersonajeMagia masterMagia2 = new TIPersonajeMagia
					{
						Personaje = master2,
						Magia = magia2
					};

					TIServantNoblePhantasm servantNoblePhantasm1 = new TIServantNoblePhantasm
					{
						Servant = servant1,
						NoblePhantasm = noblePhantasm1
					};

					TIServantNoblePhantasm servantNoblePhantasm2 = new TIServantNoblePhantasm
					{
						Servant = servant2,
						NoblePhantasm = noblePhantasm2
					};

					master1.Magias.Add(masterMagia1);
					master2.Magias.Add(masterMagia2);

					servant1.NoblePhantasms.Add(servantNoblePhantasm1);
					servant2.NoblePhantasms.Add(servantNoblePhantasm2);

					ModeloMapa mapa = new ModeloMapa
					{
						EFormatoImagen = EFormatoImagen.Png,
						NombreMapa = "Seoul"
					};

					ModeloUnidadMapa unidadServant1 = new ModeloUnidadMapaMasterServant
					{
						ETipoUnidad = ETipoUnidad.Servant,
						Nombre = "Bahsaka",
						EClaseServant = EClaseServant.Berserker
					};

					ModeloUnidadMapa unidadServant2 = new ModeloUnidadMapaMasterServant
					{
						ETipoUnidad = ETipoUnidad.Servant,
						Nombre = "Raidah",
						EClaseServant = EClaseServant.Rider
					};

					ModeloUnidadMapa unidadMaster1 = new ModeloUnidadMapaMasterServant
					{
						ETipoUnidad = ETipoUnidad.Master,
						Nombre = "Master Bahsaka",
						EClaseServant = EClaseServant.Berserker
					};

					ModeloUnidadMapa unidadMaster2 = new ModeloUnidadMapaMasterServant
					{
						ETipoUnidad = ETipoUnidad.Master,
						Nombre = "Master Raidah",
						EClaseServant = EClaseServant.Rider
					};

					ModeloVector2 posBerserker = new ModeloVector2
					{
						X = 150,
						Y = 100
					};

					ModeloVector2 posMasterBerserker = new ModeloVector2
					{
						X = 170,
						Y = 120
					};

					ModeloVector2 posRider = new ModeloVector2
					{
						X = 330,
						Y = 170
					};

					ModeloVector2 posMasterRider = new ModeloVector2
					{
						X = 410,
						Y = 40
					};

					TIUnidadMapaVector2 tiPosBerserker = new TIUnidadMapaVector2
					{
						Unidad = unidadServant1,
						Posicion = posBerserker
					};

					TIUnidadMapaVector2 tiPosMasterBerserker = new TIUnidadMapaVector2
					{
						Unidad = unidadMaster1,
						Posicion = posMasterBerserker
					};

					TIUnidadMapaVector2 tiPosRider = new TIUnidadMapaVector2
					{
						Unidad = unidadServant2,
						Posicion = posRider
					};

					TIUnidadMapaVector2 tiPosMasterRider = new TIUnidadMapaVector2
					{
						Unidad = unidadMaster2,
						Posicion = posMasterRider
					};

					TIPersonajeUnidadMapa tiServantUnidadMapa1 = new TIPersonajeUnidadMapa
					{
						Unidad = unidadServant1,
						Personaje = servant1
					};

					TIPersonajeUnidadMapa tiMasterUnidadMapa1 = new TIPersonajeUnidadMapa
					{
						Unidad = unidadMaster1,
						Personaje = master1
					};

					TIPersonajeUnidadMapa tiServantUnidadMapa2 = new TIPersonajeUnidadMapa
					{
						Unidad = unidadServant2,
						Personaje = servant2
					};

					TIPersonajeUnidadMapa tiMasterUnidadMapa2 = new TIPersonajeUnidadMapa
					{
						Unidad = unidadMaster2,
						Personaje = master2
					};

					unidadServant1.Posicion = tiPosBerserker;
					unidadServant2.Posicion = tiPosRider;
					unidadMaster1.Posicion = tiPosMasterBerserker;
					unidadMaster2.Posicion = tiPosMasterRider;

					TIMapaUnidadMapa tiMapaBerserker = new TIMapaUnidadMapa
					{
						Mapa = mapa,
						Unidad = unidadServant1
					};

					TIMapaUnidadMapa tiMapaMasterBerserker = new TIMapaUnidadMapa
					{
						Mapa = mapa,
						Unidad = unidadMaster1
					};

					TIMapaUnidadMapa tiMapaRider = new TIMapaUnidadMapa
					{
						Mapa = mapa,
						Unidad = unidadServant2
					};

					TIMapaUnidadMapa tiMapaMasterRider = new TIMapaUnidadMapa
					{
						Mapa = mapa,
						Unidad = unidadMaster2
					};

					mapa.PosicionesUnidades.Add(tiMapaBerserker);
					mapa.PosicionesUnidades.Add(tiMapaMasterBerserker);
					mapa.PosicionesUnidades.Add(tiMapaRider);
					mapa.PosicionesUnidades.Add(tiMapaMasterRider);

					mDBRol.Add(new ModeloAdministradorDeCombate
					{
						IndicePersonajeTurnoActual = 0,
						Nombre = "SuperCombateFeroz",
						TurnoActual = 0,
						Participantes = new List<TIAdministradorDeCombateParticipante>()
					});

					mDBRol.Add(mapa);
					mDBRol.Add(contratoPiola);
					mDBRol.Add(master1Contrato);
					mDBRol.Add(master2Contrato);
					mDBRol.Add(alianzaPiola);
					mDBRol.Add(alianzaPiolaContrato);
					mDBRol.Add(master1Alianza);
					mDBRol.Add(master2Alianza);
					mDBRol.Add(unidadServant1);
					mDBRol.Add(unidadMaster1);
					mDBRol.Add(unidadServant2);
					mDBRol.Add(unidadMaster2);
					mDBRol.Add(posBerserker);
					mDBRol.Add(posMasterBerserker);
					mDBRol.Add(posRider);
					mDBRol.Add(posMasterRider);
					mDBRol.Add(tiMapaBerserker);
					mDBRol.Add(tiMapaMasterBerserker);
					mDBRol.Add(tiMapaRider);
					mDBRol.Add(tiMapaMasterRider);
					mDBRol.Add(tiPosBerserker);
					mDBRol.Add(tiPosMasterBerserker);
					mDBRol.Add(tiPosRider);
					mDBRol.Add(tiPosMasterRider);
					mDBRol.Add(tiServantUnidadMapa1);
					mDBRol.Add(tiMasterUnidadMapa2);
					mDBRol.Add(tiServantUnidadMapa2);
					mDBRol.Add(tiMasterUnidadMapa1);

					mDBRol.Add(servant1);
					mDBRol.Add(master1);
					mDBRol.Add(servant2);
					mDBRol.Add(master2);
					mDBRol.Add(magia1);
					mDBRol.Add(magia2);
					mDBRol.Add(masterMagia1);
					mDBRol.Add(masterMagia2);
					mDBRol.Add(masterMagia1);
					mDBRol.Add(masterMagia2);
					mDBRol.Add(servantNoblePhantasm1);
					mDBRol.Add(servantNoblePhantasm2);

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

					ModeloParticipante participante1 = new ModeloParticipante
					{
						EsSuTurno = false
					};

					ModeloParticipante participante2 = new ModeloParticipante
					{
						EsSuTurno = false
					};

					TIParticipantePersonaje personajeParticipante1 = new TIParticipantePersonaje
					{
						Participante = participante1,
						Personaje = master1
					};

					TIParticipantePersonaje personajeParticipante2 = new TIParticipantePersonaje
					{
						Participante = participante2,
						Personaje = master2
					};

					participante1.Personaje = personajeParticipante1;
					participante2.Personaje = personajeParticipante2;

					ModeloAccion accion1 = new ModeloAccion
					{
						Descripcion = "Se tiro un backflip y se ligo a una piba"
					};

					ModeloAccion accion2 = new ModeloAccion
					{
						Descripcion = "Visito un maid cafe y se ligo a una de las maids"
					};

					TIParticipanteAccion accionParticipante1 = new TIParticipanteAccion
					{
						Accion = accion1,
						Participante = participante1
					};

					TIParticipanteAccion accionParticipante2 = new TIParticipanteAccion
					{
						Accion = accion2,
						Participante = participante2
					};

					participante1.AccionesRealizadas.Add(accionParticipante1);
					participante2.AccionesRealizadas.Add(accionParticipante2);

					TIAdministradorDeCombateParticipante participante1Combate = new TIAdministradorDeCombateParticipante
					{
						AdministradorDeCombate = combate,
						Participante = participante1
					};

					TIAdministradorDeCombateParticipante participante2Combate = new TIAdministradorDeCombateParticipante
					{
						AdministradorDeCombate = combate,
						Participante = participante2
					};

					combate.Mapas.Add(mapaCombate);
					combate.Participantes.Add(participante1Combate);
					combate.Participantes.Add(participante2Combate);

					mDBRol.Add(combate);
					mDBRol.Add(mapaCombate);
					mDBRol.Add(participante1);
					mDBRol.Add(participante2);
					mDBRol.Add(personajeParticipante1);
					mDBRol.Add(personajeParticipante2);
					mDBRol.Add(accion1);
					mDBRol.Add(accion2);
					mDBRol.Add(accionParticipante1);
					mDBRol.Add(accionParticipante2);
					mDBRol.Add(participante1Combate);
					mDBRol.Add(participante2Combate);

					mDBRol.SaveChanges();
				}

				//Cargamos los datos de una manera bastante primitiva :u

				var personajeUnidadMapa =
					(from m in mDBRol.PersonajesUnidadesMapa
					 select m).ToList();

				var personajeAlianzas =
					(from m in mDBRol.PersonajesAlianzas
					 select m).ToList();

				var masters =
					(from m in mDBRol.Masters
					 select m).ToList();

				masters.TrimExcess();

				var servants =
					(from m in mDBRol.Servants
					 select m).ToList();

				servants.TrimExcess();

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

				SistemaPrincipal.LoggerGlobal.Log("Creando controladores...", ESeveridad.Info);

				Masters = new List<ControladorPersonaje>(masters.Count);

				for (int i = 0; i < masters.Count; ++i)
				{
					ControladorPersonaje controladorActual = new ControladorPersonaje(masters[i]);

					Masters.Add(controladorActual);
					masters[i].controlador = controladorActual;
				}

				Servants = new List<ControladorPersonaje>(servants.Count);

				for (int i = 0; i < servants.Count; ++i)
				{
					ControladorPersonaje controladorActual = new ControladorPersonaje(servants[i]);

					Servants.Add(controladorActual);
					servants[i].controlador = controladorActual;
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

	#endregion
}