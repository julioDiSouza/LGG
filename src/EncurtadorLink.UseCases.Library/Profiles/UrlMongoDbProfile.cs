using AutoMapper;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Entities.Library.Mongo;

namespace EncurtadorLink.UseCases.Library.Profiles
{
    public class UrlMongoDbProfile : Profile
    {
        public UrlMongoDbProfile()
        {
            CreateMap<UrlMongoDb, UrlMongoDbModel>()
                .ReverseMap();
        }
    }
}