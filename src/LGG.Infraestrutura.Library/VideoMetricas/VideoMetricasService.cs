using Microsoft.Extensions.Logging;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IVideoMetricas;

namespace LGG.Infraestrutura.Library.VideoMetricas
{
    public class VideoMetricasService : IVideoMetricasService
    {
        private readonly ILogger<VideoMetricasService> _logger;
        private IUnidTrabalho _ut;

        public VideoMetricasService(IUnidTrabalho ut, ILogger<VideoMetricasService> logger)
        {
            _ut = ut;
            _logger = logger;
        }

        public async Task<CampeonatoMensal?> ObterCampeonatoVigenteAsync()
            => await _ut.CampeonatoMensalRepositorio.ObterVigenteAsync();

        public async Task<IEnumerable<VideoBalde?>> ObterVideosBaldeAsync()
        {
            var ultimoCiclo = await _ut.VideoCicloRepositorio.ObterUltimoVideoCicloAsync();

            if (ultimoCiclo == null || ultimoCiclo.Status != VideoCicloStatusEnum.ProcessandoMetricas)
                return new List<VideoBalde?>();

            return await _ut.VideoBaldeRepositorio.ObterVideosBaldeAsync(ultimoCiclo.Id);
        }

        public async Task AdicionarVideoEstatisticaAsync(VideoEstatistica videoEstatistica)
            => await _ut.VideoEstatisticaRepositorio.AdicionarAsync(videoEstatistica);
    }
}
