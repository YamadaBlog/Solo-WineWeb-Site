using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


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

    public ICollection<TvaType> GetTvaTypes()
    {
        return _context.TvaTypes.OrderBy(p => p.ID).ToList();
    }
    public TvaType GetTvaTypeById(int tvaTypeId)
    {
        return _context.TvaTypes.Where(c => c.ID == tvaTypeId).FirstOrDefault();
    }

    public bool UpdateTvaType(TvaType tvaType)
    {
        _context.Update(tvaType);
        return Save();
    }
    public bool DeleteTvaType(TvaType tvaType)
    {
        _context.Remove(tvaType);
        return Save();
    }

    public TvaType TvaTypeExists(TvaTypeResource tvaTypeCreate)
    {
        return GetTvaTypes().Where(c => c.Nom.Trim().ToUpper() == tvaTypeCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public bool TvaTypeExistsById(int tvaTypeId)
    {
        return _context.TvaTypes.Any(r => r.ID == tvaTypeId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
