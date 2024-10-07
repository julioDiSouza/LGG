using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class UrlSqlRepositorio2 : SqlRepositorioBase2<UrlSql>, IUrlSqlRepositorio
    {
        public UrlSqlRepositorio2(string connectionString) : base(connectionString)
        {

        }

        public async Task<(int, List<UrlSql>)> ObterUrlsPaginadasAsync(Guid clienteId, int pagina, int quantidade)
        {
            using (var db = Contexto())
            {
                var query = db.UrlSql
                    .AsQueryable()
                    .Where(u => u.ClienteId == clienteId);

                var total = await query.CountAsync();

                var urls = await query
                    .OrderByDescending(u => u.DataInclusao)
                    .Skip((pagina - 1) * quantidade)
                    .Take(quantidade)
                    .ToListAsync();

                return (total, urls);
            }
        }

        public async Task<UrlSql> ApagarUrlAsync(Guid clienteId, Guid urlId)
        {
            using (var db = Contexto())
            {
                var query = from u in db.UrlSql
                            where u.Id == urlId &&
                                  u.ClienteId == clienteId
                            select u;

                var urlSql = await query.FirstOrDefaultAsync();

                await ExcluirAsync(query);

                return urlSql!;
            }
        }
    }
}