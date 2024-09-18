using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EncurtadorLink.Database.Library.Contextos
{
    public class MysqlContext : IdentityDbContext<ApplicationUser>
    {
        public MysqlContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UrlSql> UrlSql { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteAplicacao> ClienteAplicacao { get; set; }
        public DbSet<FilaEmail> FilaEmail { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var _connectionString = "server=localhost; database=db_encurtador; user=root; password=root";

                optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString),
                    builder => builder.MigrationsAssembly("EncurtadorLink"));
                base.OnConfiguring(optionsBuilder);
            }
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UrlSql>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasIndex(x => x.ClienteId);
                builder.Property(x => x.Codigo).HasMaxLength(10);
                builder.HasIndex(x => x.Codigo).IsUnique();
                builder.HasIndex(x => x.IP);
                builder.HasIndex(x => x.DataInclusao);
            });

            modelBuilder.Entity<Cliente>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.NomeCompleto).HasMaxLength(50);
                builder.Property(c => c.Email).HasMaxLength(50);
                builder.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<ClienteAplicacao>(builder =>
            {
                builder.HasKey(ca => ca.ClienteId);
                builder.HasOne(ca => ca.Cliente).WithMany(c => c.Aplicacoes).HasForeignKey(c => c.ClienteId);
                builder.Property(c => c.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<FilaEmail>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.HasOne(ca => ca.Cliente).WithMany(c => c.FilaEmails).HasForeignKey(c => c.ClienteId);
                builder.Property(c => c.Email).HasMaxLength(50);
                builder.HasIndex(x => x.Email);
            });
        }
    }
}
