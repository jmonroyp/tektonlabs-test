using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Test.Core.Dtos;
using Test.Core.External;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Repositories;
using Test.Infraestructure.Specifications;

namespace Test.Core.Services
{
    public class ProductsService : IDbService<ProductDto>
    {
        private readonly IRepository<Product> _repo;
        private readonly IMapper _mapper;

        public ProductsService(IRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return _mapper
            .Map<List<Product>, List<ProductDto>>(await _repo.GetAllAsync(null));
        }

        public async Task<ProductDto> GetByIdAsync(int id, string storeApi)
        {
            var product = _mapper.Map<Product, ProductDto>(await _repo.GetAsync(new ProductByIdSpecification(id)));
            if (product != null)
            {
                var extItem = await HttpClientWrapper<ExternalProductDetailDto>.Get($"{storeApi}products/{id}");
                if (extItem != null)
                {
                    product.Detail = _mapper.Map<ExternalProductDetailDto, ProductDetail>(extItem);
                }
            }

            return product;
        }

        public async Task InsertAsync(ProductDto dto)
        {
            await _repo.InsertAsync(_mapper.Map<ProductDto, Product>(dto));
        }

        public async Task UpdateAsync(ProductDto dto)
        {
            await _repo
            .UpdateAsync(_mapper.Map<ProductDto, Product>(dto), new ProductByIdSpecification(dto.Id));
        }

        public async Task RemoveByIdAsync(int id)
        {
            await _repo.RemoveAsync(new ProductByIdSpecification(id));
        }
    }
}