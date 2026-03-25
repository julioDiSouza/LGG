using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Mediator.Canais.Commands;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Mediator.Canais.Handlers
{
    public class ValidarCanalHandler : IRequestHandler<ValidarCanalCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IRabbitMqPublisher _publisher;
        private readonly string _filaKey;
        private readonly string _filaValidarCanal;

        public ValidarCanalHandler(IConfiguration configuration, IMediator mediator, IRabbitMqPublisher publisher)
        {
            _mediator = mediator;
            _publisher = publisher;
            _filaKey = configuration["Criptografia:Fila:ChaveFila"]!.ToString();
            _filaValidarCanal = configuration["RabbitMqFilas:ValidarCanal"]!.ToString();
        }

        public async Task<bool> Handle(ValidarCanalCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _mediator.Send(new ObterUsuarioQuery { Id = request.UsuarioId });

            if (usuario == null || 
                usuario.Canal == null || 
                usuario.Status != UsuarioStatusEnum.Ativo)
                return false;

            var filaCanal = MontaFilaCanal(request, usuario.Canal.Codigo);
            await _publisher.PublishAsync(_filaValidarCanal, filaCanal);

            return true;
        }

        private CanalValidacaoDtoMsg MontaFilaCanal(ValidarCanalCommand request, string codigo)
        {
            return new CanalValidacaoDtoMsg(_filaKey)
            {
                UsuarioId = request.UsuarioId,
                Nome = request.Nome,
                Codigo = codigo
            };
        }
    }
}
