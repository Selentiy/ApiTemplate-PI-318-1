using App.Models.News;
using Microsoft.EntityFrameworkCore;

namespace App.News.Database
{
    public class NewsDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                        .HasKey(e => e.ArticleID);
            modelBuilder.Entity<Comment>()
                        .HasKey(e => e.CommentID);

            modelBuilder.Entity<Comment>()
                        .Property(f => f.CommentID)
                        .ValueGeneratedOnAdd();
        }
    }
}
