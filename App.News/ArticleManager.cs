using App.Configuration;
using App.Models.News;
using App.News.Exceptions;
using App.Repositories.News;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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

            if (article == null)
                throw new EntityNotFoundException(typeof(Article), id);

            return article;
        }

        public IEnumerable<Article> GetArticles()
        {
            _logger.LogInformation("Call GetArticles method");

            var articles =_repository.GetArticles();

            if (articles.Count() == 0)
                throw new NoArticlesContentException();

            return articles;
        }
    }
}
