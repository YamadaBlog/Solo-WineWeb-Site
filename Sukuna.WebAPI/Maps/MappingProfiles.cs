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
            CreateMap<Client,  ClientResource>();
            CreateMap<ClientResource, Client>();
            CreateMap<User, UserResource>();
            CreateMap<UserResource, User>();
            CreateMap<OrderResource, OrderLine>();
            CreateMap<OrderLine, OrderResource>();
            CreateMap<Supplier, SupplierResource>();
            CreateMap<SupplierResource, Supplier>();
        }
    }
}