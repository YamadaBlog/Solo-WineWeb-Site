using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Models;

public class User
{
    [Key]
    public int ID { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string MotDePasseHashe { get; set; }
    public string Role { get; set; }
    public ICollection<SupplierOrder> SupplierOrders { get; set; } // Relation un client a plusieurs commandes

}
