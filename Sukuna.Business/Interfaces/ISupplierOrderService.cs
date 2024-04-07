using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ISupplierOrderService
{
    bool CreateSupplierOrder(SupplierOrder supplierOrder);
    SupplierOrder GetSupplierOrderById(int supplierOrderId);
    ICollection<SupplierOrder> GetSupplierOrders();
    ICollection<OrderLine> GetOrderLinesBySupplierOrder(int cliendOrderId);
    bool UpdateSupplierOrder(SupplierOrder supplierOrder);
    bool DeleteSupplierOrder(SupplierOrder supplierOrder);
    bool SupplierOrderExists(SupplierOrderResource supplierOrderCreate);
    bool SupplierOrderExistsById(int supplierOrderId);

    bool Save();
}
