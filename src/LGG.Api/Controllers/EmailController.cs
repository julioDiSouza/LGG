using MediatR;
using Microsoft.AspNetCore.Mvc;
using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;
using Microsoft.AspNetCore.Authorization;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Api.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        
        public EmailController(IConfiguration configuration, IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpGet("cadastro_confirmacao")]
        public async Task<IActionResult> ConfirmarCadastroAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Query["key"]))
                    return NotFound(SistemaEnum.NaoEncontrado.ObterDescricao());

                var (keyValida, emailConfirmacao) = _emailService.ValidarKey(Request.Query["key"].ToString());

                if (keyValida)
                {
                    var (usuarioValido, token) = await _mediator.Send(new ValidarUsuarioQuery(emailConfirmacao!.UsuarioId, emailConfirmacao.Email));

                    if (usuarioValido)
                        return Ok(token);
                }

                return NotFound(SistemaEnum.NaoEncontrado.ObterDescricao());
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, SistemaEnum.ErroInterno.ObterDescricao());
            }
        }
    }
}
