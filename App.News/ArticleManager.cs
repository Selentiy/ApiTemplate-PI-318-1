using App.Configuration;
using App.Models.News;
using App.Repositories.News;
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
        public ArticleManager(IArticlesRepository repo)
        {
            _repository = repo;
        }

        public Article GetArticleById(int id)
        {
            return _repository.GetArticleById(id);
        }

        public IEnumerable<Article> GetArticles()
        {
            return _repository.GetArticles();
        }
    }
}
