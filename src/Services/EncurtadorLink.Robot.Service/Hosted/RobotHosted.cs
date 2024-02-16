using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Models.Configuracoes;

namespace EncurtadorLink.Robot.Service.Hosted
{
    internal class RobotHosted : BackgroundService
    {
        private readonly ConfiguracaoAutenticacao _configuracaoAutenticacao;

        public RobotHosted(IConfiguration configuration)
        {
            _configuracaoAutenticacao = configuration.GetSection("ConfiguracaoAutenticacao").Get<ConfiguracaoAutenticacao>()!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    await Task.Run(() =>
                    {
                        Console.WriteLine("****************");
                        Console.WriteLine("**  Rodando!  **");
                        Console.WriteLine("****************");
                        Console.WriteLine("");
                    });

                }
                catch (Exception ex)
                {
                    if (ex is TaskCanceledException)
                        return;

                    throw;
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
