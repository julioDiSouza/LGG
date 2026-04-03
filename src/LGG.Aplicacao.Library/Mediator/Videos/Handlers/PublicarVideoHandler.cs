using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Mediator.Canais.Queries;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class PublicarVideoHandler : IRequestHandler<PublicarVideoCommand, SistemaEnum>
    {
        private readonly IMediator _mediator;
        private readonly IRabbitMqPublisher _publisher;
        private readonly string _filaKey;
        private readonly string _filaPublicarVideo;

        public PublicarVideoHandler(IConfiguration configuration, IMediator mediator, IRabbitMqPublisher publisher)
        {
            _mediator = mediator;
            _publisher = publisher;
            _filaKey = configuration["Criptografia:Fila:ChaveFila"]!.ToString();
            _filaPublicarVideo = configuration["RabbitMqFilas:PublicarVideo"]!.ToString();
        }

        public async Task<SistemaEnum> Handle(PublicarVideoCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _mediator.Send(new ObterUsuarioQuery { Id = request.UsuarioId });

            if (usuario == null ||
                usuario.Canal == null ||
                usuario.Status != UsuarioStatusEnum.Ativo)
                return SistemaEnum.UsuarioInvalido;

            var canal = await _mediator.Send(new ObterCanalQuery { CanalId = usuario.Canal.CanalId });

            if (canal == null ||
                canal.Status != CanalStatusEnum.Ativo)
                return SistemaEnum.CanalInvalido;

            var campeonatoVigente = await _mediator.Send(new ObterCampeonatoVigenteQuery());
            if (campeonatoVigente == null ||
                campeonatoVigente.Status != CampeonatoMensalStatusEnum.Aberto)
                return SistemaEnum.CampeonatoIndisponivel;

            var msgVideo = MontaMsgVideo(request);
            await _publisher.PublishAsync(_filaPublicarVideo, msgVideo);

            return SistemaEnum.SolicitacaoEnviadaSucesso;
        }

        private VideoPublicacaoDtoMsg MontaMsgVideo(PublicarVideoCommand request)
        {
            return new VideoPublicacaoDtoMsg(_filaKey)
            {
                UsuarioId = request.UsuarioId,
                Link = request.Link
            };
        }
    }
}
