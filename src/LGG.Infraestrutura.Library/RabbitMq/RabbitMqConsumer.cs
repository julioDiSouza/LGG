using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LGG.Global.Library.Enumeradores;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;

namespace LGG.Infraestrutura.Library.RabbitMq
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly RabbitMqSettings _settings;
        private readonly ILogger<RabbitMqConsumer> _logger;

        public RabbitMqConsumer(
            IOptions<RabbitMqSettings> settings,
            ILogger<RabbitMqConsumer> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task StartAsync(
            string queue,
            Func<string, Task<MsgResultadoEnum>> onMessage,
            CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            var connection = await factory.CreateConnectionAsync(cancellationToken);
            var channel = await connection.CreateChannelAsync();

            await channel.BasicQosAsync(0, 10, false);

            await channel.QueueDeclareAsync(
                queue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var resultado = await onMessage(message);

                switch (resultado)
                {
                    case MsgResultadoEnum.Ack:
                        await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                        break;

                    case MsgResultadoEnum.Retry:
                        await channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
                        break;

                    case MsgResultadoEnum.Discard:
                        await channel.BasicNackAsync(eventArgs.DeliveryTag, false, false);
                        break;
                }
            };

            await channel.BasicConsumeAsync(
                queue: queue,
                autoAck: false,
                consumer: consumer);

            _logger.LogInformation("Consumindo fila {Queue}", queue);
        }
    }
}
