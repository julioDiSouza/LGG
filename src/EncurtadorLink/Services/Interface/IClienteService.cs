using EncurtadorLink.Model.Usuario;

namespace EncurtadorLink.Services.Interface
{
    public interface IClienteService
    {
        Task<bool> CadastrarAsync(ClienteDtoCriar clienteDtoCriar);
    }
}
