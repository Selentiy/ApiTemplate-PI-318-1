using App.Models.News;
using App.News.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.News.Controllers
{
    [Route("api/news/")]
    [ApiController]
    [TypeFilter(typeof(NewsExceptionFilter), Arguments = new object[] { nameof(NewsController) })]
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
            
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public ActionResult<Article> GetNews(int id)
        {
            var article = _articleManager.GetArticleById(id);

            return Ok(article);
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
        public ActionResult AddComment(int id, [FromBody]Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            _articleManager.GetArticleById(id);
            _commentManager.AddComment(comment);

            return Ok();
        }
    }
}
