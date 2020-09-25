using Microsoft.EntityFrameworkCore;

namespace AppGM.Core
{
    public class RolContext : DbContext
    {
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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source = datitosrol.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Agregar muchas cosas :u

            // Persoanje
            modelBuilder.Entity<ModeloPersonaje>().ToTable("ModeloPersonaje")
                .HasDiscriminator<int>("Clase")
                .HasValue<ModeloServant>(1)
                .HasValue<ModeloMaster>(2)
                .HasValue<ModeloInvocacion>(3)
                .HasValue<ModeloPersonaje>(4);

            // Personaje efectos
            modelBuilder.Entity<TIPersonajeEfecto>().HasKey(e => new { e.IdEfecto, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajeEfecto>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIPersonajeEfecto>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Efectos)
                .HasForeignKey(ip => ip.IdPersonaje);
            
            // Personaje utilizables
            modelBuilder.Entity<TIPersonajeUtilizable>().HasKey(e => new { e.IdUtilizable, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajeUtilizable>()
                .HasOne(i => i.Utilizable);

            modelBuilder.Entity<TIPersonajeUtilizable>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Inventario)
                .HasForeignKey(ip => ip.IdPersonaje);

            // Personaje defensivos
            modelBuilder.Entity<TIPersonajeDefensivo>().HasKey(e => new { e.IdDefensivo, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajeDefensivo>()
                .HasOne(i => i.Defensivo);

            modelBuilder.Entity<TIPersonajeDefensivo>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Armadura)
                .HasForeignKey(ip => ip.IdPersonaje);

            // Personaje aliados
            modelBuilder.Entity<TIPersonajePersonaje>().HasKey(e => new { e.IdAliado, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajePersonaje>()
                .HasOne(i => i.Personaje);

            modelBuilder.Entity<TIPersonajePersonaje>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Aliados)
                .HasForeignKey(ip => ip.IdPersonaje);

            // Personaje perks
            modelBuilder.Entity<TIPersonajePerk>().HasKey(e => new { e.IdPerk, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajePerk>()
                .HasOne(i => i.Personaje);

            modelBuilder.Entity<TIPersonajePerk>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Perks)
                .HasForeignKey(ip => ip.IdPersonaje);


            // Personaje skills
            modelBuilder.Entity<TIPersonajeHabilidad>().HasKey(e => new { e.IdHabilidad, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajeHabilidad>()
                .HasOne(i => i.Personaje);

            modelBuilder.Entity<TIPersonajeHabilidad>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Skills)
                .HasForeignKey(ip => ip.IdPersonaje);


            // Personaje magias
            modelBuilder.Entity<TIPersonajeMagia>().HasKey(e => new { e.IdMagia, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajeMagia>()
                .HasOne(i => i.Personaje);

            modelBuilder.Entity<TIPersonajeMagia>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.Magias)
                .HasForeignKey(ip => ip.IdPersonaje);


            // Personaje modificador de defensa
            modelBuilder.Entity<TIPersonajeModificadorDeDefensa>().HasKey(e => new { e.IdModificadorDefensa, e.IdPersonaje });

            modelBuilder.Entity<TIPersonajeModificadorDeDefensa>()
                .HasOne(i => i.Personaje);

            modelBuilder.Entity<TIPersonajeModificadorDeDefensa>()
                .HasOne(i => i.Personaje)
                .WithMany(p => p.ModificadoresDeDefensa)
                .HasForeignKey(ip => ip.IdPersonaje);

            // Servant noble phantasm
            modelBuilder.Entity<TIServantNoblePhantasm>().HasKey(e => new { e.IdServant, e.IdNoblePhantasm });

            modelBuilder.Entity<TIServantNoblePhantasm>()
                .HasOne(i => i.Servant);

            modelBuilder.Entity<TIServantNoblePhantasm>()
                .HasOne(i => i.Servant)
                .WithMany(p => p.NoblePhantasms)
                .HasForeignKey(ip => ip.IdServant);

            // Efectos
            modelBuilder.Entity<TIEfectoModificadorDeStatBase>().HasKey(e => new { e.IdEfecto, e.IdModificadorDeStat });

            modelBuilder.Entity<TIEfectoModificadorDeStatBase>()
                .HasOne(i => i.Efecto);

            modelBuilder.Entity<TIEfectoModificadorDeStatBase>()
                .HasOne(i => i.Efecto)
                .WithMany(p => p.Modificaciones)
                .HasForeignKey(ip => ip.IdEfecto);

            // Utilizable
        }
    }
}
