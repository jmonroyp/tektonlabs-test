using System.Linq;
using System.Threading.Tasks;
using Test.Infraestructure.Specifications;
using Test.Tests.Fixtures;
using Xunit;

namespace Test.Tests
{
    public class ProductsServicesTest : IClassFixture<ProductServiceFixture>
    {

        private ProductServiceFixture _productService;
         public ProductsServicesTest(ProductServiceFixture service)
        {
            _productService = service;
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var res = await _productService.ProductsService.GetAllAsync();
            Assert.Equal(20, res.Count());
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            var res = await _productService.ProductsService.GetByIdAsync(new ProductByIdSpecification(1));
            Assert.NotNull(res);
        }

        [Fact]
        public async Task RemoveAsync()
        {
            int id = 15;
            await _productService.ProductsService.RemoveByIdAsync(new ProductByIdSpecification(id));
            var res = await _productService.ProductsService.GetByIdAsync(new ProductByIdSpecification(id));
            Assert.Null(res);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            string newTitle = "Updated title!!";
            int id = 2;
            var res = await _productService.ProductsService.GetByIdAsync(new ProductByIdSpecification(id));
            res.Title = newTitle;
            await _productService.ProductsService.UpdateAsync(res, new ProductByIdSpecification(id));
            var res2 = await _productService.ProductsService.GetByIdAsync(new ProductByIdSpecification(id));
            Assert.Equal(res2.Title, newTitle);
        }
    }
}
