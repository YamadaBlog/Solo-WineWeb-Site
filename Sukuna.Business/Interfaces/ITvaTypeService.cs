using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces
{
    public interface ITvaTypeService
    {
        TvaType GetTvaTypeTrimToUpper(TvaTypeResource tvaTypeCreate);
        bool CreateTvaType(TvaType tvaType);
        ICollection<TvaType> GetTvaTypes();
        bool Save();
    }
}
