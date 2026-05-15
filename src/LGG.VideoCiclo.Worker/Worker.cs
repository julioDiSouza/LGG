using LGG.Aplicacao.Library.Interfaces.IVideoCiclo;

namespace LGG.VideoCiclo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IVideoCicloService _videoCicloService;
        private readonly int _intervaloExecucaoSegundos;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IVideoCicloService videoCicloService)
        {
            _logger = logger;
            _videoCicloService = videoCicloService;
            _intervaloExecucaoSegundos = configuration.GetValue<int>("IntervaloExecucaoSegundos", 30) * 1000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"#=> Iniciando em: {DateTime.Now}.");

                    await _videoCicloService.IniciarCicloAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    _logger.LogInformation($"#=> Finalizando em: {DateTime.Now}.");
                    await Task.Delay(_intervaloExecucaoSegundos, stoppingToken);
                }
            }
        }
    }
}
