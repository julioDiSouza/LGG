using EncurtadorLink.Services.Library.Models;

namespace EncurtadorLink.Services.Library.Interfaces
{
    public interface IClienteService
    {
        Task<bool> CadastrarAsync(ClienteModelCriar clienteCriar);
    }
}
