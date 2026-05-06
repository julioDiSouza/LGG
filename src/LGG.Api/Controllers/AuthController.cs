using MediatR;
using Microsoft.AspNetCore.Mvc;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using LGG.Aplicacao.Library.Helpers;
using LGG.Global.Library.Enumeradores;

namespace LGG.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginRequisicaoDto requisicao)
        {
            var validacao = requisicao.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var retorno = await _mediator.Send(requisicao.ConverterUsuarioTokenQuery());

            if (retorno != null)
                return Ok(retorno);

            return NotFound(SistemaEnum.NaoEncontrado.ObterDescricao());
        }
    }
}
