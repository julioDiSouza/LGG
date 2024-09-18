using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Classes;
using EncurtadorLink.Shared.Library.Extensoes;
using EncurtadorLink.Shared.Library.Enumeradores;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnidadeTrabalho _udt;
        private readonly ILogger<LogGenerico> _logger;
        private readonly string _chaveInterna;
        private readonly int _segundos;
        private readonly int _segundosRefresh;
        private readonly string _cryptoKey;

        public TokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IUnidadeTrabalho udt, ILogger<LogGenerico> logger)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _udt = udt;
            _logger = logger;
            _chaveInterna = configuration["TokenJWT:ChaveInterna"]!;
            _segundos = int.Parse(configuration["TokenJWT:Segundos"]!);
            _segundosRefresh = int.Parse(configuration["TokenJWT:SegundosRefresh"]!);
            _cryptoKey = configuration["CryptoKey"]!;
        }

        public (ITokenModel, bool) ObterTokenAplicacao(TokenModelRequisicao requisicao)
        {
            var cliente = _udt.ClienteRepositorio.ConsultarClientePorId(requisicao.ClientId);

            if (cliente is null ||
                cliente.Ativo is false ||
                cliente.Aplicacoes is null ||
                !cliente.Aplicacoes.Any(a =>
                a.ClienteSecret.ToString() == requisicao.ClientSecret && a.Ativo is true)
                )
                return (new TokenErroModel { Status = 400, Erro = "Cliente inválido.", Detail = "Cliente ou credenciais inválidas." }, false);

            var clienteAplicacao = cliente.Aplicacoes.FirstOrDefault(x => x.Ativo == true)!;
            var claims = CriarClaims(cliente.Email.ObterUserName(), cliente.NomeCompleto.ObterPrimeiroNome() , cliente.Id, clienteAplicacao.Role, nameof(LoginTipoEnum.Api));
            var tokenJWT = CriarTokenJWT(claims, _segundos);

            return (MontarTokenModel(tokenJWT), true);
        }

        public async Task<(ITokenModel, bool)> ObterTokenLoginAsync(LoginModelRequisicao requisicao)
        {
            var resultado = await _signInManager
                .PasswordSignInAsync(requisicao.Email.ObterUserName(), requisicao.Senha, false, false);

            if (resultado.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(u => u.NormalizedUserName == requisicao.Email.ObterUserName().ToUpper());

                if (!identityUser!.EmailConfirmed)
                    return UsuarioInvalido();

                var claims = CriarClaims(identityUser!.UserName!, identityUser!.NomeCompleto.ObterPrimeiroNome()!, identityUser.ClienteId, identityUser.Role, nameof(LoginTipoEnum.Email));
                var tokenJWT = CriarTokenJWT(claims, _segundosRefresh);

                identityUser.RefreshToken = CriarRefreshToken();
                identityUser.RefreshTokenDataExpiracao = DateTime.Now.AddSeconds(_segundos);

                await _userManager.UpdateAsync(identityUser);

                return (MontarTokenModel(tokenJWT, identityUser.RefreshToken), true);
            }

            return UsuarioInvalido();
        }

        public async Task<(ITokenModel, bool)> ObterTokenSsoAsync(LoginModelSso model, IClienteService clienteService)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedEmail == model.Email.ToUpper());

            if (identityUser == null)
            {
                var senha = CriarRefreshToken();

                var clienteModel = new ClienteModelCriar
                {
                    NomeCompleto = model.Nome ?? model.Name,
                    Email = model.Email,
                    Senha = senha,
                    ReSenha = senha
                };

                var sucesso = await clienteService.CadastrarAsync(clienteModel);

                identityUser = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedEmail == model.Email.ToUpper());
            }

            var claims = CriarClaims(identityUser!.UserName!, identityUser!.NomeCompleto.ObterPrimeiroNome()!, identityUser.ClienteId, identityUser.Role, nameof(LoginTipoEnum.Google));
            var tokenJWT = CriarTokenJWT(claims, _segundosRefresh);

            identityUser.RefreshToken = CriarRefreshToken();
            identityUser.RefreshTokenDataExpiracao = DateTime.Now.AddSeconds(_segundos);

            await _userManager.UpdateAsync(identityUser);

            return (MontarTokenModel(tokenJWT, identityUser.RefreshToken), true);
        }

        public async Task<(ITokenModel, bool)> ObterRefreshTokenAsync(TokenRefreshModelRequisicao requisicao)
        {
            try
            {
                var principal = ObterPrincipioTokenExpirado(requisicao.AccessToken);
                var username = principal.FindFirst("name")!.Value;
                var loginType = principal.FindFirst("logintype")!.Value;
                var id = principal.FindFirst("id")!.Value;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

                requisicao.RefreshToken = Criptografador.DecryptString(requisicao.RefreshToken, _cryptoKey);

                if (user != null && user.RefreshToken == requisicao.RefreshToken && 
                    user.RefreshTokenDataExpiracao > DateTime.Now && user.ClienteId.ToString() == id)
                {
                    user.RefreshToken = CriarRefreshToken();
                    user.RefreshTokenDataExpiracao = DateTime.Now.AddSeconds(_segundos);

                    var claims = CriarClaims(user.UserName!, user.NomeCompleto.ObterPrimeiroNome(), user.ClienteId, user.Role, loginType);
                    var tokenJWT = CriarTokenJWT(claims, _segundosRefresh);

                    await _userManager.UpdateAsync(user);

                    return (MontarTokenModel(tokenJWT, user.RefreshToken), true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}.");
            }

            return (new TokenErroModel { Status = 400, Erro = "Token inválido.", Detail = "Falha ao atualizar o token." }, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region ..:: Privados ::..

        private ITokenModel MontarTokenModel(JwtSecurityToken? tokenJWT, string? refreshToken = null)
        {
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenJWT);

            if (refreshToken is null)
                return new TokenModel(accessToken, _segundos);

            return new RefreshTokenModel(accessToken, refreshToken);
        }

        private JwtSecurityToken CriarTokenJWT(Claim[] claims, int segundos)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveInterna));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            return 
                new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credenciais,
                    expires: DateTime.Now.AddSeconds(segundos),
                    issuer: "EncurtadorLink",
                    audience: "EncurtadorLink"
                );
        }

        private Claim[] CriarClaims(string nome, string primeiroNome, Guid clienteId, string role, string tipoLogin)
        {
            return new Claim[]
            {
                new Claim("name", nome),
                new Claim("firstname", primeiroNome),
                new Claim("role", role),
                new Claim("id", clienteId.ToString()),
                new Claim("loginkey", Convert.ToBase64String(Criptografador.Encrypt(nome, _cryptoKey))),
                new Claim("logintype", tipoLogin)
            };
        }

        private string CriarRefreshToken()
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var emUso = _signInManager
                .UserManager
                .Users
                .Any(x => x.RefreshToken == refreshToken);

            if (emUso)
                return CriarRefreshToken();

            return refreshToken;
        }

        private ClaimsPrincipal ObterPrincipioTokenExpirado(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = _configuration.GetSection("TokenJWT:Issuers").Get<List<string>>(),
                ValidateAudience = true,
                ValidAudiences = _configuration.GetSection("TokenJWT:Audiences").Get<List<string>>(),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveInterna)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Problema no RefreshToken");

            return principal;
        }

        private (TokenErroModel, bool) UsuarioInvalido() =>
            (new TokenErroModel { Status = 400, Erro = "Usuário inválido.", Detail = "Usuário ou senha inválida." }, false);

        #endregion ..:: Fim Privados ::..
    }
}
