using System;
using Test.Infraestructure.Entities;

namespace Test.Infraestructure.Specifications
{
     public class ProductByIdSpecification : Specification<Product>
    {
        public ProductByIdSpecification()
        {
            AddIncludes();
        }

        public ProductByIdSpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes() 
        {
            
        }
    }
}