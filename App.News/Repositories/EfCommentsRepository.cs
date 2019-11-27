using App.Configuration;
using App.Models.News;
using App.News.Database;
using App.Repositories.News;
<<<<<<< HEAD
using System;
=======
>>>>>>> 850aa6bd3b706578a1929545e6affb0a4cf8222d
using System.Collections.Generic;
using System.Linq;

namespace App.News.Repositories
{
    public class EfCommentsRepository : ICommentsRepository, ITransientDependency, IDisposable
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

        public Comment GetCommentById(int commentId)
        {
            var comment = _dbContext.Comments.AsQueryable()
                                             .FirstOrDefault(c => c.CommentID == commentId);

            return comment;
        }

        public IEnumerable<Comment> GetComments()
        {
            var comments = _dbContext.Comments.ToList();
            return comments;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
