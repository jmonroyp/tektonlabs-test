using AutoMapper;
using Test.Core.Dtos;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Repositories;

namespace Test.Core.Services
{
    public class ProductsService : DbService<ProductDto, Product>
    {
        public ProductsService(IRepository<Product> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }

    public class ProductsProxyService : ProxyService<ProductDetail, ExternalProductDetailDto>
    {
        public ProductsProxyService(IMapper mapper) : base(mapper)
        {
        }
    }
}