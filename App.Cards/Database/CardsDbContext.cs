using App.Models.Cards;
using Microsoft.EntityFrameworkCore;

namespace App.Cards.Database
{
    public class CardsDbContext: DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public CardsDbContext(DbContextOptions<CardsDbContext> options) : base(options) { }
        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
    }
}
