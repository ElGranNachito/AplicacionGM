using Microsoft.EntityFrameworkCore;

namespace AppGM.Core
{
    public class RolContext : DbContext
    {
        #region Miembros

        private string mNombreRolSeleccionado;

        #endregion

        #region Propiedades
        // Modelos ---
        public DbSet<ModeloRol> Rols { get; set; }

        public DbSet<ModeloPersonaje> Personajes { get; set; }
        public DbSet<ModeloServant> Servants { get; set; }
        public DbSet<ModeloMaster> Masters { get; set; }
        public DbSet<ModeloInvocacion> Invocaciones { get; set; }



        // Relaciones ---
        public DbSet<TIPersonajeEfecto> PersonajeEfectos { get; set; }
        public DbSet<TIPersonajeUtilizable> PersonajeUtilizables { get; set; }
        public DbSet<TIPersonajePortable> PersonajePortables { get; set; }
        public DbSet<TIPersonajeDefensivo> PersonajeDefensivos { get; set; }
        public DbSet<TIPersonajeDefensivoAbsoluto> PersonajeDefensivosAbsolutos { get; set; }
        public DbSet<TIPersonajeConsumible> PersonajeConsumibles { get; set; }
        public DbSet<TIPersonajeArmaDistancia> PersonajeArmasDistancias { get; set; }
        public DbSet<TIPersonajePersonaje> PersonajeAliados { get; set; }
        public DbSet<TIPersonajePerk> PersonajePerks { get; set; }
        public DbSet<TIPersonajeHabilidad> PersonajeSkills { get; set; }
        public DbSet<TIPersonajeMagia> PersonajeMagias { get; set; }
        public DbSet<TIPersonajeModificadorDeDefensa> PersonajeModificadoresDeDefensa { get; set; }

        public DbSet<TIServantNoblePhantasm> ServantNoblePhantasms { get; set; }
        #endregion

        #region Configuracion de la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source = Db{mNombreRolSeleccionado}.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Agregar muchas cosas :u

            // Persoanjes:
            modelBuilder.Entity<ModeloPersonaje>().ToTable("ModeloPersonaje")
                .HasDiscriminator<int>("Clase")
                .HasValue<ModeloPersonajeJugable>(1)
                .HasValue<ModeloInvocacion>(2)
                .HasValue<ModeloPersonaje>(3);

            // - Personaje efectos
            modelBuilder.Entity<TIPersonajeEfecto>().HasKey(e => new { e.IdPersonaje, e.IdEfecto });

            modelBuilder.Entity<TIPersonajeEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIPersonajeEfecto>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Efectos)
                .HasForeignKey(ip => ip.IdPersonaje);

            // - Personaje utilizables
            modelBuilder.Entity<TIPersonajeUtilizable>().HasKey(e => new { e.IdPersonaje, e.IdUtilizable });

            modelBuilder.Entity<TIPersonajeUtilizable>()
                .HasOne(i => i.Utilizable);

