using EncurtadorLink.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorLink.Infra.BancoDados.SQL
{
    public class ContextoDb : IdentityDbContext<ApplicationUser>
    {
        public ContextoDb(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UrlEncurtada> UrlEncurtadas { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteAplicacao> ClienteAplicacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UrlEncurtada>(builder =>
            {
                builder.Property(u => u.Codigo).HasMaxLength(10);
                builder.HasIndex(u => u.Codigo).IsUnique();
            });

            modelBuilder.Entity<Cliente>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Nome).HasMaxLength(50);
                builder.Property(c => c.Email).HasMaxLength(50);
                builder.HasIndex(u => u.Email).IsUnique();
                builder.Property(c => c.Documento).HasMaxLength(14);
            });

            modelBuilder.Entity<ClienteAplicacao>(builder =>
            {
                builder.HasKey(ca => ca.ClienteId);
                builder.HasOne(ca => ca.Cliente).WithMany(c => c.Aplicacoes).HasForeignKey(c => c.ClienteId);
                builder.Property(c => c.Nome).HasMaxLength(50);
            });
        }

    }
}

