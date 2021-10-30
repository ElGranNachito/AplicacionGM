﻿using Microsoft.EntityFrameworkCore;

namespace AppGM.Core
{
    public class RolContext : DbContext
    {
	    #region Propiedades

        // ------------------------------------MODELOS------------------------------------

        /// <summary>
        /// Roles existentes
        /// </summary>
        public DbSet<ModeloRol> Rols { get; set; }

        /// <summary>
        /// Personajes existentes
        /// </summary>
        public DbSet<ModeloPersonaje> Personajes { get; set; }

        /// <summary>
        /// Servants existentes
        /// </summary>
        public DbSet<ModeloServant> Servants { get; set; }

        /// <summary>
        /// Masters existentes
        /// </summary>
        public DbSet<ModeloMaster> Masters { get; set; }

        /// <summary>
        /// Invocaciones existentes
        /// </summary>
        public DbSet<ModeloInvocacion> Invocaciones { get; set; }

        /// <summary>
        /// Combates existentes
        /// </summary>
        public DbSet<ModeloAdministradorDeCombate> Combates { get; set; }

        /// <summary>
        /// Participantes existentes
        /// </summary>
        public DbSet<ModeloParticipante> Participantes { get; set; }

        /// <summary>
        /// Vectores existentes
        /// </summary>
        public DbSet<ModeloVector2> Vectores2 { get; set; }

        /// <summary>
        /// Mapas existentes
        /// </summary>
        public DbSet<ModeloMapa> Mapas { get; set; }

        /// <summary>
        /// Unidades en mapas
        /// </summary>
        public DbSet<ModeloUnidadMapa> UnidadesMapa { get; set; }

        /// <summary>
        /// Acciones de tomadas por personajes
        /// </summary>
        public DbSet<ModeloAccion> Acciones { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public RolContext() { }

        #endregion

        #region Configuracion de la base de datos

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = DbAppGm.db");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// TODO: Agregar muchas cosas :u

			#region Personajes

			// Persoanjes:
			modelBuilder.Entity<ModeloPersonaje>().ToTable("ModeloPersonaje")
				.HasDiscriminator<int>("Clase")
				.HasValue<ModeloPersonajeJugable>(1)
				.HasValue<ModeloInvocacion>(2)
				.HasValue<ModeloPersonaje>(3)
				.HasValue<ModeloInvocacionCondicionada>(4)
				.HasValue<ModeloInvocacionTemporal>(5)
				.HasValue<ModeloMaster>(6)
				.HasValue<ModeloServant>(7);

			// - Personaje unidad mapa
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.UnidadesEnMapa)
				.WithOne(u => u.Personaje)
				.OnDelete(DeleteBehavior.Cascade);

			// - Personaje efectos
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.EfectosAplicandose)
				.WithOne(e => e.Objetivo)
				.OnDelete(DeleteBehavior.Cascade);

