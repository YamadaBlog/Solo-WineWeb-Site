using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class Supplier
    {
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public ICollection<SupplierOrder> SupplierOrders { get; set; } // Relation un fournisseur a plusieurs commandes
        public ICollection<Article> Articles { get; set; } 
    }
}
