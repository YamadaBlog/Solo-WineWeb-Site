using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ITvaTypeService
{
    bool CreateTvaType(TvaType tvaType);
    TvaType GetTvaTypeById(int tvaTypeId);
    ICollection<TvaType> GetTvaTypes();
    bool UpdateTvaType(TvaType tvaType);
    bool DeleteTvaType(TvaType tvaType);
    TvaType TvaTypeExists(TvaTypeResource tvaTypeCreate);
    bool TvaTypeExistsById(int tvaTypeId);

    bool Save();
}
