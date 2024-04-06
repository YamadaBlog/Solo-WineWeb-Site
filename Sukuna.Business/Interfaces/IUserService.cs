using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces
{
    public interface IUserService
    {
        User GetUserTrimToUpper(UserResource userCreate);
        bool CreateUser(User user);
        ICollection<User> GetUsers();
        bool Save();
    }
}
