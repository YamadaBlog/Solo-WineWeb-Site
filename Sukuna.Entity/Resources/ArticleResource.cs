using Sukuna.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Resources;

public class ArticleResource // Les ressources sont les saisies utilisateurs
{
    public int ID { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public int Prix { get; set; }
    public int QuantiteEnStock { get; set; }
    public string ImageUrl { get; set; }
    public int TvaTypeID { get; set; }
    public int SupplierID { get; set; }
}
