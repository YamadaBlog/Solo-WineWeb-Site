using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class OrderLineService : IOrderLineService
{
    private readonly DataContext _context;

    public OrderLineService(DataContext context)
    {
        _context = context;
    }

    public bool CreateOrderLine(OrderLine orderLine)
    {
        _context.Add(orderLine);

        return Save();
    }

    public ICollection<OrderLine> GetOrderLines()
    {
        return _context.OrderLines.OrderBy(p => p.ID).ToList();
    }
    public OrderLine GetOrderLineById(int orderLineId)
    {
        return _context.OrderLines.Where(o => o.ID == orderLineId).FirstOrDefault();
    }
    public ICollection<OrderLine> GetOrderLinesOfASupplierOrder(int supplierOrderId)
    {
        return _context.OrderLines.Where(o => o.SupplierOrder.ID == supplierOrderId).ToList();
    }
    public ICollection<OrderLine> GetOrderLinesOfAClientOrder(int clientOrderId)
    {
        return _context.OrderLines.Where(o => o.ClientOrder.ID == clientOrderId).ToList();
    }
    public ICollection<OrderLine> GetOrderLinesOfAArticle(int articleId)
    {
        return _context.OrderLines.Where(o => o.Article.ID == articleId).ToList();
    }
    public bool UpdateOrderLine(OrderLine orderLine)
    {
        _context.Update(orderLine);
        return Save();
    }
    public bool DeleteOrderLines(List<OrderLine> orderLines) 
    { 
        _context.RemoveRange(orderLines);
        return Save();
    }

    public bool DeleteOrderLine(OrderLine orderLine)
    {
        _context.Remove(orderLine);
        return Save();
    }

    public bool OrderLineExists(OrderLineResource orderLineCreate)
    {
        // Si ligne déja existante exit
        if (_context.OrderLines.Any(r => r.ID == orderLineCreate.ID))
        {
            return true;
        }
        else
        {
            // On vérifie qu'une commande existe et que l'article existe.
            return (!_context.OrderLines.Any(r => r.ID == orderLineCreate.ID))
                && ((!_context.ClientOrders.Any(r => r.ID == orderLineCreate.ClientOrderID)) || (!_context.SupplierOrders.Any(r => r.ID == orderLineCreate.SupplierOrderID)))
                && (!_context.Articles.Any(r => r.ID == orderLineCreate.ArticleID));
        }
    }
    public bool OrderLineExistsById(int orderLineId)
    {
        return _context.OrderLines.Any(r => r.ID == orderLineId);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
