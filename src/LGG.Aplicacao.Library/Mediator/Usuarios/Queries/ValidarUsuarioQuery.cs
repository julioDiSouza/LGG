using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Queries
{
    public class ValidarUsuarioQuery : IRequest<(bool, UsuarioTokenRespostaDto?)>
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public ValidarUsuarioQuery(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}