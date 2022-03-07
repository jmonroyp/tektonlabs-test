using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Test.Infraestructure.Database;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Repositories;

namespace Test.Tests.Fixtures
{
    public class DbFixture<Entity> where Entity : BaseEntity
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<StoreContext>(options => options.UseInMemoryDatabase(databaseName: "Store"));

            ServiceProvider = serviceCollection.BuildServiceProvider();
            Repository = new Repository<Entity>(ServiceProvider.GetService<StoreContext>());
        }

        public IRepository<Entity> Repository {get; private set;}

        public ServiceProvider ServiceProvider { get; private set; }
    }
}