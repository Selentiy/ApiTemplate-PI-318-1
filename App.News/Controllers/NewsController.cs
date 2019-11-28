using App.Models.News;
using App.News.Exceptions;
using App.News.Filters;
using App.News.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace App.News.Controllers
{
    [Route("api/news/")]
    [ApiController]
    [TypeFilter(typeof(NewsExceptionFilter), Arguments = new object[] { nameof(NewsController) })]
    public class NewsController : ControllerBase
    {
        readonly IArticleManager _articleManager;
        readonly ICommentManager _commentManager;
        readonly ILogger<NewsController> _logger;

        public NewsController(
            IArticleManager articleManager,
            ICommentManager commentManager,
            ILogger<NewsController> logger)
        {
            _articleManager = articleManager;
            _commentManager = commentManager;
            _logger = logger;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Article>> GetAllNews()
        {
            _logger.LogInformation("Call GetAllNews method");

            var articles = _articleManager.GetArticles();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public ActionResult<Article> GetNews(int id)
        {
            _logger.LogInformation("Call GetNews method with id {id}", id);

            var article = _articleManager.GetArticleById(id);

            if (article == null)
                throw new EntityNotFoundException(typeof(Article), id);

            return Ok(article);
        }

        [HttpGet("{id}/comments")]
        public ActionResult<IEnumerable<Comment>> GetComments(int id)
        {
            _logger.LogInformation("Call GetComments method with id {id}", id);

            var article = _articleManager.GetArticleById(id);

            if (article == null)
                throw new EntityNotFoundException(typeof(Article), id);

            var comments = _commentManager.GetComments(id);
            return Ok(comments);
        }

        [HttpPost("{id}/comments")]
        public ActionResult AddComment(int id, [FromBody]CreateCommentViewModel createComment)
        {
            _logger.LogInformation("Call AddComment method with id {id}", id);

            var article = _articleManager.GetArticleById(id);

            if (article == null)
                throw new EntityNotFoundException(typeof(Article), id);

            var comment = MapCreateCommentToComment(createComment);
            comment.Date = DateTime.Now;
            comment.ArticleID = id;

            _commentManager.AddComment(comment);

            return Ok();
        }

        private Comment MapCreateCommentToComment(CreateCommentViewModel createComment)
        {
            _logger.LogInformation("Call MapCreateCommentToComment method");

            return new Comment()
            {
                AuthorName = createComment.AuthorName,
                Content = createComment.Content,
            };
        }
    }
}
