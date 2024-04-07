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
    bool SupplierOrderExists(SupplierOrderResource supplierOrderCreate);
    bool CreateSupplierOrder(SupplierOrder supplierOrder);
    ICollection<SupplierOrder> GetSupplierOrders();
    bool Save();
}
