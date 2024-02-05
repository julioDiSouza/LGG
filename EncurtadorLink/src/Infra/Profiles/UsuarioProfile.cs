using AutoMapper;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Usuario;

namespace EncurtadorLink.Infra.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDtoCriar, ApplicationUser>().ReverseMap();
        }
    }
}
