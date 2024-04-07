using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IClientOrderService
{
    bool CreateClientOrder(ClientOrder clientOrder);
    ClientOrder GetClientOrderById(int clientOrderId);
    ICollection<ClientOrder> GetClientOrders();
    ICollection<OrderLine> GetOrderLinesByClientOrder(int cliendOrderId);
    bool UpdateClientOrder(ClientOrder clientOrder);
    bool DeleteClientOrder(ClientOrder clientOrder);
    bool ClientOrderExists(ClientOrderResource clientOrderCreate);
    bool ClientOrderExistsById(int clientOrderId);

    bool Save();
}
