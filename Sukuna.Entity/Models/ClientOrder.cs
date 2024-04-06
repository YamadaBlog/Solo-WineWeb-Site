using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class ClientOrder
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public DateTime DateCommande { get; set; }
        public string StatutCommande { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } // Relation une commande a plusieurs lignes de commande
    }

}
