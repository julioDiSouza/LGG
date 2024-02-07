using AutoMapper;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Usuario;

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
                .ForMember(d => d.UserName, m => m.MapFrom(s => ObterUserName(s.Email)))
                .ReverseMap();
        }

        private string ObterUserName(string email)
        {
            var arr = email.Split('@');
            return arr[0];
        }
    }
}
