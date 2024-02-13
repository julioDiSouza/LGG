using AutoMapper;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Extensoes;

namespace EncurtadorLink.UseCases.Library.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteModelCriar, Cliente>()
                .ForMember(d => d.Nome, m => m.MapFrom(s => s.RazaoSocial))
                .ReverseMap();

            CreateMap<ClienteModelCriar, ApplicationUser>()
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.Email.ObterUserName()))
                .ReverseMap();
        }
    }
}
