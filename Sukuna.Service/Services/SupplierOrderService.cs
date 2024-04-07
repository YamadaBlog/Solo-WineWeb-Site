﻿using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class SupplierOrderService : ISupplierOrderService
{
    private readonly DataContext _context;

    public SupplierOrderService(DataContext context)
    {
        _context = context;
    }

    public bool CreateSupplierOrder(SupplierOrder supplierOrder)
    {
        _context.Add(supplierOrder);

        return Save();
    }

    public bool SupplierOrderExists(SupplierOrderResource supplierOrderCreate)
    {
        if (_context.SupplierOrders.Any(r => r.ID == supplierOrderCreate.ID))
        {
            return true;
        }
        else
        {
            return (!_context.SupplierOrders.Any(r => r.ID == supplierOrderCreate.ID)) && (!_context.Users.Any(r => r.ID == supplierOrderCreate.UserID));
        }
    }

    public ICollection<SupplierOrder> GetSupplierOrders()
    {
        return _context.SupplierOrders.OrderBy(p => p.ID).ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
