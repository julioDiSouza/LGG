using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Extensoes;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class TokenService : ITokenService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnidadeTrabalho _udt;
        private readonly string _chaveInterna;
        private readonly int _segundos;

        public TokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IUnidadeTrabalho udt)
        {
            _chaveInterna = configuration["TokenJWT:ChaveInterna"]!;
            _segundos = int.Parse(configuration["TokenJWT:Segundos"]!);
            _signInManager = signInManager;
            _udt = udt;
        }

        public (ITokenModel, bool) ObterTokenAplicacao(TokenModelRequisicao requisicao, string ip)
        {
            var cliente = _udt.ClienteRepositorio.ConsultarClientePorIdAsync(requisicao.ClientId);

            if (cliente is null ||
                cliente.Ativo is false ||
                cliente.Aplicacoes is null ||
                !cliente.Aplicacoes.Any(a =>
                a.ClienteSecret.ToString() == requisicao.ClientSecret && a.Ativo is true)
                )
                return (new TokenErroModel { Erro = "Cliente inválido.", Descricao = "Cliente ou credenciais inválidas." }, false);

            var clienteAplicacao = cliente.Aplicacoes.FirstOrDefault(x => x.Ativo == true)!;
            var claims = MontarClaims(cliente.Email.ObterUserName(), cliente.Id, clienteAplicacao.TipoRole, ip);

            return (CriarToken(claims), true);
        }

        public async Task<(ITokenModel, bool)> ObterTokenLoginAsync(LoginModelRequisicao requisicao, string ip)
        {
            var resultado = await _signInManager
                .PasswordSignInAsync(requisicao.Email.ObterUserName(), requisicao.Senha, false, false);

            if (resultado.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(u => u.NormalizedUserName == requisicao.Email.ObterUserName().ToUpper());

                var claims = MontarClaims(identityUser!.UserName!, identityUser.ClienteId, identityUser.TipoRole, ip);

                return (CriarToken(claims), true);
            }

            return (new TokenErroModel { Erro = "Usuário inválido.", Descricao = "Usuário ou senha inválida." }, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region ..:: Privados ::..

        private TokenModel CriarToken(Claim[] claims)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveInterna));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credenciais,
                expires: DateTime.Now.AddSeconds(_segundos),
                issuer: "EncurtadorLink",
                audience: "EncurtadorLink"
                );

            return new TokenModel
            {
                TokenType = "Bearer",
                ExpiresIn = _segundos,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private Claim[] MontarClaims(string username, Guid clienteId, string role, string ip)
        {
            return new Claim[]
            {
                new Claim("name", username),
                new Claim("role", role),
                new Claim("id", clienteId.ToString()),
                new Claim("ip", ip)
            };
        }

        #endregion ..:: Fim Privados ::..
    }
}
