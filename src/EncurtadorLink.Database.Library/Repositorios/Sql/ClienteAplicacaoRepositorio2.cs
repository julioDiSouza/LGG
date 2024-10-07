using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class ClienteAplicacaoRepositorio2 : SqlRepositorioBase2<ClienteAplicacao>, IClienteAplicacaoRepositorio
    {
        public ClienteAplicacaoRepositorio2(string connectionString) : base(connectionString)
        {

        }
    }
}
