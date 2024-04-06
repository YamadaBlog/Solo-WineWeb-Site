using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Sukuna.Service.Services;

public class OrderService : IOrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }

    public void CreateClientOrder(int clientId, List<OrderLine> orderLines)
    {
        var clientOrder = new ClientOrder
        {
            ClientID = clientId,
            DateCommande = DateTime.Now,
            StatutCommande = "Nouveau",
            OrderLines = orderLines
        };

        _context.ClientOrders.Add(clientOrder);
        Save();
    }

    public void CreateSupplierOrder(int userId, int supplierId, List<OrderLine> orderLines)
    {
        var user = _context.Users.Find(userId);
        if (user == null)
        {
            throw new ArgumentException("Invalid user ID");
        }

        var supplierOrder = new SupplierOrder
        {
            UserID = userId,
            SupplierID = supplierId,
            DateCommande = DateTime.Now,
            StatutCommande = "Nouveau",
            OrderLines = orderLines
        };

        _context.SupplierOrders.Add(supplierOrder);
        _context.SaveChanges();
    }

    public void UpdateClientOrder(ClientOrder clientOrder)
    {
        _context.ClientOrders.Update(clientOrder);
        Save();
    }

    public void UpdateSupplierOrder(SupplierOrder supplierOrder)
    {
        _context.SupplierOrders.Update(supplierOrder);
        Save();
    }

    public void CancelClientOrder(int clientId)
    {
        var clientOrder = _context.ClientOrders.Find(clientId);
        if (clientOrder != null)
        {
            _context.ClientOrders.Remove(clientOrder);
            Save();
        }
    }

    public void CancelSupplierOrder(int supplierId)
    {
        var supplierOrder = _context.SupplierOrders.Find(supplierId);
        if (supplierOrder != null)
        {
            _context.SupplierOrders.Remove(supplierOrder);
            Save();
        }
    }

    public ClientOrder GetClientOrderById(int orderId)
    {
        return _context.ClientOrders
            .Include(co => co.OrderLines)
            .FirstOrDefault(co => co.ID == orderId);
    }

    public SupplierOrder GetSupplierOrderById(int orderId)
    {
        return _context.SupplierOrders
            .Include(so => so.OrderLines)
            .FirstOrDefault(so => so.ID == orderId);
    }

    public List<ClientOrder> GetClientOrdersByClientId(int clientId)
    {
        return _context.ClientOrders
            .Include(co => co.OrderLines)
            .Where(co => co.ClientID == clientId)
            .ToList();
    }

    public List<SupplierOrder> GetSupplierOrdersBySupplierId(int supplierId)
    {
        return _context.SupplierOrders
            .Include(so => so.OrderLines)
            .Where(so => so.SupplierID == supplierId)
            .ToList();
    }

    public List<OrderLine> GetOrderLinesByClientId(int clientId)
    {
        return _context.OrderLines
            .Include(ol => ol.ClientOrder)
            .Include(ol => ol.Article)
            .Where(ol => ol.ClientOrder.ClientID == clientId)
            .ToList();
    }

    public List<OrderLine> GetOrderLinesBySupplierId(int supplierId)
    {
        return _context.OrderLines
            .Include(ol => ol.SupplierOrder)
            .Include(ol => ol.Article)
            .Where(ol => ol.SupplierOrder.SupplierID == supplierId)
            .ToList();
    }

    public List<ClientOrder> GetAllClientOrders()
    {
        return _context.ClientOrders
            .Include(co => co.OrderLines)
            .ToList();
    }

    public List<SupplierOrder> GetAllSupplierOrders()
    {
        return _context.SupplierOrders
            .Include(so => so.OrderLines)
            .ToList();
    }

    public List<OrderLine> GetAllOrderLines()
    {
        return _context.OrderLines
            .Include(ol => ol.ClientOrder)
            .Include(ol => ol.SupplierOrder)
            .Include(ol => ol.Article)
            .ToList();
    }

    public List<OrderLine> GetOrderLinesByArticleId(int articleId)
    {
        return _context.OrderLines
            .Include(ol => ol.ClientOrder)
            .Include(ol => ol.SupplierOrder)
            .Where(ol => ol.ArticleID == articleId)
            .ToList();
    }

    public List<OrderLine> GetOrderLinesByOrderId(int orderId)
    {
        return _context.OrderLines
            .Include(ol => ol.ClientOrder)
            .Include(ol => ol.SupplierOrder)
            .Where(ol => ol.ClientOrderID == orderId || ol.SupplierOrderID == orderId)
            .ToList();
    }

    public void DeleteClientOrder(int orderId)
    {
        var clientOrder = _context.ClientOrders.Find(orderId);
        if (clientOrder != null)
        {
            _context.ClientOrders.Remove(clientOrder);
            Save();
        }
    }

    public void DeleteSupplierOrder(int orderId)
    {
        var supplierOrder = _context.SupplierOrders.Find(orderId);
        if (supplierOrder != null)
        {
            _context.SupplierOrders.Remove(supplierOrder);
            Save();
        }
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
