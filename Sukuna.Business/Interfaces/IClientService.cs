using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IClientService
{
    Client GetClientTrimToUpper(ClientResource clientCreate);
    bool CreateClient(Client client);
    ICollection<Client> GetClients();
    bool Save();
}
