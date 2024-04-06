using AutoMapper;
using Sukuna.Common.Resources;
using Sukuna.Common.Models;

namespace Sukuna.WebAPI.Maps
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Article, ArticleResource>();
            CreateMap<TvaTypeResource, TvaType>();
            CreateMap<ArticleResource, Article>();
            CreateMap<TvaType, TvaTypeResource>();
        }
    }
}