using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.Business.Interfaces;

public interface ISupplierService
{
    Supplier GetSupplierTrimToUpper(SupplierResource supplierCreate);
    bool CreateSupplier(Supplier supplier);
    ICollection<Supplier> GetSuppliers();
    bool Save();
}
