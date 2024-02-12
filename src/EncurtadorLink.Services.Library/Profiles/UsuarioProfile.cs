using AutoMapper;
using EncurtadorLink.Services.Library.Models;
using EncurtadorLink.Core.Library.Entidades.MySql;

namespace EncurtadorLink.Services.Library.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioModelCriar, ApplicationUser>()
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.NomeUsuario))
                .ReverseMap();
        }
    }
}
