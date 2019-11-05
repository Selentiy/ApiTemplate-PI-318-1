using App.Configuration;
using App.Models.News;
using App.Repositories.News;
using System.Collections.Generic;
using System.Linq;

namespace App.News
{
    public interface ICommentManager
    {
        void AddComment(Comment comment);
        IEnumerable<Comment> GetComments(int articleId);
    }

    public class CommentManager : ICommentManager, ITransientDependency
    {
        readonly ICommentsRepository _repository;

        public CommentManager(ICommentsRepository repo)
        {
            _repository = repo;
        }

        public void AddComment(Comment comment)
        {
            var sameComment = _repository.GetComments().Where(cm => cm.ArticleID == comment.ArticleID)
                                                       .FirstOrDefault(cm => cm.CommentID == comment.CommentID);

            _repository.CreateComment(comment);
        }

        public IEnumerable<Comment> GetComments(int articleId)
        {
            return _repository.GetComments().Where(cm => cm.ArticleID == articleId);
        }
    }
}
