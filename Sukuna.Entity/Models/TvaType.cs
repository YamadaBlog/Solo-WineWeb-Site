using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class TvaType
    {
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public int Taux { get; set; }
        public string Description { get; set; }

        // Propriété de navigation pour les articles associés à ce type de TVA
        public ICollection<Article> Articles { get; set; }
    }
}
