using App.Models.News;
using App.Repositories.News;
using System.Collections.Generic;
using System.Linq;

namespace App.News
{
    public interface ICommentManager
    {
        bool AddComment(Comment comment);
        IEnumerable<Comment> GetComments(int articleId);
    }

    public class CommentManager : ICommentManager
    {
        readonly ICommentsRepository _repository;

        public CommentManager(ICommentsRepository repo)
        {
            _repository = repo;
        }

        public bool AddComment(Comment comment)
        {
            if (comment == null)
                return false;

            var sameComment = _repository.GetComments().Where(cm => cm.ArticleID == comment.ArticleID)
                                                       .FirstOrDefault(cm => cm.CommentID == comment.CommentID);

            if (sameComment != null)
                return false;

            _repository.CreateComment(comment);
            return true;
        }

        public IEnumerable<Comment> GetComments(int articleId)
        {
            return _repository.GetComments().Where(cm => cm.ArticleID == articleId);
        }
    }
}
