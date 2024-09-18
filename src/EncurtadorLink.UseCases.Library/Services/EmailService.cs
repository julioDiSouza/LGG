using AutoMapper;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class EmailService : IIEmailService
    {
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;

        public EmailService(IUnidadeTrabalho udt, IMapper mapper)
        {
            _udt = udt;
            _mapper = mapper;
        }

        public async Task CriarFilaEmailAsync(FilaEmailModel model)
        {
            var filaEmail = _mapper.Map<FilaEmail>(model);

            await _udt.FilaEmailRepositorio.AdicionarAsync(filaEmail);
        }
    }
}