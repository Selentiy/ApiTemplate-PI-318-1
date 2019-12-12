using System;
using System.Collections.Generic;
using System.Globalization;
using App.Configuration;
using App.Models.News;
using App.News.Database;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;

namespace App.News
{
    public class NewsModule : IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<NewsDbContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<NewsDbContext>();

                builder.UseInMemoryDatabase("NewsDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<NewsDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            using (var context = container.Resolve<NewsDbContext>())
            {
                var articles = GetArticles();
                var comments = GetComments();

                context.Articles.AddRange(articles);
                context.Comments.AddRange(comments);

                context.SaveChanges();
            }
        }

        private static IEnumerable<Article> GetArticles()
        {
            return new List<Article>()
            {
                new Article()
                {
                    ArticleID = 1,
                    Header = "First Article",
                    Author = "Author 1",
                    BackgroundImageUrl = "https://msdn.microsoft.com/en-us/",
                    Content = "Some content",
                    Date = DateTime.Now
                },
                new Article()
                {
                    ArticleID = 2,
                    Header = "Second Article",
                    Author = "Author 2",
                    BackgroundImageUrl = "https://msdn.microsoft.com/en-us/",
                    Content = "Some content",
                    Date = DateTime.Now
                },
                new Article()
                {
                    ArticleID = 3,
                    Header = "Third Article",
                    Author = "Author 1",
                    BackgroundImageUrl = "https://msdn.microsoft.com/en-us/",
                    Content = "Some content",
                    Date = DateTime.Now
                },
                new Article()
                {
                    ArticleID = 4,
                    Header = "Fourth Article",
                    Author = "Author 3",
                    BackgroundImageUrl = "https://msdn.microsoft.com/en-us/",
                    Content = "Some content",
                    Date = DateTime.Now
                }
            };
        }

        private static IEnumerable<Comment> GetComments()
        {
            var provider = new CultureInfo("en-US");

            return new List<Comment>()
            {
                new Comment()
                {
                    ArticleID = 1,
                    AuthorName = "Anonim",
                    Content = "This is my anonim comment",
                    Date = DateTime.Parse("01/18/2016 07:22:16", provider),
                },
                new Comment()
                {
                    ArticleID = 1,
                    AuthorName = "Taras",
                    Content = "I love dogs",
                    Date = DateTime.Parse("02/13/2016 06:21:16", provider),
                },
                new Comment()
                {
                    ArticleID = 2,
                    AuthorName = "Taras",
                    Content = "I love cats",
                    Date = DateTime.Parse("02/13/2016 06:23:06", provider),
                },
                new Comment()
                {
                    ArticleID = 3,
                    AuthorName = "Taras",
                    Content = "I love birds",
                    Date = DateTime.Parse("02/13/2016 06:24:12", provider),
                }
            };
        }
    }
}
