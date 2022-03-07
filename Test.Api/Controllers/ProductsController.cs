using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Test.Core.Dtos;
using Test.Core.External;
using Test.Core.Services;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Specifications;

namespace Test.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductsService _productsService;
        private readonly ProductsProxyService _productsProxyService;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger,
        ProductsService productsService,
        ProductsProxyService productsProxyService,
        IConfiguration config,
        IMapper mapper)
        {
            _logger = logger;
            _productsService = productsService;
            _configuration = config;
            _mapper = mapper;
            _productsProxyService = productsProxyService.SetApi(_configuration["External:StoreApi"]) as ProductsProxyService;
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        public async Task<List<ProductDto>> Get()
        {
            _logger.LogInformation("Get() executed without cache");
            var products = await _productsService.GetAllAsync();
            if (products.Any())
            {
                var details = await _productsProxyService.GetAllAsync($"products");
                products.ForEach(c => c.Detail = details.FirstOrDefault(d => d.Id == c.Id));
            }
            return products;
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        [Route("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            _logger.LogInformation("Get() executed without cache");
            var product = await _productsService.GetByIdAsync(new ProductByIdSpecification(id));
            if (product != null)
                product.Detail = await _productsProxyService.GetByIdAsync($"products/{product.Id}");
            return product;
        }

        [HttpPost]
        public async Task Post(ProductDto product)
        {
            await _productsService.InsertAsync(product);
        }

        [HttpPut]
        public async Task Put(ProductDto product)
        {
            await _productsService.UpdateAsync(product, new ProductByIdSpecification(product.Id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _productsService.RemoveByIdAsync(new ProductByIdSpecification(id));
        }
    }
}
