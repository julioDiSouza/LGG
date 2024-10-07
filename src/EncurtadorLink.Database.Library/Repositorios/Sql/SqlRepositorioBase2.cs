using System.Linq.Expressions;
using Microsoft.Win32.SafeHandles;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class SqlRepositorioBase2<T> : ISqlRepositorioBase<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<MysqlContext2> _optionsBuilder;
        private readonly string _connectionString;

        public SqlRepositorioBase2(string connectionString)
        {
            _optionsBuilder = new DbContextOptions<MysqlContext2>();
            _connectionString = connectionString;
        }

        public MysqlContext2 Contexto()
        {
            return new MysqlContext2(_optionsBuilder, _connectionString);
        }

        public async Task AdicionarAsync(T obj)
        {
            using (var db = Contexto())
            {
                await db.Set<T>().AddAsync(obj);
                await db.SaveChangesAsync();
            }
        }

        public async Task AtualizarAsync(T obj)
        {
            using (var db = Contexto())
            {
                db.Set<T>().Update(obj);
                await db.SaveChangesAsync();
            }
        }

        public async Task<T?> BuscarPorIdAsync(int id)
        {
            using (var db = Contexto())
            {
                return await db.Set<T>().FindAsync(id);
            }
        }

        public async Task ExcluirAsync(T obj)
        {
            using (var db = Contexto())
            {
                db.Set<T>().Remove(obj);
                await db.SaveChangesAsync();
            }
        }

        public async Task ExcluirAsync(IQueryable<T> query)
        {
            using (var db = Contexto())
            {
                var entities = query.ToList();
                db.Set<T>().RemoveRange(entities);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<T>> ListarAsync()
        {
            using (var db = Contexto())
            {
                return await db.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
        {
            using (var db = Contexto())
            {
                return await db.Set<T>().AnyAsync(query);
            }
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
