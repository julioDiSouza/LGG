using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Shared.Library.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
