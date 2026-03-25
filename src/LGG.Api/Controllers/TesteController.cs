using Microsoft.AspNetCore.Mvc;
using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;
using Microsoft.AspNetCore.Authorization;

namespace LGG.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        [HttpGet("teste_publico")]
        public IActionResult Public() => Ok(MsgSistemaEnum.ApiFuncionando.ObterDescricao());

        [Authorize]
        [HttpGet("teste_authorized")]
        public IActionResult Protected() => Ok(MsgSistemaEnum.ApiFuncionando.ObterDescricao());
    }
}