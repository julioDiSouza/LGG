namespace LGG.Aplicacao.Library.Interfaces.IRabbitMq
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync<T>(string queueName, T message);
    }
}
