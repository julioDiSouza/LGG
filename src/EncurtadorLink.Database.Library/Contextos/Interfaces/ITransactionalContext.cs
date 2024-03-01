namespace EncurtadorLink.Database.Library.Contextos.Interfaces
{
    public interface ITransactionalContext : IDisposable
    {
        Task StartTransactionAsync();
        Task CommitAsync();
        Task AbortAsync();
    }

    public interface ITransactionalContext<T> : ITransactionalContext
    {
        T Transaction { get; }
    }
}
