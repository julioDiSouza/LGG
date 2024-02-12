using EncurtadorLink.Services.Library.Models;

namespace EncurtadorLink.Services.Library.Services.Interfaces
{
    public interface IUrlMongoDbService
    {
        Task<UrlMongoDbModel> ConsultarPorCodigoAsync(string codigo);
    }
}
