using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class SupplierOrderResource
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int SupplierID { get; set; }  // Clé étrangère vers le fournisseur
    public DateTime DateCommande { get; set; }
    public string StatutCommande { get; set; }
}
