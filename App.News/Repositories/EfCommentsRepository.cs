using App.Configuration;
using App.Models.News;
using App.News.Database;
using App.Repositories.News;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Repositories
{
    public class EfCommentsRepository : ICommentsRepository, ITransientDependency
    {
        private readonly NewsDbContext _dbContext;

        public EfCommentsRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateComment(Comment comment)
        {
                _dbContext.Add(comment);
                _dbContext.SaveChanges();
        }

        public Comment GetCommentById(int articleId, int commentId)
        {
            var comment = _dbContext.Comments.AsQueryable()
                                             .Where(c => c.ArticleID == articleId)
                                             .FirstOrDefault(c => c.CommentID == commentId);

            return comment;
        }

        public IEnumerable<Comment> GetComments()
        {
            var comments = _dbContext.Comments.ToList();
            return comments;
        }
    }
}
