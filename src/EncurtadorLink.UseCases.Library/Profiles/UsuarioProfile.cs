using AutoMapper;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Profiles
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
