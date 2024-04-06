using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class UserResource
{
    public int ID { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string MotDePasseHashe { get; set; }
    public string Role { get; set; }
}
