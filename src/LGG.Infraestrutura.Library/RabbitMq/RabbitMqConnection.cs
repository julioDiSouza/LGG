using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;

namespace LGG.Infraestrutura.Library.RabbitMq
{
    public sealed class RabbitMqConnection : IRabbitMqConnection, IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public RabbitMqConnection(IOptions<RabbitMqSettings> settings)
        {
            _factory = new ConnectionFactory
            {
                HostName = settings.Value.HostName,
                UserName = settings.Value.UserName,
                Password = settings.Value.Password
            };
        }

        public async Task<IConnection> GetConnectionAsync()
        {
            if (_connection is { IsOpen: true })
                return _connection;

            await _lock.WaitAsync();
            try
            {
                if (_connection is { IsOpen: true })
                    return _connection;

                _connection = await _factory.CreateConnectionAsync();
                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }

            _lock.Dispose();
        }
    }
}
