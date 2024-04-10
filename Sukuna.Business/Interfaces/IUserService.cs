using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IUserService
{
    bool CreateUser(User user);
    User GetUserById(int userId);
    ICollection<User> GetUsers();
    User GetAuthauthUser(string userEmail, string userMpd);
    ICollection<SupplierOrder> GetSupplierOrdersByUser(int userId);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
    bool UserExists(UserResource userCreate);
    bool UserExistsById(int userId);

    bool Save();
}
