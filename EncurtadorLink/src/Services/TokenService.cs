using System.Text;
using System.Security.Claims;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Token;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entidades.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using EncurtadorLink.Infra.BancoDados;
using EncurtadorLink.Services.Interface;

namespace EncurtadorLink.Services
{
    public class TokenService : ITokenService
    {
        private readonly ContextoDb _dbContexto;
        private readonly string _chaveInterna;
        private readonly int _segundos;

        public TokenService(IConfiguration configuration, ContextoDb dbContexto)
        {
            _chaveInterna = configuration["TokenJWT:ChaveInterna"]!;
            _segundos = int.Parse(configuration["TokenJWT:Segundos"]!);
            _dbContexto = dbContexto;
        }

        public (ITokenModel, bool) ObterTokenJWT(TokenRequisicao requisicao, string ip)
        {
            var cliente = _dbContexto.Cliente
                .Include(c => c.Aplicacoes)
                .FirstOrDefault(c =>
                    c.Id.ToString() == requisicao.ClientId &&
                    c.Ativo == true
                );

            if (cliente is null || 
                cliente.Aplicacoes is null || 
                !cliente.Aplicacoes.Any(a => 
                a.ClienteSecret.ToString() == requisicao.ClientSecret && a.Ativo is true)
                )
                return (new TokenErroModel { Erro = "Cliente inválido.", Descricao = "Cliente ou credenciais inválidas." }, false);

            var tipo = cliente.Aplicacoes.FirstOrDefault(x => x.Ativo == true)!.Tipo;
            var claims = MontarClaims(cliente, tipo, ip);

            return (CriarToken(claims, issuer: tipo.ToString()), true);
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

        private Claim[] MontarClaims(Cliente cliente, ClienteAplicacaoTipo tipo, string ip)
        {
            switch (tipo)
            {
                case ClienteAplicacaoTipo.APP:
                case ClienteAplicacaoTipo.API:
                    return new Claim[]
                    {
                        new Claim("username", cliente.Nome),
                        new Claim("id", cliente.Id.ToString()),
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
