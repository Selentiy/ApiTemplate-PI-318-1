using App.Configuration;
using App.Models.News;
using App.News.Database;
using App.Repositories.News;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Repositories
{
    public class EfArticlesRepository : IArticlesRepository, ITransientDependency
    {
        private readonly NewsDbContext _dbContext;

        public EfArticlesRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Article GetArticleById(int id)
        {
            return _dbContext.Articles.AsQueryable()
                                      .FirstOrDefault(a => a.ArticleID == id);
        }

        public IEnumerable<Article> GetArticles()
        {
            var articles = _dbContext.Articles.ToList();
            return articles;
        }
    }
}
