using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/sukuna")]
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

    // public IActionResult CreateArticle([FromQuery] int clientOrderId, [FromQuery] int supplierOrderId, [FromBody] ArticleResource articleCreate)
    public IActionResult CreateArticle([FromBody] ArticleResource articleCreate)
    {
        if (articleCreate == null)
            return BadRequest(ModelState);

        var articles = _articleService.GetArticleTrimToUpper(articleCreate);

        if (articles != null)
        {
            ModelState.AddModelError("", "Owner already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var articleMap = _mapper.Map<Article>(articleCreate);

        // if (!_articleService.CreateArticle(clientOrderId, supplierOrderId, articleMap))
        if (!_articleService.CreateArticle(articleMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
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

    [HttpGet("{articleId}")]
    [ProducesResponseType(200, Type = typeof(Article))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemon(int articleId)
    {
        if (!_articleService.ArticleExists(articleId))
            return NotFound();

        var article = _mapper.Map<ArticleResource>(_articleService.GetArticle(articleId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(article);
    }
}