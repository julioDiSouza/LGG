using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUrlMongoDbService
    {
        Task<UrlMongoDbModel> ConsultarPorCodigoAsync(string codigo);
    }
}
