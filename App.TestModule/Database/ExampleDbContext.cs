using App.Models.Example;
using Microsoft.EntityFrameworkCore;

namespace App.Example.Database
{
    // Class, which represents an entry point for interacting with database.
    // For more details, please, visit next resources:
    // https://www.entityframeworktutorial.net/efcore/entity-framework-core-dbcontext.aspx
    public class ExampleDbContext : DbContext
    {
        public DbSet<SimpleValue> SimpleValues { get; set; }
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options) { }

        // method for configuring object-relational mapping
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SimpleValue>()
                .HasKey(sv => sv.Key);
        }
    }
}