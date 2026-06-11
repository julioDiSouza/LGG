using MediatR;
using Microsoft.AspNetCore.Mvc;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using LGG.Aplicacao.Library.Helpers;
using LGG.Global.Library.Enumeradores;
using Microsoft.AspNetCore.Authorization;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;
using LGG.Aplicacao.Library.Mediator.Usuarios.Commands;

namespace LGG.Api.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator, IRabbitMqPublisher publisher)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] UsuarioCadastroDto usuario)
        {
            var validacao = usuario.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var retorno = await _mediator.Send(usuario.ConverterComandoCadastrar());

            if (retorno is null)
                return StatusCode(500, SistemaEnum.UsuarioFalhaCadastro.ObterDescricao());

            if (retorno.Mensagem.Sucesso)
                return Ok(retorno);

            return Conflict(retorno);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] string id)
        {
            var usuario = await _mediator.Send(new ObterUsuarioQuery { Id = id });

            return usuario is null ? NotFound() : Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarTodosAsync()
        {
            var usuarios = await _mediator.Send(new ObterTodosUsuariosQuery());

            return usuarios != null && usuarios.Any() ? Ok(usuarios) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync([FromRoute] string id, [FromBody] UsuarioAtualizarDto usuario)
        {
            var validacao = usuario.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var retorno = await _mediator.Send(usuario.ConverterComandoAtualizar(id));

            return retorno is null ? NotFound() : Ok(retorno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ApagarAsync([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(SistemaEnum.ParametroIdObrigatorio.ObterDescricao());

            await _mediator.Send(new ApagarUsuarioCommand { UsuarioId = id });

            return NoContent();
        }
    }
}
