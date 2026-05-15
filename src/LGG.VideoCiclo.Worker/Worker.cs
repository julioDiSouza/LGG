using LGG.Aplicacao.Library.Interfaces.IVideoCiclo;

namespace LGG.VideoCiclo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly string _nomeServico = "LGG.VideoCiclo.Worker";
        private readonly ILogger<Worker> _logger;
        private readonly IVideoCicloService _videoCicloService;

        public Worker(ILogger<Worker> logger, IVideoCicloService videoCicloService)
        {
            _logger = logger;
            _videoCicloService = videoCicloService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"Worker {_nomeServico} iniciando em: {DateTime.Now}.");

                    await _videoCicloService.IniciarCicloAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message, _nomeServico, DateTime.Now);
                }
                finally
                {
                    _logger.LogInformation($"Worker {_nomeServico} finalizando em: {DateTime.Now}.");
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
