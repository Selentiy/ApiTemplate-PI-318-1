using App.Configuration;
using App.Models.News;
using App.News.Exceptions;
using System;
using System.Linq;

namespace App.News
{
    public interface ICommentValidatorService
    {
        void ValidateComment(Comment comment);
    }

    public class CommentValidatorService : ICommentValidatorService, ITransientDependency
    {
        readonly static string[] _restrictedNames = new[] { "admin", "administrator", "superuser", "root" };

        public void ValidateComment(Comment comment)
        {
            ValidateAuthorName(comment.AuthorName);
            ValidateCommentContent(comment.Content);
        }

        private void ValidateAuthorName(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new CommentParamsNullOrEmptyException(nameof(authorName));
            }

            authorName = authorName.ToLower();

            if (_restrictedNames.Contains(authorName))
            {
                throw new CommentParamsRestrictedException(nameof(authorName));
            }
        }

        private void ValidateCommentContent(string commentContent)
        {
            if (string.IsNullOrWhiteSpace(commentContent))
            {
                throw new CommentParamsNullOrEmptyException(nameof(commentContent));
            }
        }
    }
}
