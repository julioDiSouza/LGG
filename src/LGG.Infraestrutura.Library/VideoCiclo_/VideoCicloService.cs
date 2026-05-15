using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IVideoCiclo;

namespace LGG.Infraestrutura.Library.VideoCiclo_
{
    public class VideoCicloService : IVideoCicloService
    {
        private IUnidTrabalho _ut;

        public VideoCicloService(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task IniciarCicloAsync()
        {
            var ultimoCiclo = await _ut.VideoCicloRepositorio.ObterUltimoVideoCicloAsync();

            if(ultimoCiclo == null || ultimoCiclo.Status == VideoCicloStatusEnum.Concluido)
            {
                var videoCiclo = new VideoCiclo();
                var dataAtual = DateTime.Now;
                videoCiclo.CampeonatoMensalId = 1;
                videoCiclo.DataCadastro = dataAtual;
                videoCiclo.Status = VideoCicloStatusEnum.ProcessandoBalde;
                videoCiclo.DataStatus = dataAtual;

                await _ut.VideoCicloRepositorio.AdicionarAsync(videoCiclo);
            }

            await Task.CompletedTask;
        }

    }
}
