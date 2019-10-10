using System;

namespace App.Models.News
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string BackgroundReference { get; set; }
    }
}
