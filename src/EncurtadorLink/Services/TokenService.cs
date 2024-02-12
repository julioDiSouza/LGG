using System.Text;
using System.Security.Claims;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Token;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entidades.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using EncurtadorLink.Infra.Extensoes;
using System.IdentityModel.Tokens.Jwt;
using EncurtadorLink.Model.Requisicao;
using EncurtadorLink.Services.Interface;
using EncurtadorLink.Infra.BancoDados.SQL;

namespace EncurtadorLink.Services
{
    public class TokenService : ITokenService
    {
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ContextoDb _dbContexto;
        private readonly string _chaveInterna;
        private readonly int _segundos;

        public TokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager, ContextoDb dbContexto)
        {
            _chaveInterna = configuration["TokenJWT:ChaveInterna"]!;
            _segundos = int.Parse(configuration["TokenJWT:Segundos"]!);
            _signInManager = signInManager;
            _dbContexto = dbContexto;
        }

        public (ITokenModel, bool) ObterTokenAplicacao(TokenRequisicaoModel requisicao, string ip)
        {
            var cliente = _dbContexto.Cliente
                .Include(c => c.Aplicacoes)
                .FirstOrDefault(c =>
                    c.Id.ToString() == requisicao.ClientId &&
                    c.Ativo == true
                );

            if (cliente is null || 
                cliente.Ativo is false || 
                cliente.Aplicacoes is null || 
                !cliente.Aplicacoes.Any(a => 
                a.ClienteSecret.ToString() == requisicao.ClientSecret && a.Ativo is true)
                )
                return (new TokenErroModel { Erro = "Cliente inválido.", Descricao = "Cliente ou credenciais inválidas." }, false);

            var tipo = cliente.Aplicacoes.FirstOrDefault(x => x.Ativo == true)!.Tipo;
            var claims = MontarClaims(cliente.Email.ObterUserName(), cliente.Id, tipo, ip);

            return (CriarToken(claims, issuer: tipo.ToString()), true);
        }

        public async Task<(ITokenModel, bool)> ObterTokenLoginAsync(LoginRequisicaoModel requisicao, string ip)
        {
            var resultado = await _signInManager
                .PasswordSignInAsync(requisicao.Email.ObterUserName(), requisicao.Senha, false, false);

            if (resultado.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(u => u.NormalizedUserName == requisicao.Email.ObterUserName().ToUpper());

                var claims = MontarClaims(identityUser.UserName, identityUser.ClienteId, ClienteAplicacaoTipo.WEB, ip);

                return (CriarToken(claims, issuer: ClienteAplicacaoTipo.WEB.ToString()), true);
            }

            return (new TokenErroModel { Erro = "Usuário inválido.", Descricao = "Usuário ou senha inválida." }, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region ..:: Privados ::..

        private TokenModel CriarToken(Claim[] claims, string issuer)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveInterna));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credenciais,
                expires: DateTime.Now.AddSeconds(_segundos),
                issuer: issuer,
                audience: "EncurtadorLink"
                );

            return new TokenModel
            {
                TokenType = "Bearer",
                ExpiresIn = _segundos,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private Claim[] MontarClaims(string username, Guid clienteId, ClienteAplicacaoTipo tipo, string ip)
        {
            switch (tipo)
            {
                case ClienteAplicacaoTipo.WEB:
                case ClienteAplicacaoTipo.APP:
                case ClienteAplicacaoTipo.API:
                    return new Claim[]
                    {
                        new Claim("username", username),
                        new Claim("id", clienteId.ToString()),
                        new Claim("ip", ip)
                    };
                default:
                    return new Claim[]
                    {
                        new Claim("ip", ip)
                    };
            }
        }

        #endregion ..:: Fim Privados ::..

    }
}
