using System;

namespace Test.Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public ProductDetail Detail { get; set; }
    }
}