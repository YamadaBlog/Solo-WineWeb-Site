using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Business.Interfaces;

namespace Sukuna.Service.Services;

public class TvaTypeService : ITvaTypeService
{
    private readonly DataContext _context;

    public TvaTypeService(DataContext context)
    {
        _context = context;
    }

    public bool CreateTvaType(TvaType tvaType)
    {
        _context.Add(tvaType);

        return Save();
    }

    public TvaType GetTvaTypeTrimToUpper(TvaTypeResource tvaTypeCreate)
    {
        return GetTvaTypes().Where(c => c.Nom.Trim().ToUpper() == tvaTypeCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public ICollection<TvaType> GetTvaTypes()
    {
        return _context.TvaTypes.OrderBy(p => p.ID).ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
