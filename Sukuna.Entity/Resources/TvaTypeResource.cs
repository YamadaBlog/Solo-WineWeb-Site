using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources
{
    public class TvaTypeResource
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public int Taux { get; set; }
        public string Description { get; set; }

    }
}
