using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Mensagem;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using Microsoft.Extensions.Configuration;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Usuarios.Commands;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class CadastrarUsuarioHandler : IRequestHandler<CadastrarUsuarioCommand, UsuarioCadastroRetornoDto>
    {
        private readonly IUnidTrabalho _ut;
        private readonly int _tamanho;
        private readonly string _caracteres;

        public CadastrarUsuarioHandler(IConfiguration configuration, IUnidTrabalho ut)
        {
            _tamanho = int.Parse(configuration["ConfiguracaoCodigoUnico:Tamanho"]!);
            _caracteres = configuration["ConfiguracaoCodigoUnico:Caracteres"]!;
            _ut = ut;
        }

        public async Task<UsuarioCadastroRetornoDto> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _ut.UsuarioRepositorio.ObterPorEmailAsync(request.Email);

            try
            {
                if (usuario != null)
                {
                    var usuarioExistente = new UsuarioCadastroRetornoDto();
                    usuarioExistente.Mensagem = new Mensagem(SistemaEnum.EmailJaCadastrado);

                    return usuarioExistente;
                }

                usuario = new Usuario().Cadastrar(
                        request.Nome, request.Email, request.Sexo, request.DataNascimento,
                        request.Tipo, request.Status, request.Senha, await ObterCodigoUnicoAsync()
                    );

                await _ut.UsuarioRepositorio.AdicionarAsync(usuario);

                var emailBalde = new EmailBalde();
                emailBalde.UsuarioId = usuario.Id;
                emailBalde.Email = usuario.Email;
                emailBalde.Tipo = EmailEnum.Confirmacao;
                await _ut.EmailBaldeRepositorio.AdicionarAsync(emailBalde);

                var usuarioDto = new UsuarioCadastroRetornoDto();
                usuarioDto.UsuarioId = usuario.Id.ToString();
                usuarioDto.Nome = usuario.Nome;
                usuarioDto.Email = usuario.Email;
                usuarioDto.Sexo = usuario.Sexo;
                usuarioDto.DataNascimento = usuario.DataNascimento;
                usuarioDto.Tipo = usuario.Tipo;
                usuarioDto.Status = usuario.Status;
                usuarioDto.Mensagem = new Mensagem(SistemaEnum.Sucesso);

                return usuarioDto;
            }
            catch (Exception)
            {
                await _ut.UsuarioRepositorio.RemoverDefinitivoPorIdAsync(usuario!.Id);

                throw;
            }
        }

        private async Task<string> ObterCodigoUnicoAsync()
        {
            Random random = new();
            var arrCodigo = new char[_tamanho];

            while (true)
            {
                for (int i = 0; i < _tamanho; i++)
                {
                    var indexAleatorio = random.Next(_caracteres.Length);

                    arrCodigo[i] = _caracteres[indexAleatorio];
                }

                var codigo = new string(arrCodigo);

                if (await _ut.UsuarioRepositorio.CodigoLiberadoAsync(codigo))
                    return codigo;
            }
        }
    }
}