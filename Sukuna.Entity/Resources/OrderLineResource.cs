using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources
{
    public class OrderLineResource
    {
        public int ID { get; set; }
        public int ArticleID { get; set; } // L'identifiant de l'article commandé
        public int Quantite { get; set; }
        public int PrixUnitaire { get; set; }
    }
}
