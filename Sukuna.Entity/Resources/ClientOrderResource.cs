using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class ClientOrderResource
{
    public int ID { get; set; }
    public int ClientID { get; set; }
    public DateTime DateCommande { get; set; }
    public string StatutCommande { get; set; }

}