            modelBuilder.Entity<TIPersonajeUtilizable>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Inventario)
                .HasForeignKey(ip => ip.IdPersonaje);

            // - Personaje defensivos
            modelBuilder.Entity<TIPersonajeDefensivo>().HasKey(e => new { e.IdPersonaje, e.IdDefensivo });

            modelBuilder.Entity<TIPersonajeDefensivo>()
                .HasOne(i => i.Defensivo);

            modelBuilder.Entity<TIPersonajeDefensivo>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Armadura)
                .HasForeignKey(ip => ip.IdPersonaje);

            // - Personaje aliados
            modelBuilder.Entity<TIPersonajePersonaje>().HasKey(e => new { e.IdPersonaje, e.IdAliado });

            modelBuilder.Entity<TIPersonajePersonaje>()
                .HasOne(i => i.Aliado);

            modelBuilder.Entity<TIPersonajePersonaje>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Aliados)
                .HasForeignKey(ip => ip.IdPersonaje);

            // - Personaje perks
            modelBuilder.Entity<TIPersonajePerk>().HasKey(e => new { e.IdPersonaje, e.IdPerk });

            modelBuilder.Entity<TIPersonajePerk>()
                .HasOne(i => i.Perk);

            modelBuilder.Entity<TIPersonajePerk>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Perks)
                .HasForeignKey(ip => ip.IdPersonaje);


            // - Personaje skills
            modelBuilder.Entity<TIPersonajeHabilidad>().HasKey(e => new { e.IdPersonaje, e.IdHabilidad });

            modelBuilder.Entity<TIPersonajeHabilidad>()
                .HasOne(i => i.Habilidad);

            modelBuilder.Entity<TIPersonajeHabilidad>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Skills)
                .HasForeignKey(ip => ip.IdPersonaje);


            // - Personaje magias
            modelBuilder.Entity<TIPersonajeMagia>().HasKey(e => new { e.IdPersonaje, e.IdMagia });

            modelBuilder.Entity<TIPersonajeMagia>()
                .HasOne(i => i.Magia);

            modelBuilder.Entity<TIPersonajeMagia>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Magias)
                .HasForeignKey(ip => ip.IdPersonaje);


            // - Personaje modificador de defensa
            modelBuilder.Entity<TIPersonajeModificadorDeDefensa>().HasKey(e => new { e.IdPersonaje, e.IdModificadorDefensa });

            modelBuilder.Entity<TIPersonajeModificadorDeDefensa>()
                .HasOne(i => i.ModificadorDeDefensa);

            modelBuilder.Entity<TIPersonajeModificadorDeDefensa>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.ModificadoresDeDefensa)
                .HasForeignKey(ip => ip.IdPersonaje);

            // Servant noble phantasm:
            modelBuilder.Entity<TIServantNoblePhantasm>().HasKey(e => new { e.IdServant, e.IdNoblePhantasm });

            modelBuilder.Entity<TIServantNoblePhantasm>()
                .HasOne(i => i.NoblePhantasm);

            modelBuilder.Entity<TIServantNoblePhantasm>()
                .HasOne(i => i.Servant)
                .WithMany(p => p.NoblePhantasms)
                .HasForeignKey(ip => ip.IdServant);

            // Personajes jugables:
            modelBuilder.Entity<ModeloPersonajeJugable>().ToTable("ModeloPersonajeJugable")
                .HasDiscriminator<int>("Clase")
                .HasValue<ModeloMaster>(1)
                .HasValue<ModeloServant>(2);

            // - PersonajeJugable caracteristicas
            modelBuilder.Entity<TIPersonajeJugableCaracteristicas>().HasKey(e => new { e.IdPersonajeJugable, e.IdCaracteristica });

            modelBuilder.Entity<TIPersonajeJugableCaracteristicas>()
                .HasOne(i => i.Caracteristicas);

            modelBuilder.Entity<TIPersonajeJugableCaracteristicas>()
                .HasOne(i => i.PersonajeJugable)
                .WithOne(p => p.Caracteristicas);

            // - PersonajeJugable invocaciones
            modelBuilder.Entity<TIPersonajeJugableInvocacion>().HasKey(e => new { e.IdPersonajeJugable, e.IdInvocacion });

            modelBuilder.Entity<TIPersonajeJugableInvocacion>()
                .HasOne(i => i.Invocacion);

            modelBuilder.Entity<TIPersonajeJugableInvocacion>()
                .HasOne(i => i.PersonajeJugable)
                .WithMany(p => p.Invocaciones)
                .HasForeignKey(ip => ip.IdPersonajeJugable);

            // Invocaciones:
            modelBuilder.Entity<ModeloInvocacion>().ToTable("ModeloInvocacion")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloInvocacionTemporal>(1)
                .HasValue<ModeloInvocacionCondicionada>(2);

            // - Invocacion personaje
            modelBuilder.Entity<TIInvocacionPersonaje>().HasKey(e => new { e.IdInvocacion, e.IdPersonaje });

            modelBuilder.Entity<TIInvocacionPersonaje>()
                .HasOne(i => i.PersonajeInvocador);

            modelBuilder.Entity<TIInvocacionPersonaje>()
                .HasOne(i => i.Invocacion)
                .WithOne(p => p.Invocador);

            // - Invocacion efecto
            modelBuilder.Entity<TIInvocacionEfecto>().HasKey(e => new { e.IdInvocacion, e.IdEfecto });

            modelBuilder.Entity<TIInvocacionEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIInvocacionEfecto>()
                .HasOne(i => i.Invocacion)
                .WithOne(p => p.Efecto);

            // Participante:
            // - Participante personaje
            modelBuilder.Entity<TIParticipantePersonaje>().HasKey(e => new { e.IdParticipante, e.IdPersonaje });

            modelBuilder.Entity<TIParticipantePersonaje>()
                .HasOne(i => i.Personaje);

            modelBuilder.Entity<TIParticipantePersonaje>()
                .HasOne(i => i.Participante)
                .WithOne(p => p.Personaje);

            // - Participante accion
            modelBuilder.Entity<TIParticipanteAccion>().HasKey(e => new { e.IdParticipante, e.IdAccion });

            modelBuilder.Entity<TIParticipanteAccion>()
                .HasOne(i => i.Accion);

            modelBuilder.Entity<TIParticipanteAccion>()
                .HasOne(i => i.Participante)
                .WithMany(p => p.AccionesRealizadas)
                .HasForeignKey(ip => ip.IdParticipante);

            // Administrador de combate participantes:
            modelBuilder.Entity<TIAdministradorDeCombateParticipante>().HasKey(e => new { e.IdAdministradorDeCombate, e.IdParticipante });

            modelBuilder.Entity<TIAdministradorDeCombateParticipante>()
                .HasOne(i => i.Participante);

            modelBuilder.Entity<TIAdministradorDeCombateParticipante>()
                .HasOne(i => i.AdministradorDeCombate)
                .WithMany(p => p.Participantes)
                .HasForeignKey(ip => ip.IdAdministradorDeCombate);

            // Efectos:
            modelBuilder.Entity<ModeloEfecto>().ToTable("ModeloEfecto")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloEfecto>(1)
                .HasValue<ModeloEfectoTemporal>(2);

            modelBuilder.Entity<TIEfectoModificadorDeStatBase>().HasKey(e => new { e.IdEfecto, e.IdModificadorDeStat });

            modelBuilder.Entity<TIEfectoModificadorDeStatBase>()
                .HasOne(i => i.Modificador);

            modelBuilder.Entity<TIEfectoModificadorDeStatBase>()
                .HasOne(i => i.Efecto)
                .WithMany(p => p.Modificaciones)
                .HasForeignKey(ip => ip.IdEfecto);

            // Slot item: 
            modelBuilder.Entity<TISlotItem>().HasKey(e => new { e.IdSlot, e.IdItem });

            modelBuilder.Entity<TISlotItem>()
                .HasOne(i => i.Item);

            modelBuilder.Entity<TISlotItem>()
                .HasOne(i => i.Slot)
                .WithMany(p => p.ItemsAlmacenados)
                .HasForeignKey(ip => ip.IdSlot);

            // Utilizables:
            modelBuilder.Entity<ModeloUtilizable>().ToTable("ModeloUtilizable")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloItem>(1)
                .HasValue<ModeloPortable>(2);

            // - Utilizable tirada base
            modelBuilder.Entity<TIUtilizableTiradaBase>().HasKey(e => new { e.IdUtilizable, e.IdTirada });

            modelBuilder.Entity<TIUtilizableTiradaBase>()
                .HasOne(i => i.TiradaBase);

            modelBuilder.Entity<TIUtilizableTiradaBase>()
                .HasOne(i => i.Utilizable)
                .WithOne(p => p.TiradaDeUso);

            // - Utilizable modificador de stat base
            modelBuilder.Entity<TIUtilizableModificadorDeStatBase>().HasKey(e => new { e.IdUtilizable, e.IdModificadorStatBase });

            modelBuilder.Entity<TIUtilizableModificadorDeStatBase>()
                .HasOne(i => i.ModificadorDeStatBase);

            modelBuilder.Entity<TIUtilizableModificadorDeStatBase>()
                .HasOne(i => i.Utilizable)
                .WithOne(p => p.VentajaAlUtilizarlo);

            // - Utilizable efecto
            modelBuilder.Entity<TIUtilizableEfecto>().HasKey(e => new { e.IdUtilizable, e.IdEfecto });

            modelBuilder.Entity<TIUtilizableEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIUtilizableEfecto>()
                .HasOne(i => i.Utilizable)
                .WithOne(p => p.EfectoSobreElUsuario);

            modelBuilder.Entity<TIUtilizableEfecto>()
                .HasOne(i => i.Utilizable)
                .WithOne(p => p.EfectoSobreElObjetivo);

            // Utilizable portable:
            modelBuilder.Entity<ModeloPortable>().ToTable("ModeloPortable")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloDefensivo>(1)
                .HasValue<ModeloDefensivoAbsoluto>(2)
                .HasValue<ModeloOfensivo>(3);

            // - Portable slots
            modelBuilder.Entity<TIPortableSlots>().HasKey(e => new { e.IdPortable, e.IdSlot });

            modelBuilder.Entity<TIPortableSlots>()
                .HasOne(i => i.Slot);

            modelBuilder.Entity<TIPortableSlots>()
                .HasOne(i => i.Portable)
                .WithMany(p => p.Slots)
                .HasForeignKey(ip => ip.IdPortable);

            // - Portable modificador de stat base
            modelBuilder.Entity<TIPortableModificadorDeStatBase>().HasKey(e => new { e.IdPortable, e.IdModificadorDeStat });

            modelBuilder.Entity<TIPortableModificadorDeStatBase>()
                .HasOne(i => i.Modificador);

            modelBuilder.Entity<TIPortableModificadorDeStatBase>()
                .HasOne(i => i.Portable)
                .WithOne(p => p.DesventajasDeEquiparlo);

            modelBuilder.Entity<TIPortableModificadorDeStatBase>()
                .HasOne(i => i.Portable)
                .WithOne(p => p.VentajasDeQuiparlo);

            // - Portable-Ofensivo tirada de daño
            modelBuilder.Entity<TIOfensivoTiradaDeDaño>().HasKey(e => new { e.IdOfensivo, e.IdTiradaDeDaño });

            modelBuilder.Entity<TIOfensivoTiradaDeDaño>()
                .HasOne(i => i.TiradaDeDaño);

            modelBuilder.Entity<TIOfensivoTiradaDeDaño>()
                .HasOne(i => i.Ofensivo)
                .WithMany(p => p.TiradasDeDaño)
                .HasForeignKey(ip => ip.IdOfensivo);

            // - Portable-Ofensivo efecto
            modelBuilder.Entity<TIOfensivoEfecto>().HasKey(e => new { e.IdOfensivo, e.IdEfecto });

            modelBuilder.Entity<TIOfensivoEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIOfensivoEfecto>()
                .HasOne(i => i.Ofensivo)
                .WithOne(p => p.EfectoQueInflige);


            // Utilizable item (consumibles):
            modelBuilder.Entity<ModeloItem>().ToTable("ModeloItem")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloConsumible>(1)
                .HasValue<ModeloArmasDistancia>(2);

            // - Armas distancia tirada de daño
            modelBuilder.Entity<TIArmasDistanciaTiradaDeDaño>().HasKey(e => new { e.IdArmasDistancia, e.IdTirada });

            modelBuilder.Entity<TIArmasDistanciaTiradaDeDaño>()
                .HasOne(i => i.TiradaDaño);

            modelBuilder.Entity<TIArmasDistanciaTiradaDeDaño>()
                .HasOne(i => i.ArmasDistancia)
                .WithOne(p => p.TiradaDeDaño);

            // - Armas distancia tirada variable
            modelBuilder.Entity<TIArmasDistanciaTiradaVariable>().HasKey(e => new { e.IdArmasDistancia, e.IdTirada });

            modelBuilder.Entity<TIArmasDistanciaTiradaVariable>()
                .HasOne(i => i.TiradaVariable);

            modelBuilder.Entity<TIArmasDistanciaTiradaVariable>()
                .HasOne(i => i.ArmasDistancia)
                .WithOne(p => p.TiradaRafaga);

            // - Armas distancia efecto
            modelBuilder.Entity<TIArmasDistanciaEfecto>().HasKey(e => new { e.IdArmasDistancia, e.IdEfecto });

            modelBuilder.Entity<TIArmasDistanciaEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIArmasDistanciaEfecto>()
                .HasOne(i => i.ArmasDistancia)
                .WithMany(p => p.EfectoQueInflige)
                .HasForeignKey(ip => ip.IdArmasDistancia);

            // Modificadores:

            modelBuilder.Entity<ModeloModificadorDeStatBase>().ToTable("ModeloModificadorDeStatBase")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloModificadorDeStatBase>(1)
                .HasValue<ModeloModificadorDeStatPrimitivos>(2)
                .HasValue<ModeloModificadorDeStatClase>(3)
                .HasValue<ModeloModificadorDeDefensa>(4);

            // - Modificador stat base tirada base
            modelBuilder.Entity<TIModificadorDeStatBaseTiradaBase>().HasKey(e => new { e.IdModificadorDeStatBase, e.IdTirada });

            modelBuilder.Entity<TIModificadorDeStatBaseTiradaBase>()
                .HasOne(i => i.TiradaBase);

            modelBuilder.Entity<TIModificadorDeStatBaseTiradaBase>()
                .HasOne(i => i.ModificadorDeStatBase)
                .WithOne(p => p.ValorTirada);

            // Habilidades: 

            modelBuilder.Entity<ModeloHabilidad>().ToTable("ModeloHabilidad")
                .HasDiscriminator<int>("Tipo")
                .HasValue<ModeloHabilidad>(1)
                .HasValue<ModeloPerk>(2)
                .HasValue<ModeloMagia>(3)
                .HasValue<ModeloNoblePhantasm>(4);

            // - Habilidad limitador
            modelBuilder.Entity<TIHabilidadLimitador>().HasKey(e => new { e.IdHabilidad, e.IdLimitador });

            modelBuilder.Entity<TIHabilidadLimitador>()
                .HasOne(i => i.ModeloLimitador);

            modelBuilder.Entity<TIHabilidadLimitador>()
                .HasOne(i => i.Habilidad)
                .WithOne(p => p.LimiteDeUsos);

            // - Habilidad cargas habilidad
            modelBuilder.Entity<TIHabilidadCargasHabilidad>().HasKey(e => new { e.IdHabilidad, e.IdCargasHabilidad });

            modelBuilder.Entity<TIHabilidadCargasHabilidad>()
                .HasOne(i => i.ModeloCargasHabilidad);

            modelBuilder.Entity<TIHabilidadCargasHabilidad>()
                .HasOne(i => i.Habilidad)
                .WithOne(p => p.CargasHabilidad);

            // - Habilidad tirada de daño
            modelBuilder.Entity<TIHabilidadTiradaDeDaño>().HasKey(e => new { e.IdHabilidad, e.IdTirada });

            modelBuilder.Entity<TIHabilidadTiradaDeDaño>()
                .HasOne(i => i.TiradaDeDaño);

            modelBuilder.Entity<TIHabilidadTiradaDeDaño>()
                .HasOne(i => i.Habilidad)
                .WithOne(p => p.TiradaDeDaño);

            // - Habilidad items
            modelBuilder.Entity<TIHabilidadItem>().HasKey(e => new { e.IdHabilidad, e.IdItem });

            modelBuilder.Entity<TIHabilidadItem>()
                .HasOne(i => i.Item);

            modelBuilder.Entity<TIHabilidadItem>()
                .HasOne(i => i.Habilidad)
                .WithMany(p => p.ItemInvocacion)
                .HasForeignKey(ip => ip.IdHabilidad);

            modelBuilder.Entity<TIHabilidadItem>()
                .HasOne(i => i.Habilidad)
                .WithMany(p => p.ItemsQueCuesta)
                .HasForeignKey(ip => ip.IdHabilidad);

            // - Habilidad invocacion
            modelBuilder.Entity<TIHabilidadInvocacion>().HasKey(e => new { e.IdHabilidad, e.IdInvocacion });

            modelBuilder.Entity<TIHabilidadInvocacion>()
                .HasOne(i => i.Invocacion);

            modelBuilder.Entity<TIHabilidadInvocacion>()
                .HasOne(i => i.Habilidad)
                .WithMany(p => p.Invocacion)
                .HasForeignKey(ip => ip.IdHabilidad);

            // - Habilidad tiradas de uso
            modelBuilder.Entity<TIHabilidadTiradaBase>().HasKey(e => new { e.IdHabilidad, e.IdTirada });

            modelBuilder.Entity<TIHabilidadTiradaBase>()
                .HasOne(i => i.TiradaBase);

            modelBuilder.Entity<TIHabilidadTiradaBase>()
                .HasOne(i => i.Habilidad)
                .WithMany(p => p.TiradasDeUso)
                .HasForeignKey(ip => ip.IdHabilidad);

            // - Habilidad efectos
            modelBuilder.Entity<TIHabilidadEfecto>().HasKey(e => new { e.IdHabilidad, e.IdEfecto });

            modelBuilder.Entity<TIHabilidadEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIHabilidadEfecto>()
                .HasOne(i => i.Habilidad)
                .WithMany(p => p.EfectosSobreUsuario)
                .HasForeignKey(ip => ip.IdHabilidad);

            modelBuilder.Entity<TIHabilidadEfecto>()
                .HasOne(i => i.Habilidad)
                .WithMany(p => p.EfectoSobreObjetivo)
                .HasForeignKey(ip => ip.IdHabilidad);

        }
        #endregion

        #region Constructores

        public RolContext(string _nombreRolSeleccionado)
        {
            mNombreRolSeleccionado = _nombreRolSeleccionado;
        }

        #endregion
    }
}
