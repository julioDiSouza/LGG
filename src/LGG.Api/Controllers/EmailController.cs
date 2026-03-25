using MediatR;
using Microsoft.AspNetCore.Mvc;
using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;
using Microsoft.AspNetCore.Authorization;
using LGG.Aplicacao.Library.Interfaces.IEmail;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

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
                    return NotFound(MsgSistemaEnum.NaoEncontrado.ObterDescricao());

                var (keyValida, emailConfirmacao) = _emailService.ValidarKey(Request.Query["key"].ToString());

                if (keyValida)
                {
                    var (usuarioValido, token) = await _mediator.Send(new ValidarUsuarioQuery(emailConfirmacao!.Id, emailConfirmacao.Email));

                    if (usuarioValido)
                        return Ok(token);
                }

                return NotFound(MsgSistemaEnum.NaoEncontrado.ObterDescricao());
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MsgSistemaEnum.ErroInterno.ObterDescricao());
            }
        }
    }
}
