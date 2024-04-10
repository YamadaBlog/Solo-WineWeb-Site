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
    bool CreateClient(Client client);
    Client GetClientById(int clientId);
    Client GetAuthauthClient(string clientEmail, string clientMpd);
    ICollection<Client> GetClients();
    ICollection<ClientOrder> GetClientOrdersByClient(int clientId);
    bool UpdateClient(Client client);
    bool DeleteClient(Client client);
    bool ClientExists(ClientResource clientCreate);
    bool ClientExistsById(int clientId);

    bool Save();
}
