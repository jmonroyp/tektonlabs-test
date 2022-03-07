using System.Collections.Generic;
using System.IO;
using Test.Core.Dtos;
using Test.Core.Services;
using Test.Infraestructure.Entities;


namespace Test.Tests.Fixtures
{
    public class ProductServiceFixture : ServiceFixture<Product>
    {
        public ProductsService ProductsService { get; private set; }
        public ProductServiceFixture() : base()
        {          
            ProductsService = new ProductsService(_db.Repository, _mapperConfig.CreateMapper());
                        var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDto>>(File.ReadAllText("../../../data.json"));
            foreach (var p in items)
                 ProductsService.InsertAsync(p).Wait();
        }
    }
}