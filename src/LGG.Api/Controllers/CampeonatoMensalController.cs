using MediatR;
using Microsoft.AspNetCore.Mvc;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using LGG.Aplicacao.Library.Helpers;
using Microsoft.AspNetCore.Authorization;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands;

namespace LGG.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class CampeonatoMensalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampeonatoMensalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo campeonato mensal
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] CampeonatoMensalCadastroDto campeonatoMensal)
        {
            var validacao = campeonatoMensal.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var retorno = await _mediator.Send(campeonatoMensal.ConverterComandoCadastrar());

            if (retorno is null)
                return StatusCode(500, "Falha ao cadastrar campeonato mensal");

            if (retorno?.Mensagem != null)
                return Conflict(retorno.Mensagem.Descricao);

            return Ok(retorno);
        }

        /* Feitos por .I.A verificar necessidade
        /// <summary>
        /// Obtém um campeonato mensal pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int id)
        {
            var campeonatoMensal = await _mediator.Send(new ObterCampeonatoMensalQuery { Id = id });

            return campeonatoMensal is null ? NotFound() : Ok(campeonatoMensal);
        }

        /// <summary>
        /// Obtém todos os campeonatos mensais
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ConsultarTodosAsync()
        {
            var campeonatos = await _mediator.Send(new ObterTodosCampeonatosQuery());

            return campeonatos != null && campeonatos.Any() ? Ok(campeonatos) : NotFound();
        }

        /// <summary>
        /// Obtém campeonatos mensais de um ano específico
        /// </summary>
        [HttpGet("ano/{ano}")]
        public async Task<IActionResult> ConsultarPorAnoAsync([FromRoute] int ano)
        {
            if (ano <= 0)
                return BadRequest("O ano deve ser maior que zero");

            var campeonatos = await _mediator.Send(new ObterCampeonatosPorAnoQuery { Ano = ano });

            return campeonatos != null && campeonatos.Any() ? Ok(campeonatos) : NotFound();
        }

        /// <summary>
        /// Obtém o campeonato mensal vigente (mês e ano atuais)
        /// </summary>
        [HttpGet("vigente")]
        public async Task<IActionResult> ConsultarVigenteAsync()
        {
            var campeonatoMensal = await _mediator.Send(new ObterCampeonatoVigenteQuery());

            return campeonatoMensal is null ? NotFound() : Ok(campeonatoMensal);
        }

        /// <summary>
        /// Atualiza um campeonato mensal
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync([FromRoute] int id, [FromBody] CampeonatoMensalAtualizarDto campeonatoMensal)
        {
            if (id <= 0)
                return BadRequest("O ID deve ser maior que zero");

            var validacao = campeonatoMensal.Validacao();

            if (!validacao.IsValid)
                return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

            var retorno = await _mediator.Send(campeonatoMensal.ConverterComandoAtualizar(id));

            return retorno is null ? NotFound() : Ok(retorno);
        }

        /// <summary>
        /// Deleta um campeonato mensal
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> ApagarAsync([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("O ID deve ser maior que zero");

            await _mediator.Send(new ApagarCampeonatoMensalCommand { Id = id });

            return NoContent();
        }
        */
    }
}
