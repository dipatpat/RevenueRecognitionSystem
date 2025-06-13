namespace RevenueRecognitionSystem.Features.Clients.Models;

public abstract class Client
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public abstract string GetClientType();
}