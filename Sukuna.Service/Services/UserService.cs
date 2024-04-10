using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public bool CreateUser(User user)
    {
        _context.Add(user);

        return Save();
    }

    public User GetAuthauthUser(string userEmail, string userMpd)
    {
        return _context.Users.Where(c => c.Email == userEmail && c.MotDePasseHashe == userMpd).FirstOrDefault();
    }
    public ICollection<User> GetUsers()
    {
        return _context.Users.OrderBy(p => p.ID).ToList();
    }
    public User GetUserById(int userId)
    {
        return _context.Users.Where(c => c.ID == userId).FirstOrDefault();
    }

    public ICollection<SupplierOrder> GetSupplierOrdersByUser(int userId)
    {
        return _context.SupplierOrders.Where(r => r.User.ID == userId).ToList();
    }

    public bool UpdateUser(User user)
    {
        _context.Update(user);
        return Save();
    }
    public bool DeleteUser(User user)
    {
        _context.Remove(user);
        return Save();
    }

    public bool UserExists(UserResource userCreate)
    {
        return _context.Users.Any(r => r.ID == userCreate.ID);
    }

    public bool UserExistsById(int userId)
    {
        return _context.Users.Any(r => r.ID == userId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
