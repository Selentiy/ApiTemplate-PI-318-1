using App.Models.News;

namespace App.News.Exceptions
{
    public class NoArticlesContentException : EntityNoContentException
    {
        public NoArticlesContentException() : 
            base(typeof(Article), "articles don't exist yet.")
        {

        }
    }
}
