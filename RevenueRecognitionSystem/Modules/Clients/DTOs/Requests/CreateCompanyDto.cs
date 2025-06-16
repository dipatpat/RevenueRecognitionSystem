namespace RevenueRecognitionSystem.Modules.Clients.DTOs.Requests;

public class CreateCompanyClientDto
{
    public string Name { get; set; } = null!;
    public string KRS { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
