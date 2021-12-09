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

		public RolContext BaseDeDatos;
        private readonly ModeloRol mRolSeleccionado;

        #endregion

		#region Propiedades

		public List<ControladorPersonaje> Servants { get; set; }                     = null;
		public List<ControladorPersonaje> Masters { get; set; }                      = null;
		public List<ControladorInvocacion> Invocaciones { get; set; }                = null;
		public List<ControladorPersonaje> NPCs { get; set; }                         = null;
		public List<ControladorPersonaje> Personajes { get; set; }                   = new List<ControladorPersonaje>();
		public List<ControladorItem> Items { get; set; }                             = null;
		public List<ControladorSlot> Slots { get; set; }                             = null;
		public List<ControladorHabilidad> Perks { get; set; }                        = null;
		public List<ControladorHabilidad> Skills { get; set; }                       = null;
		public List<ControladorHabilidad> NoblePhantasms { get; set; }               = null;
		public List<ControladorMagia> Magias { get; set; }                           = null;
		public List<ControladorEfecto> Efectos { get; set; }                         = null;
		public List<ControladorAdministradorDeCombate> CombatesActivos { get; set; } = null;
        public List<ControladorLimitador> Limitadores { get; set; }                  = null;
		public List<ControladorCargasHabilidad> CargasHabilidades { get; set; }      = null;
		public List<ControladorParticipante> Participantes { get; set; }             = null;
		
		//TODO: Ver
        public List<ControladorAmbiente> Ambientes { get; set; } = null;

        //El primer mapa siempre es el principal
		public List<ControladorMapa> Mapas { get; set; } = null;

		//Unico clima general del rol.
        public List<ControladorClimaHorario> Climas { get; set; } = null;

        #endregion

		/// <summary>
		/// Crea la clase y abre conexion con la base de datos
		/// </summary>
		/// <param name="_modeloRol"></param>
		public DatosRol(int idRol)
        {
	        BaseDeDatos = new RolContext();

			if(idRol <= 0)
				return;

			mRolSeleccionado = BaseDeDatos.Roles.FirstOrDefault(m => m.Id == idRol);

			if (mRolSeleccionado == null)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"No se encontro ningun rol con la {nameof(idRol)} especificada");
			}

			SistemaPrincipal.Atar<ModeloRol>(mRolSeleccionado);
		}

		#region Metodos

		public async Task CargarDatos()
		{
			GCLatencyMode modoOriginal = GCSettings.LatencyMode;
			GCSettings.LatencyMode = GCLatencyMode.Batch;

			await Task.Run(() =>
			{
				var masters = mRolSeleccionado.Personajes.Where(p => p.TipoPersonaje == ETipoPersonaje.Master).ToList();
				var servants = mRolSeleccionado.Personajes.Where(p => p.TipoPersonaje == ETipoPersonaje.Servant).ToList();
                var invocaciones = mRolSeleccionado.Personajes.Where(p => p.TipoPersonaje == ETipoPersonaje.Invocacion).ToList();
                var npcs = mRolSeleccionado.Personajes.Where(p => p.TipoPersonaje == ETipoPersonaje.NPC).ToList();

				//Si no hay datos creamos algunos para probar
				if (!BaseDeDatos.Combates.Any())
				{
					ModeloContrato contratoPiola = new ModeloContrato
					{
						Nombre = "PiolaContrato",
						Descripcion = "Contrato entre gente de contratos",

						EsVigente = true
					};

					contratoPiola.PersonajesAfectados.AddRange(masters);

					ModeloAlianza alianzaPiola = new ModeloAlianza
					{
						EIconoAlianza = EIconoAlianza.Team_UwU,
						FormatoImagen = EFormatoImagen.Png,

						PathImagenIcono = "Team_UwU",
						Nombre = "TeamUwU",
						Descripcion = "Alianza entre magos para ganar el grial mas facil.",

						EsVigente = true,
					};

					alianzaPiola.ContratoDeAlianza = contratoPiola;

					alianzaPiola.PersonajesAfectados.AddRange(masters);

					BaseDeDatos.Add(contratoPiola);
					BaseDeDatos.Add(alianzaPiola);

					ModeloAdministradorDeCombate combate = new ModeloAdministradorDeCombate
					{
						IndicePersonajeTurnoActual = 0,
						TurnoActual = 0,
						Nombre = "最後の戦い",
						Rol = mRolSeleccionado,
					};

					var participantes = masters.Select(m => new ModeloParticipante
					{
						CombateActual = combate,
						Personaje = m,
						AccionesRealizadas = new List<ModeloAccion>
						{
							new ModeloAccion{Descripcion = "Se tiro un backflip y se ligo a una colegiala"}
						}
					}).ToList();

					combate.Mapas.Add(mRolSeleccionado.Mapas[0]);
					combate.Participantes.AddRange(participantes);

					BaseDeDatos.Add(alianzaPiola);
					BaseDeDatos.Add(contratoPiola);
					BaseDeDatos.Add(combate);
					BaseDeDatos.AddRange(participantes);

					BaseDeDatos.SaveChanges();
                }

				SistemaPrincipal.LoggerGlobal.Log("Creando controladores...", ESeveridad.Info);

				Masters = new List<ControladorPersonaje>(masters.Count);

				for (int i = 0; i < masters.Count; ++i)
				{
					ControladorPersonaje controladorActual = new ControladorPersonaje(masters[i]);

					Masters.Add(controladorActual);
				}

				Personajes.AddRange(Masters);

				Servants = new List<ControladorPersonaje>(servants.Count);

				for (int i = 0; i < servants.Count; ++i)
				{
					ControladorPersonaje controladorActual = new ControladorPersonaje(servants[i]);

					Servants.Add(controladorActual);
				}

				Personajes.AddRange(Servants);

                Invocaciones = new List<ControladorInvocacion>(invocaciones.Count);

                for (int i = 0; i < invocaciones.Count; ++i)
                {
                    ControladorInvocacion controladorActual = new ControladorInvocacion(invocaciones[i]);

                    Invocaciones.Add(controladorActual);
                }

                Personajes.AddRange(Invocaciones);

                NPCs = new List<ControladorPersonaje>(npcs.Count);

                for (int i = 0; i < npcs.Count; ++i)
                {
                    ControladorPersonaje controladorActual = new ControladorPersonaje(npcs[i]);

                    NPCs.Add(controladorActual);
                }

                Personajes.AddRange(NPCs);

				Mapas = new List<ControladorMapa>(mRolSeleccionado.Mapas.Count);

				for (int i = 0; i < Mapas.Capacity; ++i)
				{
					ControladorMapa controladorActual = new ControladorMapa(mRolSeleccionado.Mapas[i]);

					Mapas.Add(controladorActual);
				}

				Invocaciones = new List<ControladorInvocacion>();
				
                Climas = new List<ControladorClimaHorario>{new ControladorClimaHorario(mRolSeleccionado.ClimaHorarioGlobal)};

                var participantesExistentes = mRolSeleccionado.Personajes.Select(p => p.ParticipacionEnCombates).ToList();

                Participantes = new List<ControladorParticipante>();

                foreach (var listaParticipantes in participantesExistentes)
                {
	                foreach (var participante in listaParticipantes)
	                {
						ControladorParticipante controladorActual = new ControladorParticipante(participante);

						Participantes.Add(controladorActual);
					}
                }

                CombatesActivos = new List<ControladorAdministradorDeCombate>(mRolSeleccionado.Combates.Count);

				for (int i = 0; i < CombatesActivos.Capacity; ++i)
				{
					ControladorAdministradorDeCombate controladorActual = new ControladorAdministradorDeCombate(mRolSeleccionado.Combates[i]);

					CombatesActivos.Add(controladorActual);
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
			BaseDeDatos.Dispose();

			SistemaPrincipal.Desatar<DatosRol>();
		}

		/// <summary>
		/// Actualiza el estado de los modelos en la base de datos asincronicamente
		/// </summary>
		/// <returns></returns>
		public async Task GuardarDatosAsync()
		{
			await BaseDeDatos.SaveChangesAsync();
		}

		/// <summary>
		/// Actualiza el estado de los modelos en la base de datos
		/// </summary>
		public void GuardarDatos()
		{
			BaseDeDatos.SaveChanges();
		}

		/// <summary>
		/// Guarda un nuevo modelo en la base de datos
		/// </summary>
		/// <param name="modelo">Modelo que guardar</param>
		public void GuardarModelo(ModeloBaseSK modelo)
		{
			BaseDeDatos.Add(modelo);
		}

		/// <summary>
		/// Guarda un nuevo modelo en la base de datos de manera asincronica
		/// </summary>
		/// <param name="modelo">Modelo que guardar</param>
		public async Task GuardarModeloAsync(ModeloBaseSK modelo)
		{
			await BaseDeDatos.AddAsync(modelo);
		}

		/// <summary>
		/// Elimina un modelo de la base de datos
		/// </summary>
		/// <param name="modelo">Modelo que eliminar</param>
		public void EliminarModelo(ModeloBaseSK modelo)
		{
			BaseDeDatos.Remove(modelo);
		}
	} 

	#endregion
}