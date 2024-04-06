using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class OrderLine
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("SupplierOrder")]
        public int? SupplierOrderID { get; set; }
        public SupplierOrder SupplierOrder { get; set; }

        [ForeignKey("ClientOrder")]
        public int? ClientOrderID { get; set; }
        public ClientOrder ClientOrder { get; set; }
        [ForeignKey("Article")]
        public int ArticleID { get; set; }
        public Article Article { get; set; }
        public int Quantite { get; set; }
        public int PrixUnitaire { get; set; }
    }
}
