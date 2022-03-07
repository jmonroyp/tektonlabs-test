using Microsoft.EntityFrameworkCore;

namespace Test.Infraestructure.Database
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options) { }

        public DbSet<Entities.Product> Products { get; set; }
    }
}