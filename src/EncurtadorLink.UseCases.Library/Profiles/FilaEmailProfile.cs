using AutoMapper;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Profiles
{
    public class FilaEmailProfile : Profile
    {
        public FilaEmailProfile()
        {
            CreateMap<FilaEmailModel, FilaEmail>().ReverseMap();
        }
    }
}
