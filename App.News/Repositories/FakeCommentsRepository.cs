using App.Configuration;
using App.Models.News;
using App.Repositories.News;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Repositories
{
    public class FakeCommentsRepository : ICommentsRepository, ISingletoneDependency
    {
        private IEnumerable<Comment> comments;

        public FakeCommentsRepository()
        {
            comments = CommentsInitializer.GetComments();
        }

        public void CreateComment(Comment comment)
        {
            comments = comments.Append(comment);
        }

        public Comment GetCommentById(int articleId, int commentId)
        {
            return comments.Where(cm => cm.ArticleID == articleId)
                           .FirstOrDefault(cm => cm.CommentID == commentId);
        }

        public IEnumerable<Comment> GetComments()
        {
            return comments;
        }

        private static class CommentsInitializer
        {
            public static List<Comment> GetComments()
            {
                return new List<Comment>()
                {
                    new Comment()
                    {
                        ArticleID = 1,
                        AuthorName = "Anonim",
                        CommentID = 1,
                        Content = "This is my anonim comment",
                        Date = DateTime.Parse("01/18/2016 07:22:16")
                    },
                    new Comment()
                    {
                        ArticleID = 1,
                        AuthorName = "Taras",
                        CommentID = 2,
                        Content = "I love dogs",
                        Date = DateTime.Parse("02/13/2016 06:21:16")
                    },
                    new Comment()
                    {
                        ArticleID = 2,
                        AuthorName = "Taras",
                        CommentID = 1,
                        Content = "I love cats",
                        Date = DateTime.Parse("02/13/2016 06:23:06")
                    },
                    new Comment()
                    {
                        ArticleID = 3,
                        AuthorName = "Taras",
                        CommentID = 1,
                        Content = "I love birds",
                        Date = DateTime.Parse("02/13/2016 06:24:12")
                    }
                };
            }
        }
    }
}
