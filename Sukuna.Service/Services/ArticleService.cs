using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Sukuna.Service.Services;

// Il est question d'implementer et pas d'héritage CELA FONCtIONNE AVEC PROGM.CS
// remettre le bon nom des fontions 
public class ArticleService : IArticleService
{
    private readonly DataContext _context;

    public ArticleService(DataContext context)
    {
        _context = context;
    }

    // public bool CreateArticle(int clientOrderId, int supplierOrderId, Article article)
    public bool CreateArticle(Article article)
    {
    /*  var clientOrder = _context.ClientOrders.Where(a => a.ID == clientOrderId).FirstOrDefault();
        var supplierOrder = _context.SupplierOrders.Where(a => a.ID == supplierOrderId).FirstOrDefault();

        var orderLine = new OrderLine()
        {
            ClientOrder = clientOrder,
            SupplierOrder = supplierOrder,
            Article = article
        };

        _context.Add(orderLine); Pour ajouter une ligne de commande en référence à un article */

        _context.Add(article);

        return Save();
    }

    public Article GetArticleTrimToUpper(ArticleResource articleCreate)
    {
        return GetArticles().Where(c => c.Nom.Trim().ToUpper() == articleCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public ICollection<Article> GetArticles()
    {
        return _context.Articles.OrderBy(p => p.ID).ToList();
    }

    public Article GetArticle(int id)
    {
        return _context.Articles.Where(p => p.ID == id).FirstOrDefault();
    }

    public bool ArticleExists(int articleId)
    {
        return _context.Articles.Any(r => r.ID == articleId);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
