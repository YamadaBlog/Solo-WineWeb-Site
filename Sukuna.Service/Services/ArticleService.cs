using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Business.Interfaces;

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

    public bool CreateArticle(int clientOrderID, int supplierOrderID, Article article)
    {
        var clientOrder = _context.ClientOrders.Where(a => a.ID == clientOrderID).FirstOrDefault();
        var supplierOrder = _context.SupplierOrders.Where(a => a.ID == supplierOrderID).FirstOrDefault();

        var orderLine = new OrderLine()
        {
            ClientOrder = clientOrder,
            SupplierOrder = supplierOrder,
            Article = article
        };

        _context.Add(orderLine);

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

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
