using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Models
{

    public class Client
    {
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string MotDePasseHashe { get; set; }
        public ICollection<ClientOrder> ClientOrders { get; set; } // Relation un client a plusieurs commandes
    }
}