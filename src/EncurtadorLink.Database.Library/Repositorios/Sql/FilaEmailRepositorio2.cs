using MySqlConnector;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class FilaEmailRepositorio2 : SqlRepositorioBase2<FilaEmail>, IFilaEmailRepositorio
    {
        public FilaEmailRepositorio2(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<FilaEmail>> ObterPendenciasAsync()
        {
            using (var db = Contexto())
            {
                var query = from f in db.FilaEmail
                            where f.DataEnvio == null
                            select f;

                return await query
                    .OrderBy(u => u.DataInclusao)
                    .ToListAsync();
            }
        }

        public async Task RegistrarEnvioAsync(FilaEmail filaEmail)
        {
            await AtualizarAsync(filaEmail);
        }

        public async Task ApagarAnterioresAsync(FilaEmail filaEmail)
        {
            using (var db = Contexto())
            {
                var query = from f in db.FilaEmail
                            where f.DataInclusao < filaEmail.DataInclusao &&
                                  f.Tipo == filaEmail.Tipo &&
                                  f.Email == filaEmail.Email
                            select f;

                await ExcluirAsync(query);
            }
        }

        public async Task ApagarAnterioresRawAsync(FilaEmail filaEmail)
        {
            using (var db = Contexto())
            {
                var query = @"  DELETE FROM FilaEmail 
                            WHERE       DataInclusao    < @DataCorte 
                            AND         Tipo            = @Tipo 
                            AND         Email           = @Email ";

                await db.Database.ExecuteSqlRawAsync(
                    query,
                    new MySqlParameter("@DataCorte", filaEmail.DataInclusao),
                    new MySqlParameter("@Tipo", filaEmail.Tipo),
                    new MySqlParameter("@Email", filaEmail.Email)
                    );
            }
        }
    }
}