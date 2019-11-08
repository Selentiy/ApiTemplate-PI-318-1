using App.Models.Accounts;
using App.Models.News;

namespace App.Accounts.Exceptions
{
    public class ArticleNotFoundException : EntityNotFoundException
    {
        public ArticleNotFoundException(string countryCode, string checkDigits, string bankCode, string accountNumber) : 
            base(typeof(Account), $"Article with Country Code {countryCode}, Check Digits {checkDigits}, " +
                $"Bank Code {bankCode}, Account Number {accountNumber} not found.")
        {

        }

        public ArticleNotFoundException(int articleId) : base(typeof(Article), $"Article with id {articleId} not found")
        {

        }
    }
}
