using App.Configuration;
using App.Models.News;
using App.Repositories.News;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace App.News
{
    public interface IArticleManager
    {
        IEnumerable<Article> GetArticles();
        Article GetArticleById(int id);
    }

    public class ArticleManager : IArticleManager, ITransientDependency
    {
        readonly IArticlesRepository _repository;
        readonly ILogger<ArticleManager> _logger;

        public ArticleManager(IArticlesRepository repo, ILogger<ArticleManager> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public Article GetArticleById(int id)
        {
            _logger.LogInformation("Call GetArticleById with id {id}", id);

            var article = _repository.GetArticleById(id);

            return article;
        }

        public IEnumerable<Article> GetArticles()
        {
            _logger.LogInformation("Call GetArticles method");

            var articles =_repository.GetArticles();

            return articles;
        }
    }
}
