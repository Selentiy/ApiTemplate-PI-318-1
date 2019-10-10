using App.Models.News;
using System.Collections.Generic;

namespace App.Repositories.News
{
    public interface IArticlesRepository
    {
        IEnumerable<Article> GetArticles();
        Article GetArticleById(int id);
    }
}
