using FluentValidation.Results;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Entidades;
using LGG.Aplicacao.Library.Validadores;
using LGG.Aplicacao.Library.Mediator.Canais.Commands;

namespace LGG.Aplicacao.Library.Helpers
{
    public static class CanalHelper
    {
        public static ValidationResult Validacao(this CanalValidacaoDto canal)
        {
            return new CanalValidacaoDtoValidador().Validate(canal);
        }

        public static ValidarCanalCommand ComandoValidar(this CanalValidacaoDto canal)
        {
            return new ValidarCanalCommand(canal.UsuarioId, canal.Nome);
        }

        public static CanalDto ConverterParaCanalDto(this Canal canal)
        {
            if (canal == null)
                return null;

            return new CanalDto
            {
                Id = canal.Id.ToString(),
                UsuarioId = canal.UsuarioId.ToString(),
                CanalId = canal.CanalId,
                Codigo = canal.Codigo,
                Nome = canal.Nome,
                UrlPersonalizada = canal.UrlPersonalizada,
                UrlImagem = canal.UrlImagem,
                Status = canal.Status,
                DataCadastro = canal.DataCadastro,
                Titulo = canal.Titulo
            };
        }
    }
}
