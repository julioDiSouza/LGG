using MediatR;
using System.Net;
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

            var resposta = await _mediator.Send(video.ComandoPublicar());

            if (resposta == SistemaEnum.SolicitacaoEnviadaSucesso)
                return Ok(resposta.ObterObjetoCodigoDescricao());

            return NotFound(resposta.ObterObjetoCodigoDescricao());
        }

        [HttpPost("Cadastro")]
        [ProducesResponseType(typeof(VideoCadastroRetornoDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErroDto), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<VideoCadastroRetornoDto>> CadastroAsync([FromBody] VideoCadastroDto videoCadastroDto)
        {
            var validacao = videoCadastroDto.ValidacaoCadastro();
            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var resultado = await _mediator.Send(videoCadastroDto.ComandoCadastrar());
            
            if (resultado?.Erro != null)
                return BadRequest(resultado);

            return Created(string.Empty, resultado);
        }

        [HttpGet("Consultar/{id}")]
        [ProducesResponseType(typeof(VideoConsultaDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<VideoConsultaDto>> ConsultarAsync([FromRoute] long id)
        {
            var resultado = await _mediator.Send(VideoHelper.QueryObter(id));
            if (resultado == null)
                return NotFound(SistemaEnum.NaoEncontrado.ObterObjetoCodigoDescricao());

            return Ok(resultado);
        }

        [HttpGet("ConsultarTodos")]
        [ProducesResponseType(typeof(List<VideoConsultaDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<VideoConsultaDto>>> ConsultarTodosAsync()
        {
            var resultado = await _mediator.Send(new LGG.Aplicacao.Library.Mediator.Videos.Queries.ObterTodosVideosQuery());
            return Ok(resultado);
        }

        [HttpPut("Atualizar/{id}")]
        [ProducesResponseType(typeof(VideoAtualizarDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroDto), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<VideoAtualizarDto>> AtualizarAsync([FromRoute] long id, [FromBody] VideoAtualizarDto videoAtualizarDto)
        {
            videoAtualizarDto.Id = id;
            var validacao = videoAtualizarDto.ValidacaoAtualizacao();
            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var resultado = await _mediator.Send(videoAtualizarDto.ComandoAtualizar());
            if (resultado == null)
                return NotFound(SistemaEnum.NaoEncontrado.ObterObjetoCodigoDescricao());

            return Ok(resultado);
        }

        [HttpDelete("Deletar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErroDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletarAsync([FromRoute] long id)
        {
            await _mediator.Send(VideoHelper.ComandoApagar(id));
            return NoContent();
        }
    }
}
