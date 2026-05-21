using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGG.Aplicacao.Library.Interfaces.IVideoMetricas;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Global.Library.Enumeradores;
using LGG.Infraestrutura.Library.VideoCiclo_;
using Microsoft.Extensions.Logging;

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
    }
}
