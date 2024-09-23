using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EncurtadorLink.Robot.Service.Services.Interfaces;
using EncurtadorLink.Shared.Library.Models.Configuracoes;

namespace EncurtadorLink.Robot.Service.Hosted
{
    internal class RobotHosted : BackgroundService
    {
        private readonly ConfiguracaoAutenticacao _configuracaoAutenticacao;
        private readonly IServiceProvider _serviceProvider;

        public RobotHosted(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuracaoAutenticacao = configuration.GetSection("ConfiguracaoAutenticacao").Get<ConfiguracaoAutenticacao>()!;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var filaEmailService = scope.ServiceProvider.GetRequiredService<IFilaEmailService>();

                        await filaEmailService.ProcessarFilaAsync();
                    }
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
