using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IOrderLineService
{
    bool CreateOrderLine(OrderLine orderLine);

    OrderLine GetOrderLineById(int orderLineId);
    ICollection<OrderLine> GetOrderLines();
    ICollection<OrderLine> GetOrderLinesOfAClientOrder(int clientOrderId);
    ICollection<OrderLine> GetOrderLinesOfASupplierOrder(int supplierOrderId);
    ICollection<OrderLine> GetOrderLinesOfAArticle(int articleId);
    bool UpdateOrderLine(OrderLine orderLine);
    bool DeleteOrderLines(List<OrderLine> orderLines);
    bool DeleteOrderLine(OrderLine orderLine);
    bool OrderLineExists(OrderLineResource orderLineCreate);
    bool OrderLineExistsById(int orderLineId);
    bool Save();
}
