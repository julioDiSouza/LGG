using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class UrlSqlRepositorio : SqlRepositorioBase<UrlSql>, IUrlSqlRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public UrlSqlRepositorio(MysqlContext dbContexto) : base(dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public async Task<(int, List<UrlSql>)> ObterUrlsPaginadasAsync(Guid clienteId, int pagina, int quantidade)
        {
            var query = 
                _dbContexto.UrlSql.AsQueryable()
                .Where(u => u.ClienteId == clienteId);

            var total = await query.CountAsync();

            var urls = await query
                .OrderByDescending(u => u.DataInclusao)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return (total, urls);
        }

        public async Task<UrlSql> ApagarUrlAsync(Guid clienteId, Guid urlId)
        {
            var query = from u in _dbContexto.UrlSql 
                        where u.Id == urlId && 
                              u.ClienteId == clienteId 
                        select u;

            var urlSql = await query.FirstOrDefaultAsync();

            await ExcluirAsync(query);

            return urlSql!;
        }
    }
}