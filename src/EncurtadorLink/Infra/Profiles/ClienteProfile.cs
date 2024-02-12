using AutoMapper;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Usuario;
using EncurtadorLink.Infra.Extensoes;

namespace EncurtadorLink.Infra.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteDtoCriar, Cliente>()
                .ForMember(d => d.Nome, m => m.MapFrom(s => s.RazaoSocial))
                .ForMember(d => d.Nome, m => m.MapFrom(s => s.RazaoSocial))
                .ReverseMap();

            CreateMap<ClienteDtoCriar, ApplicationUser>()
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.Email.ObterUserName()))
                .ReverseMap();
        }
    }
}
