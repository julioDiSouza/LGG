using AutoMapper;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Profiles
{
    public class UrlSqlProfile : Profile
    {
        public UrlSqlProfile()
        {
            CreateMap<UrlSql, UrlSqlModel>()
                .ReverseMap();
            CreateMap<UrlSql, UrlSqlModelGrid>()
                .ReverseMap();
        }
    }
}
