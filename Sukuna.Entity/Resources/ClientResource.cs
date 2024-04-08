using Sukuna.Common.Models;

namespace Sukuna.Common.Resources;

public class ClientResource
{
    public int ID { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Adresse { get; set; }
    public string Email { get; set; }
    public string MotDePasseHashe { get; set; }
}
