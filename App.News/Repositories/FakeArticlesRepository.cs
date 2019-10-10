using App.Configuration;
using App.Models.News;
using App.Repositories.News;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Repositories
{
    public class FakeArticlesRepository : IArticlesRepository
    {
        private IEnumerable<Article> articles;

        public FakeArticlesRepository()
        {
            articles = ArticlesInitializer.GetArticles();
        }

        public Article GetArticleById(int id)
        {
            return articles.FirstOrDefault(arc => arc.ArticleID == id);
        }

        public IEnumerable<Article> GetArticles()
        {
            return articles;
        }

        private static class ArticlesInitializer
        {
            public static List<Article> GetArticles()
            {
                return new List<Article>()
                {
                    new Article()
                    {
                        ArticleID = 1,
                        Header = "First Article",
                        Author = "Author 1",
                        BackgroundReference = "https://msdn.microsoft.com/en-us/",
                        Content = "Some content",
                        Date = DateTime.Now
                    },
                    new Article()
                    {
                        ArticleID = 2,
                        Header = "Second Article",
                        Author = "Author 2",
                        BackgroundReference = "https://msdn.microsoft.com/en-us/",
                        Content = "Some content",
                        Date = DateTime.Now
                    },
                    new Article()
                    {
                        ArticleID = 3,
                        Header = "Third Article",
                        Author = "Author 1",
                        BackgroundReference = "https://msdn.microsoft.com/en-us/",
                        Content = "Some content",
                        Date = DateTime.Now
                    },
                    new Article()
                    {
                        ArticleID = 4,
                        Header = "Fourth Article",
                        Author = "Author 3",
                        BackgroundReference = "https://msdn.microsoft.com/en-us/",
                        Content = "Some content",
                        Date = DateTime.Now
                    }
                };
            }
        }
    }
}
