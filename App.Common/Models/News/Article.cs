using System;
using System.Collections.Generic;

namespace App.Models.News
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string BackgroundImageUrl { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
