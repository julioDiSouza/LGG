using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteModel>?> ListarClientesAsync();
        Task<bool> CadastroViaWebAsync(ClienteModelCriar clienteCriar);
        Task<bool> CadastrarAsync(ClienteModelCriar clienteCriar);
    }
}
