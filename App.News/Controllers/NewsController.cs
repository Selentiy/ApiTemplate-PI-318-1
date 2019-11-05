using App.Models.News;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Controllers
{
    [Route("api/news/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        readonly IArticleManager _articleManager;
        readonly ICommentManager _commentManager;

        public NewsController(
            IArticleManager articleManager,
            ICommentManager commentManager)
        {
            _articleManager = articleManager;
            _commentManager = commentManager;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Article>> GetAllNews()
        {
            var articles = _articleManager.GetArticles();

            if (articles.Count() == 0)
                return NoContent();

            return Ok(articles);
        }

        [HttpGet("{id}")]
        public ActionResult<Article> GetNews(int id)
        {
            var article = _articleManager.GetArticleById(id);

            if (article == null)
                return NotFound();

            return article;
        }

        [HttpGet("{id}/comments")]
        public ActionResult<IEnumerable<Comment>> GetComments(int id)
        {
            var article = _articleManager.GetArticleById(id);

            if (article == null)
                return NotFound();

            var comments = _commentManager.GetComments(id);

            if (comments.Count() == 0)
                return NoContent();

            return Ok(comments);
        }

        [HttpPost("{id}/comments")]
        public ActionResult AddComment([FromBody]Comment comment)
        {
            _commentManager.AddComment(comment);

            return Ok();
        }
    }
}
