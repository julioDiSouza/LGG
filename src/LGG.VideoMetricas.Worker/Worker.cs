using LGG.Aplicacao.Library.Helpers;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.VideoMetricas.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IVideoMetricasService _videoMetricasService;
        private readonly IYoutubeService _youtubeService;
        private readonly int _intervaloExecucaoSegundos;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IVideoMetricasService videoMetricasService, IYoutubeService youtubeService)
        {
            _logger = logger;
            _videoMetricasService = videoMetricasService;
            _youtubeService = youtubeService;
            _intervaloExecucaoSegundos = configuration.GetValue<int>("IntervaloExecucaoSegundos", 30) * 1000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"##=> Iniciando em: {DateTime.Now}.");

                    var campeonatoMensal = await _videoMetricasService.ObterCampeonatoVigenteAsync();
                    if (campeonatoMensal != null)
                    {
                        var videosBalde = await _videoMetricasService.ObterVideosBaldeAsync();
                        foreach (var videoBalde in videosBalde)
                        {
                            var videoResponseDto = await _youtubeService.ObterVideosPorIdAsync(videoBalde!.VideoIdYt);
                            if (videoResponseDto!.Items!.Any())
                            {
                                var videoEstatistica = videoResponseDto.ConverterParaVideoEstatistica();
                                videoEstatistica.VideoId = videoBalde.VideoId;
                                videoEstatistica.VideoCicloId = videoBalde.VideoCicloId;

                                await _videoMetricasService.AdicionarVideoEstatisticaAsync(videoEstatistica);
                            }
                        }
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
