using App.Models.News;
using System.Collections.Generic;

namespace App.Repositories.News
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> GetComments();
        Comment GetCommentById(int commentId);
        void CreateComment(Comment comment);
    }
}
