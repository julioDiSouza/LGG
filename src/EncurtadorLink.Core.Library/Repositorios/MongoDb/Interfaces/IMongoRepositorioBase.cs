using MongoDB.Driver;
using System.Linq.Expressions;
using EncurtadorLink.Core.Library.Entidades.MongoDb;

namespace EncurtadorLink.Core.Library.Repositorios.MongoDb.Interfaces
{
    public interface IMongoRepositorioBase<T> where T : EntidadeId
    {
        IQueryable<T> Consultar(Expression<Func<T, bool>> query = null);
        Task<T> ConsultarPorIdAsync(string id);
        Task<IEnumerable<T>> ConsultarPorIdsAsync(IEnumerable<string> ids);
        Task<bool> ExisteAsync(string id);
        Task<bool> ExisteAsync(Expression<Func<T, bool>> query);
        Task AdicionarAsync(T entidade);
        Task AdicionarVariosAsync(IEnumerable<T> entidades);
        Task AtualizarAsync(FilterDefinition<T> filtro, UpdateDefinition<T> atualizador, UpdateOptions updateOptions = null);
        Task AtualizarVariosAsync(FilterDefinition<T> filtro, UpdateDefinition<T> atualizador, UpdateOptions updateOptions = null);
        Task DeletarAsync(string id);
        Task DeletarVariosAsync(FilterDefinition<T> filtro);
        Task DeletarVariosAsync(Expression<Func<T, bool>> query);
    }
}
