using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


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

    public ICollection<Supplier> GetSuppliers()
    {
        return _context.Suppliers.OrderBy(p => p.ID).ToList();
    }
    public ICollection<Article> GetArticlesBySupplier(int supplierId)
    {
        return _context.Articles.Where(r => r.Supplier.ID == supplierId).ToList();
    }
    public Supplier GetSupplierById(int supplierId)
    {
        return _context.Suppliers.Where(c => c.ID == supplierId).FirstOrDefault();
    }

    public bool UpdateSupplier(Supplier supplier)
    {
        _context.Update(supplier);
        return Save();
    }
    public bool DeleteSupplier(Supplier supplier)
    {
        _context.Remove(supplier);
        return Save();
    }

    public Supplier SupplierExists(SupplierResource supplierCreate)
    {
        return GetSuppliers().Where(c => c.Nom.Trim().ToUpper() == supplierCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public bool SupplierExistsById(int supplierId)
    {
        return _context.Suppliers.Any(r => r.ID == supplierId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
