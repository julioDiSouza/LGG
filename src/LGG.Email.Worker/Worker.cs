using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Email.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailService _emailService;
        private readonly int _intervaloExecucaoSegundos;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
            _intervaloExecucaoSegundos = configuration.GetValue<int>("IntervaloExecucaoSegundos", 30) * 1000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"##=> Iniciando em: {DateTime.Now}.");

                    var emailsBalde = await _emailService.ObterEmailsBaldeAsync();
                    if (emailsBalde.Any())
                    {
                        await _emailService.EnviarEmailSync(emailsBalde);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    _logger.LogInformation($"##=> Finalizando em: {DateTime.Now}.");
                    await Task.Delay(_intervaloExecucaoSegundos, stoppingToken);
                }
            }
        }
    }
}