			// - Personaje utilizables
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Inventario)
				.WithOne(u => u.PersonajePortador)
				.OnDelete(DeleteBehavior.Cascade);

			// - Personaje defensivos
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Armadura);

			// - Personaje arma a distancia
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.ArmasDistancia);

			// - Personaje contratos
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Contratos)
				.WithMany(c => c.PersonajesAfectados);

			// - Personaje alianzas
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Alianzas)
				.WithMany(a => a.PersonajesAfectados);

			// - Personaje perks
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Perks)
				.WithOne(p => p.Dueño)
				.OnDelete(DeleteBehavior.Cascade);

			// - Personaje skills
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Skills);

			// - Personaje magias
			modelBuilder.Entity<ModeloPersonaje>()
				.HasMany(p => p.Magias);

			// - Personaje especialidades
			modelBuilder.Entity<ModeloEspecialidad>().ToTable("ModeloEspecialidad");

			modelBuilder.Entity<ModeloEspecialidad>()
				.HasOne(e => e.PersonajeContenedor)
				.WithMany(p => p.Especialidades)
				.OnDelete(DeleteBehavior.Cascade);

			// Servant noble phantasm:
			modelBuilder.Entity<ModeloServant>()
				.HasMany(p => p.NoblePhantasms);

			// - PersonajeJugable caracteristicas
			modelBuilder.Entity<ModeloPersonajeJugable>()
				.HasOne(p => p.Caracteristicas)
				.WithOne()
				.OnDelete(DeleteBehavior.SetNull);

			// - Invocacion personaje
			modelBuilder.Entity<ModeloInvocacion>()
				.HasOne(i => i.Invocador)
				.WithMany(i => i.Invocaciones)
				.OnDelete(DeleteBehavior.SetNull);

			// - Invocacion datos invocacion
			modelBuilder.Entity<ModeloInvocacion>()
				.HasOne(i => i.DatosInvocacion)
				.WithOne(d => d.Invocacion)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Participante combate

			// Participante:

			modelBuilder.Entity<ModeloParticipante>().ToTable("ModeloParticipante").HasNoDiscriminator();

			modelBuilder.Entity<ModeloParticipante>()
				.HasOne(p => p.Personaje)
				.WithMany(p => p.ParticipacionEnCombates)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ModeloParticipante>()
				.HasMany(p => p.AccionesRealizadas)
				.WithOne(a => a.Participante)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Efectos

			// Efectos:

			modelBuilder.Entity<ModeloEfecto>().ToTable("ModeloEfecto").HasNoDiscriminator();

			// Efectos siendo aplicados:

			modelBuilder.Entity<ModeloEfectoSiendoAplicado>().ToTable("ModeloEfectoSiendoAplicado").HasNoDiscriminator();

			// - Efecto siendo aplicado personaje objetivo
			modelBuilder.Entity<ModeloEfectoSiendoAplicado>()
				.HasOne(e => e.Objetivo)
				.WithMany(o => o.EfectosAplicandose)
				.OnDelete(DeleteBehavior.Cascade);

			// - Efecto siendo aplicado personaje instigador
			modelBuilder.Entity<ModeloEfectoSiendoAplicado>()
				.HasOne(e => e.Instigador);

			// - Efecto siendo aplicado, efecto base
			modelBuilder.Entity<ModeloEfectoSiendoAplicado>()
				.HasOne(e => e.Efecto)
				.WithMany(e => e.Aplicaciones)
				.OnDelete(DeleteBehavior.Cascade);

			// - Efecto siendo aplicado funcion
			modelBuilder.Entity<TIEfectoSiendoAplicadoFuncion>().HasKey(ti => new { ti.IdFuncion, ti.IdEfectoSiendoAplicado });

			modelBuilder.Entity<TIEfectoSiendoAplicadoFuncion>()
				.HasOne(e => e.EfectoAplicandose)
				.WithMany(e => e.Funciones)
				.HasForeignKey(e => e.IdEfectoSiendoAplicado);

			modelBuilder.Entity<TIEfectoSiendoAplicadoFuncion>()
				.HasOne(e => e.ModeloFuncion);

			#endregion

			#region Alianzas

			// Alianzas:
			modelBuilder.Entity<ModeloAlianza>().ToTable("ModeloAlianza").HasNoDiscriminator();

			modelBuilder.Entity<ModeloAlianza>()
				.HasMany(a => a.PersonajesAfectados)
				.WithMany(p => p.Alianzas);

			modelBuilder.Entity<ModeloAlianza>()
				.HasOne(a => a.ContratoDeAlianza)
				.WithOne(c => c.Alianza)
				.OnDelete(DeleteBehavior.SetNull);

			#endregion

			#region Contrato

			modelBuilder.Entity<ModeloContrato>().ToTable("ModeloContrato");

			modelBuilder.Entity<ModeloContrato>()
				.HasOne(c => c.Alianza)
				.WithOne(a => a.ContratoDeAlianza)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ModeloContrato>()
				.HasMany(c => c.PersonajesAfectados)
				.WithMany(p => p.Contratos);

			#endregion	

			#region Slot

			// Slot item: 

			modelBuilder.Entity<ModeloSlot>().ToTable("ModeloSlot").HasNoDiscriminator();

			// - Slot item
			modelBuilder.Entity<ModeloSlot>()
				.HasMany(s => s.ItemsAlmacenados);

			#endregion

			#region Utilizables

			// Utilizables:
			modelBuilder.Entity<ModeloUtilizable>().ToTable("ModeloUtilizable")
				.HasDiscriminator<int>("Tipo")
				.HasValue<ModeloUtilizable>(1)
				.HasValue<ModeloPortable>(2)
				.HasValue<ModeloItem>(3)
				.HasValue<ModeloConsumible>(4)
				.HasValue<ModeloArmasDistancia>(5)
				.HasValue<ModeloDefensivo>(6);

			// - Portable slots
			modelBuilder.Entity<ModeloPortable>()
				.HasMany(p => p.Slots)
				.WithOne(s => s.Dueño)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Habilidad

			// Habilidades: 
			modelBuilder.Entity<ModeloHabilidad>().ToTable("ModeloHabilidad")
				.HasDiscriminator<int>("Tipo")
				.HasValue<ModeloHabilidad>(1)
				.HasValue<ModeloPerk>(2)
				.HasValue<ModeloMagia>(3)
				.HasValue<ModeloNoblePhantasm>(4);

			// - Habilidad tiradas
			modelBuilder.Entity<ModeloHabilidad>()
				.HasMany(h => h.Tiradas)
				.WithOne(t => t.HabilidadContenedora)
				.OnDelete(DeleteBehavior.Cascade);

			// - Habilidad dueño
			modelBuilder.Entity<ModeloHabilidad>()
				.HasOne(h => h.Dueño)
				.WithMany(p => p.Habilidades)
				.OnDelete(DeleteBehavior.Cascade);

			// - Habilidad efectos
			modelBuilder.Entity<ModeloHabilidad>()
				.HasMany(h => h.Efectos)
				.WithOne(e => e.HabilidadContenedora)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Administrador de combate

			//Combates:

			//  - Administrador de combate participantes:
			modelBuilder.Entity<ModeloAdministradorDeCombate>()
				.HasMany(a => a.Participantes)
				.WithOne(p => p.CombateActual)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<ModeloAdministradorDeCombate>()
				.HasOne(a => a.AmbienteDelCombate)
				.WithOne()
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ModeloAdministradorDeCombate>()
				.HasMany(a => a.Mapas)
				.WithOne(m => m.CombateAlQuePertenece)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Ambiente

			//Ambiente:
			modelBuilder.Entity<ModeloAmbiente>().ToTable("ModeloAmbiente").HasNoDiscriminator();

			modelBuilder.Entity<ModeloAmbiente>()
				.HasOne(a => a.MapaDelAmbiente)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ModeloAmbiente>()
				.HasOne(a => a.CombateAlQuePertenece)
				.WithOne(c => c.AmbienteDelCombate)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Clima

            //Clima:
            modelBuilder.Entity<ModeloClimaHorario>().ToTable("ModeloClimaHorario").HasNoDiscriminator();

			#endregion

			#region Mapa

			//Mapa:
			modelBuilder.Entity<ModeloMapa>().ToTable("ModeloMapa").HasNoDiscriminator();

			modelBuilder.Entity<ModeloMapa>()
				.HasOne(m => m.Ambiente)
				.WithOne(a => a.MapaDelAmbiente)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ModeloMapa>()
				.HasMany(m => m.PosicionesUnidades)
				.WithOne(u => u.Mapa)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Unidad mapa

			// Unidad mapa:

			modelBuilder.Entity<ModeloUnidadMapa>()
				.HasDiscriminator<int>("Tipo")
				.HasValue<ModeloUnidadMapa>(1)
				.HasValue<ModeloUnidadMapaMasterServant>(2)
				.HasValue<ModeloUnidadMapaInvocacionTrampa>(3);

			// - Unidad mapa vector2
			modelBuilder.Entity<ModeloUnidadMapa>()
				.HasOne(u => u.Posicion);

			#endregion

			#region Rol

			//Rol:

			modelBuilder.Entity<ModeloRol>().ToTable("ModeloRol").HasNoDiscriminator();

			// - Rol personaje
			modelBuilder.Entity<ModeloRol>()
				.HasMany(r => r.Personajes)
				.WithOne(p => p.Rol)
				.OnDelete(DeleteBehavior.Cascade);

			// - Rol combate
			modelBuilder.Entity<ModeloRol>()
				.HasMany(r => r.Combates)
				.WithOne(p => p.Rol)
				.OnDelete(DeleteBehavior.Cascade);

			// - Rol mapa
			modelBuilder.Entity<ModeloRol>()
				.HasMany(r => r.Mapas)
				.WithOne(p => p.Rol)
				.OnDelete(DeleteBehavior.Cascade);

			// - Rol ambiente
			modelBuilder.Entity<ModeloRol>()
				.HasOne(r => r.AmbienteGlobal)
				.WithOne(p => p.RolAlQuePertenece)
				.OnDelete(DeleteBehavior.SetNull);

            // - Rol clima
            modelBuilder.Entity<ModeloRol>()
                .HasOne(r => r.ClimaHorarioGlobal)
                .WithOne(p => p.RolAlQuePertenece)
                .OnDelete(DeleteBehavior.SetNull);

			#endregion

			#region Funcion

			// Funcion:
			modelBuilder.Entity<ModeloFuncion>().ToTable("ModeloFuncion");

			//Funcion - Padre
			modelBuilder.Entity<TIFuncionPadreFuncion>()
				.HasKey(e => new { e.IDPadre, e.IDFuncion });

			modelBuilder.Entity<TIFuncionPadreFuncion>()
				.HasOne(e => e.Funcion)
				.WithOne(f => f.Padre)
				.HasForeignKey<TIFuncionPadreFuncion>(e => e.IDFuncion);

			modelBuilder.Entity<TIFuncionPadreFuncion>()
				.HasOne(e => e.Padre)
				.WithMany(p => p.Hijos);

			//Funcion - Habilidad
			modelBuilder.Entity<TIFuncionHabilidad>()
				.HasKey(e => new { e.IDFuncion, e.IDHabilidad });

			modelBuilder.Entity<TIFuncionHabilidad>()
				.HasOne(e => e.Funcion)
				.WithOne(f => f.HabilidadContenedora)
				.HasForeignKey<TIFuncionHabilidad>(e => e.IDFuncion);

			modelBuilder.Entity<TIFuncionHabilidad>()
				.HasOne(e => e.Habilidad)
				.WithMany(f => f.Funciones);

			//Funcion - Efecto
			modelBuilder.Entity<TIFuncionEfecto>()
				.HasKey(e => new { e.IDFuncion, e.IDEfecto });

			modelBuilder.Entity<TIFuncionEfecto>()
				.HasOne(e => e.Funcion)
				.WithOne(e => e.EfectoContenedor)
				.HasForeignKey<TIFuncionEfecto>(e => e.IDFuncion);

			modelBuilder.Entity<TIFuncionEfecto>()
				.HasOne(e => e.Efecto)
				.WithMany(e => e.Funciones);


			//Funcion handler evento
			modelBuilder.Entity<ModeloFuncion_HandlerEvento>();

			//Funcion handler evento - Personaje
			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloPersonaje>>().HasKey(e => new { e.IdFuncion, e.IdOtro });

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloPersonaje>>()
				.HasOne(e => e.Otro)
				.WithMany(p => p.HandlersEventos)
				.HasForeignKey(e => e.IdOtro);

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloPersonaje>>()
				.HasOne(e => e.Funcion)
				.WithMany(f => f.EventosEnPersonaje)
				.HasForeignKey(e => e.IdFuncion);

			//Funcion handler evento - Habilidad
			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloHabilidad>>().HasKey(e => new { e.IdFuncion, e.IdOtro });

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloHabilidad>>()
				.HasOne(e => e.Otro)
				.WithMany(h => h.HandlersEventos)
				.HasForeignKey(e => e.IdOtro);

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloHabilidad>>()
				.HasOne(e => e.Funcion)
				.WithMany(f => f.EventosEnHabilidad)
				.HasForeignKey(e => e.IdFuncion);

			//Funcion handler evento - Efecto
			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloEfecto>>().HasKey(e => new { e.IdFuncion, e.IdOtro });

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloEfecto>>()
				.HasOne(e => e.Otro)
				.WithMany(e => e.HandlersEventos)
				.HasForeignKey(e => e.IdOtro);

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloEfecto>>()
				.HasOne(e => e.Funcion)
				.WithMany(f => f.EventosEnEfecto)
				.HasForeignKey(e => e.IdFuncion);

			//Funcion handler evento - Utilizable
			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloUtilizable>>().HasKey(e => new { e.IdFuncion, e.IdOtro });

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloUtilizable>>()
				.HasOne(e => e.Otro)
				.WithMany(u => u.HandlersEventos)
				.HasForeignKey(e => e.IdOtro);

			modelBuilder.Entity<TIFuncionHandlerEvento<ModeloUtilizable>>()
				.HasOne(e => e.Funcion)
				.WithMany(f => f.EventosEnUtilizable)
				.HasForeignKey(e => e.IdFuncion);

			#endregion

			#region Tirada

			modelBuilder.Entity<ModeloTiradaBase>().ToTable("Tirada")
				.HasDiscriminator<int>("Tipo")
				.HasValue<ModeloTiradaVariable>(1)
				.HasValue<ModeloTiradaStat>(2)
				.HasValue<ModeloTiradaDeDaño>(3);

			//Tirada - Personaje
			modelBuilder.Entity<ModeloTiradaBase>()
				.HasOne(t => t.PersonajeContenedor)
				.WithMany(p => p.Tiradas)
				.OnDelete(DeleteBehavior.Cascade);

			//Tirada - Habilidad
			modelBuilder.Entity<ModeloTiradaBase>()
				.HasOne(t => t.HabilidadContenedora)
				.WithMany(p => p.Tiradas)
				.OnDelete(DeleteBehavior.Cascade);

			//Tirada - Utilizable
			modelBuilder.Entity<ModeloTiradaBase>()
				.HasOne(t => t.UtilizableContenedor)
				.WithMany(p => p.Tiradas)
				.OnDelete(DeleteBehavior.Cascade);

			//Tirada - Funcion
			modelBuilder.Entity<ModeloTiradaBase>()
				.HasOne(t => t.FuncionContenedora)
				.WithMany(p => p.Tiradas)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion

			#region Variable

			//Variable
			modelBuilder.Entity<ModeloVariableBase>().ToTable("ModeloVariable").HasDiscriminator<int>("Tipo")
				.HasValue(typeof(ModeloVariableInt), 1)
				.HasValue(typeof(ModeloVariableFloat), 2)
				.HasValue(typeof(ModeloVariableString), 3);

			//Variable - Personaje
			modelBuilder.Entity<ModeloVariableBase>()
				.HasOne(v => v.PersonajeContenedor)
				.WithMany(p => p.Variables)
				.OnDelete(DeleteBehavior.Cascade);

			//Variable - Habilidad
			modelBuilder.Entity<ModeloVariableBase>()
				.HasOne(v => v.HabilidadContenedora)
				.WithMany(p => p.Variables)
				.OnDelete(DeleteBehavior.Cascade);

			//Variable - Utilizable
			modelBuilder.Entity<ModeloVariableBase>()
				.HasOne(v => v.UtilizableContenedor)
				.WithMany(p => p.Variables)
				.OnDelete(DeleteBehavior.Cascade);

			//Variable - Funcion
			modelBuilder.Entity<ModeloVariableBase>()
				.HasOne(v => v.FuncionContenedora)
				.WithMany(p => p.Variables)
				.OnDelete(DeleteBehavior.Cascade);

			#endregion
		}
        #endregion
    }
}