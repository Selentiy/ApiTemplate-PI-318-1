using App.Configuration;
using App.Models.News;
using App.Repositories.News;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Services
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
        readonly ILogger<CommentManager> _logger;

        public CommentManager(ICommentsRepository repo, ICommentValidatorService validatorService, ILogger<CommentManager> logger)
        {
            _repository = repo;
            _validator = validatorService;
            _logger = logger;
        }

        public void AddComment(Comment comment)
        {
            _logger.LogInformation("Call AddComment method");

            _validator.ValidateComment(comment);
            _repository.CreateComment(comment);
        }

        public IEnumerable<Comment> GetComments(int articleId)
        {
            _logger.LogInformation("Call GetComments method with articleId {articleId}", articleId);

            var comments = _repository.GetComments().Where(cm => cm.ArticleID == articleId);

            return comments;
        }
    }
}
