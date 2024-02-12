using AutoMapper;
using EncurtadorLink.Services.Library.Models;
using EncurtadorLink.Services.Library.Extensoes;
using EncurtadorLink.Core.Library.Entidades.MySql;

namespace EncurtadorLink.Services.Library.Profiles
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
