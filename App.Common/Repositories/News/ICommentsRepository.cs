using App.Models.News;
using System.Collections.Generic;

namespace App.Repositories.News
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> GetComments();
        Comment GetCommentById(int articleId, int commentId);
        void CreateComment(Comment comment);
    }
}
