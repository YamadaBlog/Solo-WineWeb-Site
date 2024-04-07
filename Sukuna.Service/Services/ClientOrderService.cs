using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class ClientOrderService : IClientOrderService
{
    private readonly DataContext _context;

    public ClientOrderService(DataContext context)
    {
        _context = context;
    }

    public bool CreateClientOrder(ClientOrder clientOrder)
    {
        _context.Add(clientOrder);

        return Save();
    }

    public ICollection<ClientOrder> GetClientOrders()
    {
        return _context.ClientOrders.OrderBy(p => p.ID).ToList();
    }
    public ClientOrder GetClientOrderById(int clientOrderId)
    {
        return _context.ClientOrders.Where(c => c.ID == clientOrderId).FirstOrDefault();
    }

    public ICollection<OrderLine> GetOrderLinesByClientOrder(int cliendOrderId)
    {
        return _context.OrderLines.Where(r => r.ClientOrder.ID == cliendOrderId).ToList();
    }

    public bool UpdateClientOrder(ClientOrder clientOrder)
    {
        _context.Update(clientOrder);
        return Save();
    }
    public bool DeleteClientOrder(ClientOrder clientOrder)
    {
        _context.Remove(clientOrder);
        return Save();
    }

    public bool ClientOrderExists(ClientOrderResource clientOrderCreate)
    {
        if (_context.ClientOrders.Any(r => r.ID == clientOrderCreate.ID))
        {
            return true;
        }
        else
        {
            return (!_context.ClientOrders.Any(r => r.ID == clientOrderCreate.ID)) && (!_context.Clients.Any(r => r.ID == clientOrderCreate.ClientID));
        }
    }

    public bool ClientOrderExistsById(int clientOrderId)
    {
        return _context.ClientOrders.Any(r => r.ID == clientOrderId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
