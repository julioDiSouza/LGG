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
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PublicarAsync([FromBody] VideoPublicacaoDto video)
        {
            var validacao = video.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var solicitado = await _mediator.Send(video.ComandoPublicar());

            if (solicitado)
                return Ok(MsgSistemaEnum.SolicitacaoEnviadaSucesso.ObterDescricao());

            return NotFound(MsgSistemaEnum.NaoEncontrado.ObterDescricao());
        }
    }
}
