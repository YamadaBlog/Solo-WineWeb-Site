using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Prix { get; set; }
        public int QuantiteEnStock { get; set; }
        public int TvaTypeID { get; set; }
        public TvaType TvaType { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; } // Relation un article à plusieurs lignes de commande
    }
}