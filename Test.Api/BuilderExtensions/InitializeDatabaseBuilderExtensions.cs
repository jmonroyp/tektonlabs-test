using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Test.Core.Dtos;
using Test.Core.Services;
using Test.Infraestructure.Entities;

namespace Test.Api.BuilderExtensions
{
    public static class InitializeDatabaseBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultProducts(this IApplicationBuilder app, ProductsService service) {
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDto>>(File.ReadAllText("data.json"));
            foreach (var p in items)
                service.InsertAsync(p).Wait();
            return app;
        }
    }
}