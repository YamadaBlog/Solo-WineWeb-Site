using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Business.Interfaces;

namespace Sukuna.Service.Services;

public class ClientService : IClientService
{
    private readonly DataContext _context;

    public ClientService(DataContext context)
    {
        _context = context;
    }

    public bool CreateClient(Client client)
    {
        _context.Add(client);

        return Save();
    }

    public Client GetClientTrimToUpper(ClientResource clientCreate)
    {
        return GetClients().Where(c => c.Nom.Trim().ToUpper() == clientCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public ICollection<Client> GetClients()
    {
        return _context.Clients.OrderBy(p => p.ID).ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
