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

        public async Task IniciarCicloAsync()
        {
            var campeonatoMensal = await _ut.CampeonatoMensalRepositorio.ObterVigenteAsync();
            if(campeonatoMensal == null)
            {
                _logger.LogInformation("#=> Nenhum campeonato mensal vigente encontrado. O ciclo de vídeo não será iniciado.");
                return;
            }

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

                _logger.LogInformation("#=> Ciclo de vídeo iniciado com sucesso.");
            }
            else
            {
                _logger.LogInformation("#=> Um ciclo de vídeo já está em andamento. O novo ciclo não será iniciado.");
            }
        }

    }
}