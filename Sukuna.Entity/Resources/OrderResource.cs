using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class OrderResource

{
    public int ID { get; set; }
    public int UserID { get; set; } // L'identifiant de l'utilisateur (pour les commandes fournisseurs)
    public int ClientID { get; set; } // L'identifiant du client (pour les commandes clients)
    public int SupplierID { get; set; } // L'identifiant du fournisseur (pour les commandes fournisseurs)
    public DateTime DateCommande { get; set; }
    public string StatutCommande { get; set; }
    public List<OrderLineResource> OrderLines { get; set; } // Les lignes de la commande
}