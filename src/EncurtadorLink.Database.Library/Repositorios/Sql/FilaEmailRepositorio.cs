using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class FilaEmailRepositorio : SqlRepositorioBase<FilaEmail>, IFilaEmailRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public FilaEmailRepositorio(MysqlContext dbContexto) : base(dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public async Task<List<FilaEmail>> ObterPendenciasAsync()
        {
            var query = from f in _dbContexto.FilaEmail
                        where f.DataEnvio == null 
                        select f;

            return await query
                .OrderBy(u => u.DataInclusao)
                .ToListAsync();
        }

        public async Task RegistrarEnvioAsync(FilaEmail filaEmail)
        {
            await AtualizarAsync(filaEmail);
        }

        public async Task ApagarAnterioresAsync(FilaEmail filaEmail)
        {
            var query = from f in _dbContexto.FilaEmail 
                        where f.DataInclusao < filaEmail.DataInclusao && 
                              f.Tipo == filaEmail.Tipo && 
                              f.Email == filaEmail.Email 
                        select f; 

            await ExcluirAsync(query);
        }

        public async Task ApagarAnterioresRawAsync(FilaEmail filaEmail)
        {
            var query = @"  DELETE FROM FilaEmail 
                            WHERE       DataInclusao    < @DataCorte 
                            AND         Tipo            = @Tipo 
                            AND         Email           = @Email ";

            await _dbContexto.Database.ExecuteSqlRawAsync(
                query,
                new MySqlParameter("@DataCorte", filaEmail.DataInclusao),
                new MySqlParameter("@Tipo", filaEmail.Tipo),
                new MySqlParameter("@Email", filaEmail.Email)
                );
        }
    }
}