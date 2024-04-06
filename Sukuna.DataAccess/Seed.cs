using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;

namespace Sukuna.DataAccess
{
    public class Seed
    {
        private readonly DataContext context;

        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void SeedDataContext()
        {
            // Vérifie si la base de données contient déjà des données
            if (context.Articles.Any() || context.Clients.Any() || context.Suppliers.Any() || context.TvaTypes.Any() ||
                context.SupplierOrders.Any() || context.ClientOrders.Any() || context.OrderLines.Any() || context.Users.Any())
            {
                // La base de données a déjà été reinitialisée
                context.Articles.RemoveRange(context.Articles);
                context.Clients.RemoveRange(context.Clients);
                context.Users.RemoveRange(context.Users);
                context.Suppliers.RemoveRange(context.Suppliers);
                context.ClientOrders.RemoveRange(context.ClientOrders);
                context.SupplierOrders.RemoveRange(context.SupplierOrders);
                context.OrderLines.RemoveRange(context.OrderLines);
                context.TvaTypes.RemoveRange(context.TvaTypes);
                context.SaveChanges();
            }

            var supplier1 = new Supplier { Nom = "Supplier A", Adresse = "10 Rue du Commerce", Email = "supplierA@example.com" };
            var supplier2 = new Supplier { Nom = "Supplier B", Adresse = "20 Avenue des Vins", Email = "supplierB@example.com" };

            context.Suppliers.AddRange(supplier1, supplier2);
            context.SaveChanges();

            // Remplir la base de données avec des données initiales
            var tvaType1 = new TvaType { Nom = "TVA normale", Taux = 20, Description = "TVA standard" };
            var tvaType2 = new TvaType { Nom = "TVA réduite", Taux = 10, Description = "TVA réduite pour certains produits" };

            context.TvaTypes.AddRange(tvaType1, tvaType2);

            var article1 = new Article { Nom = "Vin rouge", Description = "Vin rouge de qualité", Prix = 15, QuantiteEnStock = 100, TvaType = tvaType1, Supplier = supplier1 };
            var article2 = new Article { Nom = "Vin blanc", Description = "Vin blanc rafraîchissant", Prix = 12, QuantiteEnStock = 80, TvaType = tvaType1, Supplier = supplier2 };
            var article3 = new Article { Nom = "Champagne", Description = "Champagne pétillant", Prix = 30, QuantiteEnStock = 50, TvaType = tvaType1, Supplier = supplier2 };

            context.Articles.AddRange(article1, article2, article3);

            var client1 = new Client { Nom = "Dupont", Prenom = "Jean", Adresse = "1 Rue de Paris", Email = "jean.dupont@example.com" };
            var client2 = new Client { Nom = "Durand", Prenom = "Marie", Adresse = "5 Avenue des Fleurs", Email = "marie.durand@example.com" };

            context.Clients.AddRange(client1, client2);
            context.SaveChanges();

            var user1 = new User { Nom = "Admin", Prenom = "Super", Email = "admin@example.com", MotDePasseHashe = "hashedpassword", Role = "Admin" };
            var user2 = new User { Nom = "Employé", Prenom = "Normal", Email = "employee@example.com", MotDePasseHashe = "hashedpassword", Role = "Employee" };

            context.Users.AddRange(user1, user2);
            context.SaveChanges();

            var supplierOrder1 = new SupplierOrder { UserID = user1.ID, SupplierID = supplier1.ID, DateCommande = DateTime.Now, StatutCommande = "En attente" };
            var supplierOrder2 = new SupplierOrder { UserID = user2.ID, SupplierID = supplier2.ID, DateCommande = DateTime.Now, StatutCommande = "En attente" };

            context.SupplierOrders.AddRange(supplierOrder1, supplierOrder2);

            var clientOrder1 = new ClientOrder { ClientID = client1.ID, DateCommande = DateTime.Now, StatutCommande = "En attente" };
            var clientOrder2 = new ClientOrder { ClientID = client2.ID, DateCommande = DateTime.Now, StatutCommande = "En attente" };

            context.ClientOrders.AddRange(clientOrder1, clientOrder2);
            context.SaveChanges();

            var orderLine1 = new OrderLine { SupplierOrderID = supplierOrder1.ID, ArticleID = article1.ID, Quantite = 10, PrixUnitaire = article1.Prix };
            var orderLine2 = new OrderLine { SupplierOrderID = supplierOrder2.ID, ArticleID = article2.ID, Quantite = 20, PrixUnitaire = article2.Prix };
            var orderLine3 = new OrderLine { ClientOrderID = clientOrder1.ID, ArticleID = article3.ID, Quantite = 5, PrixUnitaire = article3.Prix };
            var orderLine4 = new OrderLine { ClientOrderID = clientOrder2.ID, ArticleID = article1.ID, Quantite = 15, PrixUnitaire = article1.Prix };

            context.OrderLines.AddRange(orderLine1, orderLine2, orderLine3, orderLine4);

            context.SaveChanges();
        }
    }
        }
 
