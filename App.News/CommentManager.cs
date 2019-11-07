using App.Configuration;
using App.Models.News;
using App.News.Exceptions;
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
        readonly ICommentValidatorService _validator;

        public CommentManager(ICommentsRepository repo, ICommentValidatorService validatorService)
        {
            _repository = repo;
            _validator = validatorService;
        }

        public void AddComment(Comment comment)
        {
            _validator.ValidateComment(comment);
            _repository.CreateComment(comment);
        }

        public IEnumerable<Comment> GetComments(int articleId)
        {
            var comments = _repository.GetComments().Where(cm => cm.ArticleID == articleId);

            if (comments.Count() == 0)
                throw new NoCommentsContentException(articleId);

            return comments;
        }
    }
}
