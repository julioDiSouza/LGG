using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Robot.Service.Services.Interfaces;
using EncurtadorLink.Shared.Library.Models.Configuracoes;

namespace EncurtadorLink.Robot.Service.Hosted
{
    internal class RobotHosted : BackgroundService
    {
        private readonly ConfiguracaoAutenticacao _configuracaoAutenticacao;
        private readonly IMonitoramento _monitoramento;

        public RobotHosted(IConfiguration configuration, IMonitoramento monitoramento)
        {
            _configuracaoAutenticacao = configuration.GetSection("ConfiguracaoAutenticacao").Get<ConfiguracaoAutenticacao>()!;
            _monitoramento = monitoramento;
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


                    await _monitoramento.TestarAutenticacaoAsync();
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
