using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class ArticleService : IArticleService
{
    private readonly DataContext _context;

    public ArticleService(DataContext context)
    {
        _context = context;
    }

    public bool CreateArticle(Article article)
    {
        _context.Add(article);

        return Save();
    }

    public ICollection<Article> GetArticles()
    {
        return _context.Articles.OrderBy(p => p.ID).ToList();
    }
    public Article GetArticleById(int articleId)
    {
        return _context.Articles.Where(c => c.ID == articleId).FirstOrDefault();
    }

    public ICollection<OrderLine> GetOrderLinesByArticle(int articleId)
    {
        return _context.OrderLines.Where(r => r.Article.ID == articleId).ToList();
    }

    public ICollection<Article> GetArticlesOfASupplier(int supplierId)
    {
        return _context.Articles.Where(o => o.Supplier.ID == supplierId).ToList();
    }

    public bool UpdateArticle(Article article)
    {
        _context.Update(article);
        return Save();
    }
    public bool DeleteArticle(Article article)
    {
        _context.Remove(article);
        return Save();
    }

    public Article ArticleExists(ArticleResource articleCreate)
    {
        return GetArticles().Where(c => c.Nom.Trim().ToUpper() == articleCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public bool ArticleExistsById(int articleId)
    {
        return _context.Articles.Any(r => r.ID == articleId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
