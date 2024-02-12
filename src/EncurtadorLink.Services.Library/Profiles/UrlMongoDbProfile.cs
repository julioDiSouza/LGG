using AutoMapper;
using EncurtadorLink.Services.Library.Models;
using EncurtadorLink.Core.Library.Entidades.MongoDb;

namespace EncurtadorLink.Services.Library.Profiles
{
    public class UrlMongoDbProfile : Profile
    {
        public UrlMongoDbProfile()
        {
            CreateMap<UrlMongoDB, UrlMongoDbModel>()
                .ReverseMap();
        }
    }
}