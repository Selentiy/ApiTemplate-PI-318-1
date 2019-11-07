using App.Models.News;

namespace App.News.Exceptions
{
    public class NoCommentsContentException : EntityNoContentException
    {
        public int ArticleId { get; private set; }

        public NoCommentsContentException(int articleId) : 
            base(typeof(Comment), $"comments to this article (id {articleId}) don't exist yet.")
        {
            ArticleId = articleId;
        }
    }
}
