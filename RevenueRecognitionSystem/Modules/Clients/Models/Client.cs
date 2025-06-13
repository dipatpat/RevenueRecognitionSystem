using System.ComponentModel;
using System.Diagnostics.Contracts;
using RevenueRecognitionSystem.Modules.Licence.Models;
using RevenueRecognitionSystem.Modules.Licence.Models;

namespace RevenueRecognitionSystem.Features.Clients.Models;

public abstract class Client
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public ICollection<Licence> Licences { get; set; } = new List<Licence>();

    public abstract string GetClientType();
}