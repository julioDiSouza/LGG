using Microsoft.Extensions.Logging;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IVideoCiclo;

namespace LGG.Infraestrutura.Library.VideoCiclo_
{
    public class VideoCicloService : IVideoCicloService
    {
        private readonly ILogger<VideoCicloService> _logger;
        private IUnidTrabalho _ut;

        public VideoCicloService(IUnidTrabalho ut, ILogger<VideoCicloService> logger)
        {
            _ut = ut;
            _logger = logger;
        }

        public async Task<CampeonatoMensal?> ObterCampeonatoVigenteAsync() 
            => await _ut.CampeonatoMensalRepositorio.ObterVigenteAsync();

        public async Task IniciarCicloAsync(CampeonatoMensal campeonatoMensal)
        {
            var ultimoCiclo = await _ut.VideoCicloRepositorio.ObterUltimoVideoCicloAsync();

            if(ultimoCiclo == null || ultimoCiclo.Status == VideoCicloStatusEnum.Concluido)
            {
                var videoCiclo = new VideoCiclo();
                var dataAtual = DateTime.Now;
                videoCiclo.CampeonatoMensalId = campeonatoMensal.Id;
                videoCiclo.DataCadastro = dataAtual;
                videoCiclo.Status = VideoCicloStatusEnum.ProcessandoBalde;
                videoCiclo.DataStatus = dataAtual;

                await _ut.VideoCicloRepositorio.AdicionarAsync(videoCiclo);
            }
        }

        public async Task PopularBaldeAsync()
        {
            var ultimoCiclo = await _ut.VideoCicloRepositorio.ObterUltimoVideoCicloAsync();

            if (ultimoCiclo != null && ultimoCiclo.Status == VideoCicloStatusEnum.ProcessandoBalde)
            {
                var ids = await _ut.VideoBaldeRepositorio.ObterVideosIdNoBaldeAsync(ultimoCiclo.Id);
                var videosFaltantes = await _ut.VideoRepositorio.ObterFaltantesPorIdsAsync(ids);

                if (videosFaltantes.Any())
                {
                    var dataHora = DateTime.Now;

                    var videosBalde = videosFaltantes.Select(video => new VideoBalde
                    {
                        VideoCicloId = ultimoCiclo.Id,
                        UsuarioId = video.UsuarioId,
                        VideoId = video.Id,
                        VideoIdYt = video.VideoId,
                        DataCadastro = dataHora
                    }).ToList();

                    await _ut.VideoBaldeRepositorio.AdicionarAsync(videosBalde);
                }
                else
                {
                    await _ut.VideoCicloRepositorio.AtualizarStatusAsync(
                        ultimoCiclo.Id, 
                        ultimoCiclo.CampeonatoMensalId, 
                        VideoCicloStatusEnum.ProcessandoMetricas
                        );
                }
            }
        }

        public async Task ProcessarTopDezAsync()
        {
            var ultimoCiclo = await _ut.VideoCicloRepositorio.ObterUltimoVideoCicloAsync();

            if (ultimoCiclo != null && ultimoCiclo.Status == VideoCicloStatusEnum.ProcessandoMetricas)
            {
                _logger.LogInformation("##=> TODO Top 10.");
            }
        }
    }
}