using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }
    }
}
