using RabbitMQ.Client;

namespace LGG.Aplicacao.Library.Interfaces.IRabbitMq
{
    public interface IRabbitMqConnection
    {
        Task<IConnection> GetConnectionAsync();
    }
}
