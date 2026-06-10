using FluentValidation.Results;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Aplicacao.Library.Validadores;
using LGG.Aplicacao.Library.Mediator.Usuarios.Commands;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Helpers
{
    public static class UsuarioHelper
    {
        public static ValidationResult Validacao(this UsuarioCadastroDto usuario)
        {
            return new UsuarioCadastroDtoValidador().Validate(usuario);
        }

        public static ValidationResult Validacao(this UsuarioAtualizarDto usuario)
        {
            return new UsuarioAtualizarDtoValidador().Validate(usuario);
        }

        public static ValidationResult Validacao(this UsuarioLoginRequisicaoDto usuario)
        {
            return new UsuarioLoginRequisicaoDtoValidador().Validate(usuario);
        }

        public static CadastrarUsuarioCommand ConverterComandoCadastrar(this UsuarioCadastroDto usuario)
        {
            return new CadastrarUsuarioCommand
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Sexo = usuario.Sexo,
                DataNascimento = usuario.DataNascimento,
                Tipo = UsuarioTipoEnum.Usuario,
                Status = UsuarioStatusEnum.Pendente,
                Senha = usuario.Senha
            };
        }

        public static AtualizarUsuarioCommand ConverterComandoAtualizar(this UsuarioAtualizarDto usuario, string id)
        {
            var command = new AtualizarUsuarioCommand();
            command.UsuarioId = id;
            command.Nome = usuario.Nome;
            command.Sexo = usuario.Sexo;
            command.DataNascimento = usuario.DataNascimento;

            return command;
        }

        public static UsuarioConsultaDto ConverterParaUsuarioConsultaDto(this Usuario usuario)
        {
            if (usuario is null)
                return null;

            var dto = new UsuarioConsultaDto();
            dto.Id = usuario.Id.ToString();
            dto.Nome = usuario.Nome;
            dto.Email = usuario.Email;
            dto.Sexo = usuario.Sexo;
            dto.DataNascimento = usuario.DataNascimento;
            dto.Tipo = usuario.Tipo;
            dto.Status = usuario.Status;
            dto.CodigoVideo = $"[LGG:{usuario.CodigoVideo}]";

            return dto;
        }

        public static List<UsuarioConsultaDto> ConverterParaUsuariosConsultaDto(this List<Usuario> usuarios)
        {
            if (usuarios == null || !usuarios.Any())
                return null;

            var usuariosDto = new List<UsuarioConsultaDto>();

            foreach (var usuario in usuarios)
                usuariosDto.Add(usuario.ConverterParaUsuarioConsultaDto());

            return usuariosDto;
        }

        public static ObterUsuarioQuery ConverterUsuarioQuery(this UsuarioLoginRequisicaoDto usuario)
        {
            if (usuario == null)
                return null;

            var query = new ObterUsuarioQuery();
            query.Email = usuario.Email;
            query.Senha = usuario.Senha;

            return query;
        }

        public static ObterUsuarioTokenQuery ConverterUsuarioTokenQuery(this UsuarioLoginRequisicaoDto usuario)
        {
            if (usuario == null)
                return null;

            var query = new ObterUsuarioTokenQuery();
            query.Email = usuario.Email;
            query.Senha = usuario.Senha;

            return query;
        }
    }
}
