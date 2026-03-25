using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Interfaces.IRabbitMq
{
    public interface IRabbitMqConsumer
    {
        Task StartAsync(
            string queueName,
            Func<string, Task<MsgResultadoEnum>> onMessage,
            CancellationToken cancellationToken);
    }
}
