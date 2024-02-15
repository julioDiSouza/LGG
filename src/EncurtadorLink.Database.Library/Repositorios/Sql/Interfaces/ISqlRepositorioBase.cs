using System.Linq.Expressions;

namespace EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces
{
    public interface ISqlRepositorioBase<T> where T : class
    {
        Task AdicionarAsync(T obj);
        Task AtualizarAsync(T obj);
        Task ExcluirAsync(T obj);
        Task<T?> BuscarPorIdAsync(int id);
        Task<List<T>> ListarAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> query);
    }

}
