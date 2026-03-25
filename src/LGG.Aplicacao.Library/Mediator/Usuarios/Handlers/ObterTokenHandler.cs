using MediatR;
using System.Text;
using System.Security.Claims;
using LGG.Aplicacao.Library.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class ObterTokenHandler : IRequestHandler<ObterTokenQuery, UsuarioTokenRespostaDto>
    {
        private readonly IConfiguration _configuration;

        public ObterTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UsuarioTokenRespostaDto> Handle(ObterTokenQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => 
                GerarJwtToken(request.UsuarioId, request.Nome, request.Email)
            );
        }

        private UsuarioTokenRespostaDto GerarJwtToken(string usuarioId, string nome, string email)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, nome),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, "User")
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var minutos = int.Parse(jwtSettings["ExpireMinutes"]!);
            var expiresDate = DateTime.Now.AddMinutes(minutos);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiresDate,
                signingCredentials: creds
            );

            var tokenResposta = new UsuarioTokenRespostaDto
            {
                TokenType = "Bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresDate,
                ExpiresIn = minutos * 60
            };

            return tokenResposta;
        }
    }
}