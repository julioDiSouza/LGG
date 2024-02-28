using System.Linq.Expressions;
using Microsoft.Win32.SafeHandles;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class SqlRepositorioBase<T> : ISqlRepositorioBase<T>, IDisposable where T : class
    {
        private readonly MysqlContext _dbContexto;

        public SqlRepositorioBase(MysqlContext dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public async Task AdicionarAsync(T obj)
        {
            await _dbContexto.Set<T>().AddAsync(obj);
            await _dbContexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(T obj)
        {
            _dbContexto.Set<T>().Update(obj);
            await _dbContexto.SaveChangesAsync();
        }

        public async Task<T?> BuscarPorIdAsync(int id)
        {
            return await _dbContexto.Set<T>().FindAsync(id);
        }

        public async Task ExcluirAsync(T obj)
        {
            _dbContexto.Set<T>().Remove(obj);
            await _dbContexto.SaveChangesAsync();
        }

        public async Task<List<T>> ListarAsync()
        {
            return await _dbContexto.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
        {
            return await _dbContexto.Set<T>().AnyAsync(query);
        }

        #region ..:: Dispose ::..

        private bool _disposed = false;
        private SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _handle.Dispose();

            _disposed = true;
        }

        #endregion ..:: Fim - Dispose ::..

    }
}
