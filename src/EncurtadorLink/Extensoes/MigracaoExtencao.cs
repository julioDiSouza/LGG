using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Database.Library.Contextos;

namespace EncurtadorLink.Extensoes
{
    public static class MigracaoExtencao
    {
        public static void AplicarMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<MysqlContext>();

            dbContext.Database.Migrate();
        }
    }
}
