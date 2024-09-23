using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IIEmailService
    {
        Task CriarFilaEmailAsync(FilaEmailModel model);
        Task ProcessarFilaAsync();
    }
}
