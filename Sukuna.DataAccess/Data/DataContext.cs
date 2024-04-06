using Microsoft.EntityFrameworkCore;
using Sukuna.Common.Models;

namespace Sukuna.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ClientOrder> ClientOrders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<TvaType> TvaTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer la relation entre SupplierOrder et OrderLine
            modelBuilder.Entity<Article>()
                .HasOne(p => p.TvaType)
                .WithMany(pc => pc.Articles)
                .HasForeignKey(p => p.TvaTypeID);
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Supplier)
                .WithMany(pc => pc.Articles)
                .HasForeignKey(p => p.SupplierID);

            // Configurer la relation entre SupplierOrder et OrderLine
            modelBuilder.Entity<OrderLine>()
                .HasOne(c => c.ClientOrder)
                .WithMany(pc => pc.OrderLines)
                .HasForeignKey(p => p.ClientOrderID);
            modelBuilder.Entity<OrderLine>()
                .HasOne(c => c.SupplierOrder)
                .WithMany(pc => pc.OrderLines)
                .HasForeignKey(p => p.SupplierOrderID);
            modelBuilder.Entity<OrderLine>()
                .HasOne(c => c.Article)
                .WithMany(pc => pc.OrderLines)
                .HasForeignKey(p => p.ArticleID);

            // Configurer la relation entre SupplierOrder et OrderLine
            modelBuilder.Entity<SupplierOrder>()
                .HasOne(p => p.User)
                .WithMany(pc => pc.SupplierOrders)
                .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<SupplierOrder>()
           .HasOne(p => p.Supplier)
           .WithMany(pc => pc.SupplierOrders)
           .HasForeignKey(c => c.SupplierID);

            // Configurer la relation entre SupplierOrder et OrderLine
            modelBuilder.Entity<ClientOrder>()
                .HasOne(p => p.Client)
                .WithMany(pc => pc.ClientOrders)
                .HasForeignKey(p => p.ClientID);

        }
    }
}