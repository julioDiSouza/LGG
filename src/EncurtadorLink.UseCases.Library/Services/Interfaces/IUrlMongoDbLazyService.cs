using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUrlMongoDbLazyService
    {
        Task<UrlMongoDbModel> ConsultarPorCodigoAsync(string codigo);
    }
}
