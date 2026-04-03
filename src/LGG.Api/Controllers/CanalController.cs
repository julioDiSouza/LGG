using MediatR;
using Microsoft.AspNetCore.Mvc;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using LGG.Aplicacao.Library.Helpers;
using LGG.Global.Library.Enumeradores;
using Microsoft.AspNetCore.Authorization;

namespace LGG.Api.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CanalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CanalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ValidarAsync([FromBody] CanalValidacaoDto canal)
        {
            var validacao = canal.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var solicitado = await _mediator.Send(canal.ComandoValidar());

            if (solicitado)
                return Ok(SistemaEnum.SolicitacaoEnviadaSucesso.ObterDescricao());

            return NotFound(SistemaEnum.NaoEncontrado.ObterDescricao());
        }
    }
}
