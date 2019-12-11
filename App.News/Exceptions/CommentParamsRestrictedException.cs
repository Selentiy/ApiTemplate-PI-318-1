namespace App.News.Exceptions
{
    public class CommentParamsRestrictedException : ValidationCommentException
    {
        public CommentParamsRestrictedException(string paramName) : base(paramName, $"argument {paramName} has restricted value")
        {

        }
    }
}
