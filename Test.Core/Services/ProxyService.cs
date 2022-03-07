using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Test.Core.External;
using Test.Infraestructure.Specifications;

namespace Test.Core.Services
{
    public abstract class ProxyService<Dto, ExternalDto> : IProxyService<Dto, ExternalDto>
    {
        protected string _api {get; set;}
        private readonly IMapper _mapper;
        public ProxyService(IMapper mapper) {
            _mapper = mapper;
        }

        public ProxyService<Dto, ExternalDto> SetApi(string api) {
            _api = api;
            return this;
        }

        public async Task<List<Dto>> GetAllAsync(string path)
        {
            return _mapper.Map<List<ExternalDto>, List<Dto>>(await HttpClientWrapper<List<ExternalDto>>.Get($"{_api}{path}"));
        }

        public async Task<Dto> GetByIdAsync(string path)
        {
             return _mapper.Map<ExternalDto, Dto>(await HttpClientWrapper<ExternalDto>.Get($"{_api}{path}"));
        }
    }
}