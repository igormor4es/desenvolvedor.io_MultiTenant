using Microsoft.EntityFrameworkCore;
using MultiTenant.Domain;
using MultiTenant.Provider;

namespace MultiTenant.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        private readonly TenantData _tenant;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, TenantData tenant) : base(options)
        {
            _tenant = tenant;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Person 1", TenantId = "tenant-1" },
                new Person { Id = 2, Name = "Person 2", TenantId = "tenant-2" },
                new Person { Id = 2, Name = "Person 3", TenantId = "tenant-2" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Description = "Description 1", TenantId = "tenant-1" },
                new Product { Id = 2, Description = "Description 2", TenantId = "tenant-2" },
                new Product { Id = 2, Description = "Description 3", TenantId = "tenant-2" }
            );

            modelBuilder.Entity<Person>().HasQueryFilter(p => p.TenantId == _tenant.TenantId);
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == _tenant.TenantId);
        }
    }
}
