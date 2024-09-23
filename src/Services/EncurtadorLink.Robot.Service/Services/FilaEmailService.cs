using AutoMapper;
using EncurtadorLink.Robot.Service.Services.Interfaces;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Robot.Service.Services
{
    public class FilaEmailService : IFilaEmailService
    {
        private readonly IIEmailService _emailService;
        private readonly IMapper _mapper;

        public FilaEmailService(IIEmailService emailService, IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task ProcessarFilaAsync() => 
            await _emailService.ProcessarFilaAsync();
    }
}
