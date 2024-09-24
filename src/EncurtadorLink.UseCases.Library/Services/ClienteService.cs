using AutoMapper;
using System.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Classes;
using EncurtadorLink.Shared.Library.Enumeradores;
using EncurtadorLink.Entities.Library.Enumeradores;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public ClienteService(IUnidadeTrabalho udt, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _udt = udt;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<List<ClienteModel>?> ListarClientesAsync()
        {
            var entidades = await _udt.ClienteRepositorio.ListarClientesAsync();

            return _mapper.Map<List<ClienteModel>>(entidades) ?? null;
        }

        public async Task<bool> CadastroViaWebAsync(ClienteModelCriar clienteCriar)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == clienteCriar.Email.ToUpper());

            if (user == null)
            {
                await CadastrarAsync(clienteCriar);
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == clienteCriar.Email.ToUpper());
            }

            if (user.EmailConfirmed)
                return true;

            var filaEmail = new FilaEmail();
            filaEmail.Id = Guid.NewGuid();
            filaEmail.ClienteId = user.ClienteId;
            filaEmail.Email = clienteCriar.Email;
            filaEmail.ConfirmationToken = HttpUtility.UrlEncode(Criptografador.EncryptToString(
                "{ \"email\":\"" + clienteCriar.Email + "\", \"id\":\"" + user.ClienteId + "\", \"status\":\"__valido__\"}", 
                _configuration["CryptoKey"]!));
            filaEmail.DataInclusao = Criptografador.BrazilDateTime();
            filaEmail.Tipo = (int)FilaEmailEnum.Confirmacao;

            await _udt.FilaEmailRepositorio.AdicionarAsync(filaEmail);
            await _udt.FilaEmailRepositorio.ApagarAnterioresRawAsync(filaEmail);

            return true;
        }

        public async Task<bool> ValidarClienteAsync(ClienteModelCodigo cliente)
        {
            var json = Criptografador.DecryptString(cliente.Codigo, _configuration["CryptoKey"]!);

            if (!json.Contains("__valido__"))
                return false;

            var model = JsonSerializer.Deserialize<ClienteModelValidar>(json);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model!.email && u.ClienteId == model.id);

            if (user == null)
                return false;

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<bool> CadastrarAsync(ClienteModelCriar clienteCriar, bool emailConfirmed = false)
        {
            var dataAgora = DateTime.Now;

            var cliente = _mapper.Map<Cliente>(clienteCriar);
            cliente!.Id = Guid.NewGuid();
            cliente.Ativo = true;
            cliente.DataInclusao = dataAgora;

            var usuarioIdentity = _mapper.Map<ApplicationUser>(clienteCriar);
            usuarioIdentity!.ClienteId = cliente.Id;
            usuarioIdentity.Ativo = true;
            usuarioIdentity.DataInclusao = dataAgora;
            usuarioIdentity.Role = nameof(RoleEnum.Admin);
            usuarioIdentity.EmailConfirmed = emailConfirmed;

            var clienteAplicacao = new ClienteAplicacao();
            clienteAplicacao.ClienteId = cliente.Id;
            clienteAplicacao.ClienteSecret = Guid.NewGuid();
            clienteAplicacao.Nome = clienteCriar.NomeCompleto;
            clienteAplicacao.Ativo = true;
            clienteAplicacao.DataInclusao = dataAgora;
            clienteAplicacao.Role = nameof(RoleEnum.Api);

            await _udt.ClienteRepositorio.AdicionarAsync(cliente);

            await _udt.ClienteAplicacaoRepositorio.AdicionarAsync(clienteAplicacao);

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity!, clienteCriar.Senha);

            return resultadoIdentity.Succeeded;
        }
    }
}
