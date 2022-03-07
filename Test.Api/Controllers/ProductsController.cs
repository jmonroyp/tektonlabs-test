using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Test.Core.Dtos;
using Test.Core.Services;

namespace Test.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, ProductsService productsService, IConfiguration config)
        {
            _logger = logger;
            _productsService = productsService;
            _configuration = config;
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        public async Task<List<ProductDto>> Get()
        {
            _logger.LogInformation("Get() executed without cache");
            return await _productsService.GetAllAsync();
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        [Route("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            _logger.LogInformation("Get() executed without cache");
            return await _productsService.GetByIdAsync(id, _configuration["External:StoreApi"]);
        }

        [HttpPost]
        public async Task Post(ProductDto product)
        {
            await _productsService.InsertAsync(product);
        }

        [HttpPost]
        [Route("many")]
        public async Task Post(List<ProductDto> products)
        {
            foreach (var p in products)
                await _productsService.InsertAsync(p);
        }

        [HttpPut]
        public async Task Put(ProductDto product)
        {
            await _productsService.UpdateAsync(product);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _productsService.RemoveByIdAsync(id);
        }
    }
}
