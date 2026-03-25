using System.Text;
using RabbitMQ.Client;
using System.Text.Json;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;

namespace LGG.Infraestrutura.Library.RabbitMq
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IRabbitMqConnection _connectionProvider;

        public RabbitMqPublisher(IRabbitMqConnection connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task PublishAsync<T>(string queueName, T message)
        {
            var connection = await _connectionProvider.GetConnectionAsync();
            await using var channel = await connection.CreateChannelAsync(); 

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message));

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: true,
                basicProperties: properties,
                body: body);
        }
    }
}
