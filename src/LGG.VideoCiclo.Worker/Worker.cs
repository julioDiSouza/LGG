using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.VideoCiclo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IVideoCicloService _videoCicloService;
        private readonly int _intervaloExecucaoSegundos;
        private readonly int _intervaloCicloSegundos;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IVideoCicloService videoCicloService)
        {
            _logger = logger;
            _videoCicloService = videoCicloService;
            _intervaloExecucaoSegundos = configuration.GetValue<int>("IntervaloExecucaoSegundos", 30) * 1000;
            _intervaloCicloSegundos = configuration.GetValue<int>("IntervaloCicloSegundos", 60);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"##=> Iniciando em: {DateTime.Now}.");

					var campeonatoMensal = await _videoCicloService.ObterCampeonatoVigenteAsync();
                    if (campeonatoMensal != null)
                    {
						await _videoCicloService.IniciarCicloAsync(campeonatoMensal, _intervaloCicloSegundos);
						await _videoCicloService.PopularBaldeAsync();
                        await _videoCicloService.FinalizarMetricasAsync();
                        await _videoCicloService.ProcessarTopDezAsync();
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
