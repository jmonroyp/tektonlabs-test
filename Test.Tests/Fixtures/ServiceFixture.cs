using AutoMapper;
using Test.Core.Utils;
using Test.Infraestructure.Entities;

namespace Test.Tests.Fixtures
{
    public abstract class ServiceFixture<Entity> where Entity : BaseEntity
    {
        protected DbFixture<Entity> _db;
        protected MapperConfiguration _mapperConfig;

        public ServiceFixture() {
            _db = new DbFixture<Entity>();
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
        }
        
    }
}