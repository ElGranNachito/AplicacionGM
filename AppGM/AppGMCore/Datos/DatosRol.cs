using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public class DatosRol
	{
		#region Miembros

		private RolContext mDBRol;

		#endregion

		#region Propiedades

		public List<ControladorPersonaje> Servants { get; set; }                     = new List<ControladorPersonaje>();
		public List<ControladorPersonaje> Masters { get; set; }                      = new List<ControladorPersonaje>();
		public List<ControladorInvocacion> Invocaciones { get; set; }                = new List<ControladorInvocacion>();
		public List<ControladorUtilizable> Items { get; set; }                       = new List<ControladorUtilizable>();
		public List<ControladorPortable> Portables { get; set; }                     = new List<ControladorPortable>();
		public List<ControladorPortable> PortableOfensivo { get; set; }              = new List<ControladorPortable>();
		public List<ControladorDefensivo> Defensivos { get; set; }                   = new List<ControladorDefensivo>();
		public List<ControladorDefensivoAbsoluto> DefensivosAbsolutos { get; set; }  = new List<ControladorDefensivoAbsoluto>();
		public List<ControladorConsumible> Consumibles { get; set; }                 = new List<ControladorConsumible>();
		public List<ControladorArmaDistancia> ArmasDistancia { get; set; }           = new List<ControladorArmaDistancia>();
		public List<ControladorSlot> Slots { get; set; }                             = new List<ControladorSlot>();
		public List<ControladorHabilidad> Perks { get; set; }                        = new List<ControladorHabilidad>();
		public List<ControladorHabilidad> Skills { get; set; }                       = new List<ControladorHabilidad>();
		public List<ControladorHabilidad> NoblePhantasms { get; set; }               = new List<ControladorHabilidad>();
		public List<ControladorMagia> Magias { get; set; }                           = new List<ControladorMagia>();
		public List<ControladorEfecto> Efectos { get; set; }                         = new List<ControladorEfecto>();
		public List<ControladorCondicion> Condiciones { get; set; }                  = new List<ControladorCondicion>();
		public List<ControladorAdministradorDeCombate> CombatesActivos { get; set; } = new List<ControladorAdministradorDeCombate>();
		public List<ControladorLimitador> Limitadores { get; set; }                  = new List<ControladorLimitador>();
		public List<ControladorCargasHabilidad> CargasHabilidades { get; set; }      = new List<ControladorCargasHabilidad>();
		public List<ControladorParticipante> Participantes { get; set; }             = new List<ControladorParticipante>();

		//El primer mapa siempre es el principal
		public List<ControladorMapa> Mapas { get; set; } = new List<ControladorMapa>(); 
		#endregion

		/// <summary>
		/// Crea la clase y abre conexion con la base de datos
		/// </summary>
		/// <param name="_modeloRol"></param>
		public DatosRol(ModeloRol _modeloRol)
		{
			mDBRol = new RolContext(_modeloRol.Nombre);
		}
		public async Task CargarDatos()
		{
			await Task.Run(() =>
			{
				if (!mDBRol.Mapas.Any())
				{
					ModeloMaster master = new ModeloMaster
					{
						Nombre = "Un pibe",

						MaxHp = 20,
						Hp = 20,
						Str = 12,
						Agi = 13,
						End = 12,
						Intel = 13,
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

				var masters =
					(from m in mDBRol.Masters
					select m).ToList();

				var mapas =
					(from m in mDBRol.Mapas
						select m).ToList();

				var unidadesmapa =
					(from u in mDBRol.UnidadesMapa
						select u).ToList();

				var tiunidadesmapa =
					(from u in mDBRol.MapasUnidadesMapa
						select u).ToList();

				var tiunidadesposiciones =
					(from u in mDBRol.UnidadesMapaVectores2
						select u).ToList();

				var posiciones =
					(from p in mDBRol.Vectores2
						select p).ToList();

				var combates =
					(from c in mDBRol.Combates
						select c).ToList();

				var participantes =
					(from p in mDBRol.Participantes
						select p).ToList();
				var acciones =
					(from a in mDBRol.Acciones
						select a).ToList();

				var tiParticipanteAccion =
					(from ti in mDBRol.ParticipanteAccion
						select ti).ToList();
				var tiParticipantePersonaje =
					(from ti in mDBRol.ParticipantePersonaje
						select ti).ToList();
				var tiParticipanteCombate =
					(from ti in mDBRol.CombateParticipantes
						select ti).ToList();
				var tiCombateMapa =
					(from ti in mDBRol.CombateMapas
						select ti).ToList();

				for (int i = 0; i < masters.Count; ++i)
				{
					ControladorPersonaje controladorActual = new ControladorPersonaje(masters[i]);

                    Masters.Add(controladorActual);
					masters[i].controlador = controladorActual;
				}

                for (int i = 0; i < mapas.Count; ++i)
                {
					ControladorMapa controladorActual = new ControladorMapa(mapas[i]);

                    Mapas.Add(controladorActual);
                    mapas[i].controladorMapa = controladorActual;
                }

                for (int i = 0; i < participantes.Count; ++i)
                {
                    ControladorParticipante controladorActual = new ControladorParticipante(participantes[i]);

					Participantes.Add(controladorActual);
					participantes[i].controladorParticipante = controladorActual;
				}

                for (int i = 0; i < combates.Count; ++i)
                {
                    ControladorAdministradorDeCombate controladorActual = new ControladorAdministradorDeCombate(combates[i]);

                    CombatesActivos.Add(controladorActual);
                    combates[i].controladorAdministradorDeCombate = controladorActual;
                }

				//TODO: Cargar datos
			});
		}

		/// <summary>
		/// Funcion que cierra la conexion con la base de datos. Es necesario llamarla al terminar de utilizar la base de datos
		/// </summary>
		public void CerrarConexion()
		{
			mDBRol.Dispose();
		}

		public async Task GuardarDatosAsync()
		{
			await mDBRol.SaveChangesAsync();
		}

		public void GuardarDatos()
		{
			mDBRol.SaveChanges();
		}

		public void GuardarModelo(ModeloBaseSK modelo)
		{
			mDBRol.Add(modelo);
		}

		public void EliminarModelo(ModeloBaseSK modelo)
		{
			mDBRol.Remove(modelo);
		}
	}
}