using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IEmailBaldeRepositorio
    {
        Task AdicionarAsync(EmailBalde emailBalde);
    }
}
