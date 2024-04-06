using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Business.Interfaces;

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

    public User GetUserTrimToUpper(UserResource userCreate)
    {
        return GetUsers().Where(c => c.Nom.Trim().ToUpper() == userCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public ICollection<User> GetUsers()
    {
        return _context.Users.OrderBy(p => p.ID).ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
