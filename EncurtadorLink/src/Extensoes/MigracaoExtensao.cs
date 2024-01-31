using EncurtadorLink.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorLink.Extensoes
{
    public static class MigracaoExtensao
    {
        public static void AplicarMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ContextoDb>();

            dbContext.Database.Migrate();
        }
    }
}
