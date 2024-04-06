using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ITvaTypeService
{
    TvaType GetTvaType(int tvaTypeID);
    bool Save();
}
