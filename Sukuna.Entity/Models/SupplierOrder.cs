using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class SupplierOrder
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }  // Clé étrangère vers le fournisseur
        public Supplier Supplier { get; set; }
        public DateTime DateCommande { get; set; }
        public string StatutCommande { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } // Relation une commande a plusieurs lignes de commande
    }
}
