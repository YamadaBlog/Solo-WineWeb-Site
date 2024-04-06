using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Business.Interfaces;

namespace Sukuna.Service.Services;

public class SupplierService : ISupplierService
{
    private readonly DataContext _context;

    public SupplierService(DataContext context)
    {
        _context = context;
    }

    public bool CreateSupplier(Supplier supplier)
    {
        _context.Add(supplier);

        return Save();
    }

    public Supplier GetSupplierTrimToUpper(SupplierResource supplierCreate)
    {
        return GetSuppliers().Where(c => c.Nom.Trim().ToUpper() == supplierCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public ICollection<Supplier> GetSuppliers()
    {
        return _context.Suppliers.OrderBy(p => p.ID).ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
