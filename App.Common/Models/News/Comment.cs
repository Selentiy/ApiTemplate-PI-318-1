using System;

namespace App.Models.News
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ArticleID { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
