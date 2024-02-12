using EncurtadorLink.Infra.BancoDados.SQL;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorLink.Infra.Extensoes
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
