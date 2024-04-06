using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Service.Services;

public class TvaTypeService : ITvaTypeService
{
    private readonly DataContext _context;

    public TvaTypeService(DataContext context)
    {
        _context = context;
    }
    public TvaType GetTvaType(int tvaTypeID)
    {
        return _context.TvaTypes.Where(r => r.ID == tvaTypeID).Include(e => e.Articles).FirstOrDefault();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
