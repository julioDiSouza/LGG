using EncurtadorLink.Entidades;
using EncurtadorLink.Services;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorLink.DataBase
{
    public class ContextoDb : DbContext
    {
        public ContextoDb(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UrlEncurtada> UrlEncurtadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlEncurtada>(builder =>
            {
                builder.Property(u => u.Codigo).HasMaxLength(UrlEncurtadorService.Tamanho);
                builder.HasIndex(u => u.Codigo).IsUnique();
            });
        }

    }
}
