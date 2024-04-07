using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources
{
    public class OrderLineResource
    {
        public int ID { get; set; }
        public int? SupplierOrderID { get; set; }
        public int? ClientOrderID { get; set; }
        public int ArticleID { get; set; }
        public int Quantite { get; set; }
        public int PrixUnitaire { get; set; }
    }
}
