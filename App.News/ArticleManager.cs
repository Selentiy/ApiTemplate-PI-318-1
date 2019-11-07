using App.Configuration;
using App.Models.News;
using App.News.Exceptions;
using App.Repositories.News;
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
        public ArticleManager(IArticlesRepository repo)
        {
            _repository = repo;
        }

        public Article GetArticleById(int id)
        {
            var article = _repository.GetArticleById(id);

            if (article == null)
                throw new EntityNotFoundException(typeof(Article), id);

            return article;
        }

        public IEnumerable<Article> GetArticles()
        {
            var articles =_repository.GetArticles();

            if (articles.Count() == 0)
                throw new NoArticlesContentException();

            return articles;
        }
    }
}
