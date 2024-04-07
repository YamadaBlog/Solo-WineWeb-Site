using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Service.Services;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public ArticlesController(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateArticle([FromBody] ArticleResource articleCreate)
    {
        if (articleCreate == null)
            return BadRequest(ModelState);

        // Vérifie si article existe à partir du nom
        var articles = _articleService.ArticleExists(articleCreate);

        if (articles != null)
        {
            ModelState.AddModelError("", "Client doesn't exists or Article already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var articleMap = _mapper.Map<Article>(articleCreate);

        if (!_articleService.CreateArticle(articleMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

    [HttpGet("{articleId}/orderLines")]
    public IActionResult GetOrderLinesByArticle(int articleId)
    {
        if (!_articleService.ArticleExistsById(articleId))
            return NotFound();

        var orderLines = _mapper.Map<List<OrderLineResource>>(
            _articleService.GetOrderLinesByArticle(articleId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(orderLines);
    }

    [HttpGet("supplier/{supplierId}")]
    [ProducesResponseType(200, Type = typeof(Article))]
    [ProducesResponseType(400)]

    public IActionResult GetArticlesOfASupplier(int supplierId)
    {
        var articles = _mapper.Map<List<ArticleResource>>(_articleService.GetArticlesOfASupplier(supplierId));

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(articles);
    }

    [HttpGet("{articleId}")]
    [ProducesResponseType(200, Type = typeof(Article))]
    [ProducesResponseType(400)]
    public IActionResult GetArticleById(int articleId)
    {
        if (!_articleService.ArticleExistsById(articleId))
            return NotFound();

        var article = _mapper.Map<ArticleResource>(_articleService.GetArticleById(articleId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(article);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
    public IActionResult GetArticles()
    {
        var articles = _mapper.Map<List<ArticleResource>>(_articleService.GetArticles());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(articles);
    }

    [HttpPut("{articleId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateArticle(int articleId, [FromBody] ArticleResource updatedArticle)
    {
        if (updatedArticle == null)
            return BadRequest(ModelState);

        if (articleId != updatedArticle.ID)
            return BadRequest(ModelState);

        if (!_articleService.ArticleExistsById(articleId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var articleMap = _mapper.Map<Article>(updatedArticle);

        if (!_articleService.UpdateArticle(articleMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{articleId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteArticle(int articleId)
    {
        if (!_articleService.ArticleExistsById(articleId))
        {
            return NotFound();
        }

        var articleToDelete = _articleService.GetArticleById(articleId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_articleService.DeleteArticle(articleToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting article");
        }

        return Ok("Successfully deleted");
    }
}