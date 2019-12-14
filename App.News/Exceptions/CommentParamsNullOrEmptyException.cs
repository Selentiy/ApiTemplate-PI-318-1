namespace App.News.Exceptions
{
    public class CommentParamsNullOrEmptyException : ValidationCommentException
    {
        public CommentParamsNullOrEmptyException(string paramName) : base(paramName, $"argument {paramName} is undefined")
        {

        }
    }
}
