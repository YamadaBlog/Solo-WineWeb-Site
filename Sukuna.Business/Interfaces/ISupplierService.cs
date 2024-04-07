using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ISupplierService
{
    bool CreateSupplier(Supplier supplier);
    Supplier GetSupplierById(int supplierId);
    ICollection<Supplier> GetSuppliers();
    ICollection<Article> GetArticlesBySupplier(int supplierId);
    bool UpdateSupplier(Supplier supplier);
    bool DeleteSupplier(Supplier supplier);
    Supplier SupplierExists(SupplierResource supplierCreate);
    bool SupplierExistsById(int supplierId);

    bool Save();
}
