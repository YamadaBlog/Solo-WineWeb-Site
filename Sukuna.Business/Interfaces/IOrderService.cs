using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces
{
    public interface IOrderService
    {
        void CreateClientOrder(int clientId, List<OrderLine> orderLines);
        void CreateSupplierOrder(int userId, int supplierId, List<OrderLine> orderLines);
        void UpdateClientOrder(ClientOrder clientOrder);
        void UpdateSupplierOrder(SupplierOrder supplierOrder);
        void CancelClientOrder(int clientId);
        void CancelSupplierOrder(int supplierId);
        ClientOrder GetClientOrderById(int orderId);
        SupplierOrder GetSupplierOrderById(int orderId);
        List<ClientOrder> GetClientOrdersByClientId(int clientId);
        List<SupplierOrder> GetSupplierOrdersBySupplierId(int supplierId);
        List<OrderLine> GetOrderLinesByClientId(int clientId);
        List<OrderLine> GetOrderLinesBySupplierId(int supplierId);
        List<ClientOrder> GetAllClientOrders();
        List<SupplierOrder> GetAllSupplierOrders();
        List<OrderLine> GetAllOrderLines();
        List<OrderLine> GetOrderLinesByArticleId(int articleId);
        List<OrderLine> GetOrderLinesByOrderId(int orderId);
        void DeleteClientOrder(int orderId);
        void DeleteSupplierOrder(int orderId);
        bool Save();
    }
}
